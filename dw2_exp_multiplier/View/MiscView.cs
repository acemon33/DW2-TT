using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using dw2_exp_multiplier.Base;
using dw2_exp_multiplier.Manager;


namespace dw2_exp_multiplier.View
{
    public partial class MiscView : UserControl
    {
        #region Fields Region
        Dictionary<string, Bitmap> imageList = new Dictionary<string, Bitmap>();
        #endregion

        public MiscView()
        {
            InitializeComponent();
            digibeetleComboBox.DisplayMember = "Value";
            digibeetleComboBox.ValueMember = "Key";
            try
            {
                digibeetleComboBox.DataSource = new BindingSource(DigiBeetlePatcher.getDigiBeetleIds(ref this.imageList, "Resources\\digi-beetle.zip"), null);
            }
            catch (Exception e) { }
        }

        #region Button Events Region
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

                    if (noEncounterPatcherCheckBox.Checked)
                        enemysetManager.AddModifier(new UnmoveableEnemy());

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

                List<IPatcher> patcherList = new List<IPatcher>();

                if (dnaLabsPatcherCheckBox.Checked)
                    patcherList.Add(new DnaLabsPatcher());

                if (DnaDpCheckBox.Checked)
                    patcherList.Add(new DnaDpPatcher());

                if (digimonGiftPatcherCheckBox.Checked)
                    patcherList.Add(new DigimonGiftPatcher());

                if (digiLinePatcherCheckBox.Checked)
                    patcherList.Add(new DigiLinePatcher());

                if (beastKingFistEnhanceCheckBox.Checked)
                    patcherList.Add(new BattleEnhancementPatcher());
                                
                if (battleFixPatcherCheckBox.Checked)
                {
                    BattleFixPatcher battleFixPatcher = new BattleFixPatcher(ref fs);
                    battleFixPatcher.Patch(ref fs);
                }

                if (nameExpansionPatcherCheckBox.Checked)
                {
                    NameExpansionPatcher nameExpansionPatcher = new NameExpansionPatcher(ref fs);
                    nameExpansionPatcher.Patch(ref fs);
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
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
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
                groupBox5.Enabled = true;
                battlePatchesGroupBox.Enabled = true;
                Main.GetMain().UnHideOptions();
            }
            else
            {
                saveButton.Enabled = false;
                exportButton.Enabled = false;
                unHideAAA.Enabled = false;
                digibeetleComboBox.Enabled = false;
                groupBox5.Enabled = false;
                battlePatchesGroupBox.Enabled = false;
                Main.GetMain().HideOptions();
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
                noEncounterPatcherCheckBox.Enabled = true;
            }
            else
            {
                importButton.Enabled = false;
                multiplier.Enabled = false;
                bossMultiplier.Enabled = false;
                extremeModeCheckBox.Enabled = false;
                noEncounterPatcherCheckBox.Enabled = false;
            }
        }

        private string ValidateFilename(ref DragEventArgs e)
        {
            string filename = (e.Data.GetData(DataFormats.FileDrop) as string[])[0];
            if (filename.Contains(".bin") || filename.Contains(".BIN")) return filename;
            return "";
        }

        public string GetDw2BinPath()
        {
            return this.dw2TextBox.Text;
        }
        #endregion

        #region MouseHover Events Region
        private void noEncounterPatcherCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(UnmoveableEnemy.TOOLTIP, noEncounterPatcherCheckBox, 30, 0, 5000);
        }

        private void battleEnhancementPatcherCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(BattleEnhancementPatcher.TOOLTIP, battleEnhancementPatcherCheckBox, 30, 0, 5000);
        }

        private void battleFixPatcherCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(BattleFixPatcher.TOOLTIP, battleFixPatcherCheckBox, 30, 0, 5000);
        }

        private void dnaLabsPatcherCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(DnaLabsPatcher.TOOLTIP, dnaLabsPatcherCheckBox, 30, 0, 5000);
        }

        private void digiLinePatcherCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(DigiLinePatcher.TOOLTIP, digiLinePatcherCheckBox, 30, 0, 5000);
        }

        private void digimonGiftPatcherCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(DigimonGiftPatcher.TOOLTIP, digimonGiftPatcherCheckBox, 30, 0, 5000);
        }
        
        private void nameExpansionPatcherCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(NameExpansionPatcher.TOOLTIP, nameExpansionPatcherCheckBox, 30, 0, 5000);
        }
        #endregion
    }

}
