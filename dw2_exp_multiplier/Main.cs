using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using dw2_exp_multiplier.Base;
using dw2_exp_multiplier.Entity;
using dw2_exp_multiplier.Manager;
using dw2_exp_multiplier.Mapper;

/*
 * @author acemon33
 */

namespace dw2_exp_multiplier
{
    public partial class Main : Form
    {
        List<Enemyset> EnemysetList = new List<Enemyset>();
        Dictionary<string, Bitmap> imageList = new Dictionary<string, Bitmap>();
        public static string Title;
        
        public Main()
        {
            InitializeComponent();
            Main.Title = this.Text;
            digibeetleComboBox.DisplayMember = "Value";
            digibeetleComboBox.ValueMember = "Key";
            digibeetleComboBox.DataSource = new BindingSource(DigiBeetlePatcher.getDigiBeetleIds(ref this.imageList, "Resources\\digi-beetle.zip"), null);
        }

        private void about_Click(object sender, EventArgs e)
        {
            var aboutForm = new About();
            aboutForm.ShowDialog();
        }

        private void dw2BrowseButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Open DW2 Bin File";
            ofd.Filter = "Digimon World 2 File|*.bin";
            if (ofd.ShowDialog() == DialogResult.OK) dw2TextBox.Text = ofd.FileName;
        }

        private void enemysetBrowseButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Open ENEMYSET.BIN File";
            ofd.Filter = "ENEMYSET.BIN File|*.bin";
            if (ofd.ShowDialog() == DialogResult.OK) enemysetTextBox.Text = ofd.FileName;
        }

        private void checkSaveButton()
        {
            if (dw2TextBox.Text.Length > 0 && enemysetTextBox.Text.Length > 0)
            {
                importButton.Enabled = true;
                multiplier.Enabled = true;
                bossMultiplier.Enabled = true;
                extremeModeCheckBox.Enabled = true;
            }
            else
            {
                importButton.Enabled = false;
                multiplier.Enabled = false;
                bossMultiplier.Enabled = false;
                extremeModeCheckBox.Enabled = false;
            }
        }

        private void dw2TextBox_TextChanged(object sender, EventArgs e)
        {
            this.checkSaveButton();
            if (dw2TextBox.Text.Length > 0)
            {
                saveButton.Enabled = true;
                exportButton.Enabled = true;
                unHideAAA.Enabled = true;
                digibeetleComboBox.Enabled = true;
            }
            else
            {
                saveButton.Enabled = false;
                exportButton.Enabled = false;
                unHideAAA.Enabled = false;
                digibeetleComboBox.Enabled = false;
            }
        }

        private void enemysetTextBox_TextChanged(object sender, EventArgs e)
        {
            this.checkSaveButton();
        }

        private void dw2TextBox_DragDrop(object sender, DragEventArgs e)
        {
            dw2TextBox.Text = this.ValidateFilename(ref e);
        }

        private void dw2TextBox_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void enemysetTextBox_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void enemysetTextBox_DragDrop(object sender, DragEventArgs e)
        {
            enemysetTextBox.Text = this.ValidateFilename(ref e);
        }

        private string ValidateFilename(ref DragEventArgs e)
        {
            string filename = (e.Data.GetData(DataFormats.FileDrop) as string[])[0];
            if (filename.Contains(".bin") || filename.Contains(".BIN")) return filename;
            return "";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.EnemysetList.Clear();
            string filename = dw2TextBox.Text;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
            try
            {
                DW2Slus.ValidImageFile(ref fs);

                if (enemysetTextBox.Text.Length > 0)
                {
                    EnemysetMapper.ReadFile(enemysetTextBox.Text, ref this.EnemysetList);
                    
                    if (multiplier.Value.CompareTo(Decimal.One) != 0)
                        EnemysetManager.MultiplyExpBits(multiplier.Value, ref this.EnemysetList);

                    if (bossMultiplier.Value.CompareTo(Decimal.One) != 0)
                    {
                        if (extremeModeCheckBox.Checked)
                            EnemysetManager.MultiplyExtremeStats(bossMultiplier.Value, ref this.EnemysetList);
                        else
                            EnemysetManager.MultiplyBossStats(bossMultiplier.Value, ref this.EnemysetList);
                    }
                    
                    if (Control.ModifierKeys == Keys.Control) EnemysetMapper.WriteFile("ENEMYSET_TEST.BIN", ref this.EnemysetList);
                    else EnemysetMapper.WriteBin(ref fs, ref this.EnemysetList);
                }
                
                if (unHideAAA.Checked)
                {
                    bool hide = (Control.ModifierKeys == Keys.Shift) ? true: false;
                    DW2Slus.UnhideAAAFolder(ref fs, hide);
                }

                if (digibeetleComboBox.SelectedIndex > 0)
                {
                    ushort id = Convert.ToUInt16(((KeyValuePair<string, string>)digibeetleComboBox.SelectedItem).Key, 16);
                    DigiBeetlePatcher.patch(ref fs, id);
                }
                
                MessageBox.Show("The file has been Saved Successfully", "Enjoy ^_^");
            }
            catch (FileLoadException ex)
            {
                MessageBox.Show(ex.Message, "DW2 invalid file");
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("The File \"" + filename + "\" is not found", "File Error");
            }
            catch (IOException ex)
            {
                MessageBox.Show("The File \"" + filename + "\" is being used by anothe program", "File Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            this.EnemysetList.Clear();
            string filename = dw2TextBox.Text;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
            try
            {
                DW2Slus.ValidImageFile(ref fs);

                EnemysetMapper.ReadBin(ref fs, ref this.EnemysetList);
                filename = Path.GetDirectoryName(dw2TextBox.Text) + "\\ENEMYSET_" + Path.GetFileNameWithoutExtension(dw2TextBox.Text) + ".BIN";
                EnemysetMapper.WriteFile(filename, ref this.EnemysetList);
                enemysetTextBox.Text = filename;
                
                MessageBox.Show("ENEMYSET.BIN has been Exported Successfully");
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("The File \"" + filename + "\" is not found", "File Error");
            }
            catch (IOException ex)
            {
                MessageBox.Show("The File \"" + filename + "\" is being used by anothe program", "File Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            this.EnemysetList.Clear();
            string filename = dw2TextBox.Text;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
            try
            {
                DW2Slus.ValidImageFile(ref fs);

                EnemysetMapper.ReadFile(enemysetTextBox.Text, ref this.EnemysetList);
                EnemysetMapper.WriteBin(ref fs, ref this.EnemysetList);
                
                MessageBox.Show("ENEMYSET.BIN has been Imported Successfully");
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("The File \"" + filename + "\" is not found", "File Error");
            }
            catch (IOException ex)
            {
                MessageBox.Show("The File \"" + filename + "\" is being used by anothe program", "File Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        private void digibeetleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = ((KeyValuePair<string, string>)digibeetleComboBox.SelectedItem).Key;
            if (imageList.ContainsKey(id)) this.digibeetlPictureBox.Image = imageList[id];
            else this.digibeetlPictureBox.Image = null;
        }

        #region SaveEditorMenuItem Region
        private void vanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vanillaToolStripMenuItem.Checked = true;
            improvementToolStripMenuItem.Checked = false;
            resurrectionToolStripMenuItem.Checked = false;
            alternativeToolStripMenuItem.Checked = false;
            faithfulToolStripMenuItem.Checked = false;
            Configuration.SetVanillaMode();
        }

        private void improvementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vanillaToolStripMenuItem.Checked = false;
            improvementToolStripMenuItem.Checked = true;
            resurrectionToolStripMenuItem.Checked = false;
            alternativeToolStripMenuItem.Checked = false;
            faithfulToolStripMenuItem.Checked = false;
            Configuration.SetImprovementMode();
        }

        private void resurrectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vanillaToolStripMenuItem.Checked = false;
            improvementToolStripMenuItem.Checked = false;
            resurrectionToolStripMenuItem.Checked = true;
            alternativeToolStripMenuItem.Checked = false;
            faithfulToolStripMenuItem.Checked = false;
            Configuration.SetResurrectionMode();
        }

        private void alternativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vanillaToolStripMenuItem.Checked = true;
            improvementToolStripMenuItem.Checked = false;
            resurrectionToolStripMenuItem.Checked = false;
            alternativeToolStripMenuItem.Checked = true;
            faithfulToolStripMenuItem.Checked = false;
            Configuration.SetAlternativeMode();
        }
        
        private void faithfulToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vanillaToolStripMenuItem.Checked = false;
            improvementToolStripMenuItem.Checked = false;
            resurrectionToolStripMenuItem.Checked = false;
            alternativeToolStripMenuItem.Checked = false;
            faithfulToolStripMenuItem.Checked = true;
            Configuration.SetFaithfulMode();
        }
        #endregion
    }
    
}
