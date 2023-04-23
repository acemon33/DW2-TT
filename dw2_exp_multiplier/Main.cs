using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using dw2_exp_multiplier.Base;
using dw2_exp_multiplier.Manager;
using dw2_exp_multiplier.View;


namespace dw2_exp_multiplier
{
    public partial class Main : Form
    {
        #region Fields Region
        Dictionary<string, Bitmap> imageList = new Dictionary<string, Bitmap>();
        public static string Title;
        #endregion

        public Main()
        {
            InitializeComponent();
            Main.Title = this.Text;
            digibeetleComboBox.DisplayMember = "Value";
            digibeetleComboBox.ValueMember = "Key";
            digibeetleComboBox.DataSource = new BindingSource(DigiBeetlePatcher.getDigiBeetleIds(ref this.imageList, "Resources\\digi-beetle.zip"), null);
            if (!(Control.ModifierKeys == Keys.Shift))
                tabControl1.TabPages.RemoveByKey("tabPage3");
        }

        #region Button Events Region
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            string filename = dw2TextBox.Text;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
            try
            {
                DW2Slus.ValidImageFile(ref fs);

                if (enemysetTextBox.Text.Length > 0)
                {
                    EnemysetManager enemysetManager = new EnemysetManager(enemysetTextBox.Text);
                    
                    if (multiplier.Value.CompareTo(Decimal.One) != 0)
                        enemysetManager.AddModifier(new ExpBitsMultiplier(multiplier.Value));

                    if (bossMultiplier.Value.CompareTo(Decimal.One) != 0)
                    {
                        if (extremeModeCheckBox.Checked)
                            enemysetManager.AddModifier(new ExtremeStatsMultiplier(multiplier.Value));
                        else
                            enemysetManager.AddModifier(new BossStatsMultiplier(multiplier.Value));
                    }

                    enemysetManager.ApplyModifiers();

                    if (Control.ModifierKeys == Keys.Control)
                        enemysetManager.WrtieToFile("ENEMYSET_TEST.BIN");
                    else
                        enemysetManager.WriteToBin(ref fs);
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

                if (dnaLabsPatcherCheckBox.Checked)
                {
                    DnaLabsPatcher dnaLabsPatcher = new DnaLabsPatcher(ref fs);
                    if (dnaLabsPatchRadioButton.Checked)
                        dnaLabsPatcher.Patch(ref fs);
                    else
                        dnaLabsPatcher.UnPatch(ref fs);
                }
                
                if (digimonGiftPatcherCheckBox.Checked)
                {
                    DigimonGiftPatcher digimonGiftPatcher = new DigimonGiftPatcher(ref fs);
                    if (digimonGiftPatchRadioButton.Checked)
                        digimonGiftPatcher.Patch(ref fs);
                    else
                        digimonGiftPatcher.UnPatch(ref fs);
                }

                if (digiLinePatcherCheckBox.Checked)
                {
                    DigiLinePatcher digiLinePatcher = new DigiLinePatcher(ref fs);
                    if (digiLinePatchRadioButton.Checked)
                        digiLinePatcher.Patch(ref fs);
                    else
                        digiLinePatcher.UnPatch(ref fs);
                }
                                
                if (mpOnGuardPatcherCheckBox.Checked)
                {
                    MpOnGuardPatcher mpOnGuardPatcher = new MpOnGuardPatcher(ref fs);
                    if (mpOnGuardPatchRadioButton.Checked)
                        mpOnGuardPatcher.Patch(ref fs);
                    else
                        mpOnGuardPatcher.UnPatch(ref fs);
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
            string filename = dw2TextBox.Text;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
            try
            {
                DW2Slus.ValidImageFile(ref fs);

                EnemysetManager enemysetManager = new EnemysetManager(ref fs);
                filename = Path.GetDirectoryName(dw2TextBox.Text) + "\\ENEMYSET_" + Path.GetFileNameWithoutExtension(dw2TextBox.Text) + ".BIN";
                enemysetManager.WrtieToFile(filename);
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
            string filename = dw2TextBox.Text;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
            try
            {
                DW2Slus.ValidImageFile(ref fs);

                EnemysetManager enemysetManager = new EnemysetManager(enemysetTextBox.Text);
                enemysetManager.WriteToBin(ref fs);
                
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
        #endregion

        #region TextBox Events Region
        private void dw2TextBox_TextChanged(object sender, EventArgs e)
        {
            this.checkSaveButton();
            if (dw2TextBox.Text.Length > 0)
            {
                saveButton.Enabled = true;
                exportButton.Enabled = true;
                unHideAAA.Enabled = true;
                digibeetleComboBox.Enabled = true;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                groupBox5.Enabled = true;
                groupBox6.Enabled = true;
            }
            else
            {
                saveButton.Enabled = false;
                exportButton.Enabled = false;
                unHideAAA.Enabled = false;
                digibeetleComboBox.Enabled = false;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
                groupBox5.Enabled = false;
                groupBox6.Enabled = false;
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
        #endregion

        #region Helper Methods Region
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

        private string ValidateFilename(ref DragEventArgs e)
        {
            string filename = (e.Data.GetData(DataFormats.FileDrop) as string[])[0];
            if (filename.Contains(".bin") || filename.Contains(".BIN")) return filename;
            return "";
        }
        #endregion

        #region Other Methods Region
        private void digibeetleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = ((KeyValuePair<string, string>)digibeetleComboBox.SelectedItem).Key;
            if (imageList.ContainsKey(id)) this.digibeetlPictureBox.Image = imageList[id];
            else this.digibeetlPictureBox.Image = null;
        }
        
        private void dnaLabsPatcherCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (dnaLabsPatcherCheckBox.Checked)
            {
                dnaLabsPatchRadioButton.Enabled = true;
                dnaLabsUnpatchRadioButton.Enabled = true;
            }
            else
            {
                dnaLabsPatchRadioButton.Enabled = false;
                dnaLabsUnpatchRadioButton.Enabled = false;
            }
        }
        
        private void digimonGiftPatcherCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (digimonGiftPatcherCheckBox.Checked)
            {
                digimonGiftPatchRadioButton.Enabled = true;
                digimonGiftUnpatchRadioButton.Enabled = true;
            }
            else
            {
                digimonGiftPatchRadioButton.Enabled = false;
                digimonGiftUnpatchRadioButton.Enabled = false;
            }
        }

        private void digiLinePatcherCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (digiLinePatcherCheckBox.Checked)
            {
                digiLinePatchRadioButton.Enabled = true;
                digiLineUnpatchRadioButton.Enabled = true;
            }
            else
            {
                digiLinePatchRadioButton.Enabled = false;
                digiLineUnpatchRadioButton.Enabled = false;
            }
        }
        
        private void mpOnGuardPatcherCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (mpOnGuardPatcherCheckBox.Checked)
            {
                mpOnGuardPatchRadioButton.Enabled = true;
                mpOnGuardUnpatchRadioButton.Enabled = true;
            }
            else
            {
                mpOnGuardPatchRadioButton.Enabled = false;
                mpOnGuardUnpatchRadioButton.Enabled = false;
            }
        }
        #endregion

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
