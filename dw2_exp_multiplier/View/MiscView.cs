﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using dw2_exp_multiplier.Base;
using dw2_exp_multiplier.Patcher;
using dw2_exp_multiplier.Patcher.BattleEnhancement;
using dw2_exp_multiplier.Patcher.BattleFix;
using dw2_exp_multiplier.Patcher.BattleFeature;
using dw2_exp_multiplier.Patcher.Misc;


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

            miscTabPage.Enabled = false;
            battleFixTabPage.Enabled = false;
            battleEnhancementTabPage.Enabled = false;
            battleFeatureTabPage.Enabled = false;
        }

        private void digibeetleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = ((KeyValuePair<string, string>)digibeetleComboBox.SelectedItem).Key;
            if (imageList.ContainsKey(id)) this.digibeetlPictureBox.Image = imageList[id];
            else this.digibeetlPictureBox.Image = null;
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
                    patcherList.Add(new BeastKingFistPatcher());
                                
                if (chronoBreakerFixCheckBox.Checked)
                    patcherList.Add(new ChronoBreakerPatcher());

                if (nameExpansionPatcherCheckBox.Checked)
                    patcherList.Add(new NameExpansionPatcher());
                
                if (maxLevelPatcherCheckBox.Checked)
                    patcherList.Add(new MaxLevelLimitPatcher());

                if (dialogFastForwardPatchercheckBox.Checked)
                    patcherList.Add(new DialogFastForwardPatcher());

                if (zudokornMonsPatcherCheckBox.Checked)
                    patcherList.Add(new ZudokornMonsPatchers());

                if (domainFreeObstaclesCheckBox.Checked)
                    patcherList.Add(new DomainFreeObstaclesPatcher());

                if (floorSkippingCheckBox.Checked)
                    patcherList.Add(new FloorSkippingPatcher());

                if (RBugRemovalCheckBox.Checked)
                    patcherList.Add(new RBugRemovalPatcher());

                if (turnSkippingCheckBox.Checked)
                    patcherList.Add(new TurnSkippingPatcher());

                if (techOrderingCheckBox.Checked)
                    patcherList.Add(new TechOrderingPatcher());

                if (nextLevelLimitCheckBox.Checked)
                    patcherList.Add(new NextLevelLimitPatcher());

                foreach (IPatcher patcher in patcherList)
                    patcher.Patch(ref fs);

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
                miscTabPage.Enabled = true;
                battleFixTabPage.Enabled = true;
                battleEnhancementTabPage.Enabled = true;
                battleFeatureTabPage.Enabled = true;
                Main.GetMain().UnHideOptions();
            }
            else
            {
                saveButton.Enabled = false;
                exportButton.Enabled = false;
                unHideAAA.Enabled = false;
                digibeetleComboBox.Enabled = false;
                miscTabPage.Enabled = false;
                battleFixTabPage.Enabled = false;
                battleEnhancementTabPage.Enabled = false;
                battleFeatureTabPage.Enabled = false;
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

        private void beastKingFistEnhanceCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(BeastKingFistPatcher.TOOLTIP, beastKingFistEnhanceCheckBox, 30, 0, 5000);
        }

        private void chronoBreakerFixCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(ChronoBreakerPatcher.TOOLTIP, chronoBreakerFixCheckBox, 30, 0, 5000);
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

        private void maxLevelPatcherCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(MaxLevelLimitPatcher.TOOLTIP, maxLevelPatcherCheckBox, 30, 0, 5000);
        }

        private void dialogFastForwardPatchercheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(DialogFastForwardPatcher.TOOLTIP, dialogFastForwardPatchercheckBox, 30, 0, 5000);
        }

        private void ZudokornMonsPatcherCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(ZudokornMonsPatchers.TOOLTIP, zudokornMonsPatcherCheckBox, 30, 0, 5000);
        }

        private void DnaDpCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(DnaDpPatcher.TOOLTIP, DnaDpCheckBox, 30, 0, 5000);
        }

        private void domainFreeObstaclesCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(DomainFreeObstaclesPatcher.TOOLTIP, domainFreeObstaclesCheckBox, 30, 0, 5000);
        }

        private void floorSkippingCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(FloorSkippingPatcher.TOOLTIP, floorSkippingCheckBox, 30, 0, 10000);
        }

        private void RBugRemovalCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(RBugRemovalPatcher.TOOLTIP, RBugRemovalCheckBox, 30, 0, 5000);
        }

        private void turnSkippingCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(TurnSkippingPatcher.TOOLTIP, turnSkippingCheckBox, 30, 0, 5000);
        }

        private void techOrderingCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(TechOrderingPatcher.TOOLTIP, techOrderingCheckBox, 30, 0, 5000);
        }

        private void nextLevelLimitCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(NextLevelLimitPatcher.TOOLTIP, nextLevelLimitCheckBox, 30, 0, 5000);
        }

        private void darkSideAttackFixCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(DarkSideAttackPatcher.TOOLTIP, darkSideAttackFixCheckBox, 30, 0, 5000);
        }

        private void gigaByteWingFixCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(GigaByteWingPatcher.TOOLTIP, gigaByteWingFixCheckBox, 30, 0, 5000);
        }

        private void invincibilityFixCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(InvincibilityPatcher.TOOLTIP, invincibilityFixCheckBox, 30, 0, 5000);
        }

        private void shadowScytheFixCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(ShadowScythePatcher.TOOLTIP, shadowScytheFixCheckBox, 30, 0, 5000);
        }

        private void venomInfusionFixCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(VenomInfusionPatcher.TOOLTIP, venomInfusionFixCheckBox, 30, 0, 5000);
        }

        private void ZenRecoveryReInitilizeFixCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(ZenRecoveryReInitilizePatcher.TOOLTIP, ZenRecoveryReInitilizeFixCheckBox, 30, 0, 5000);
        }

        private void transcendSwordSpinningNeedleFixCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(Damage15vsGuardPatcher.TOOLTIP, transcendSwordSpinningNeedleFixCheckBox, 30, 0, 5000);
        }

        private void enemyBossConfusionCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(EnemyBossConfusionPatcher.TOOLTIP, enemyBossConfusionCheckBox, 30, 0, 5000);
        }

        private void enemyBossAffectionEnhanceCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(EnemyBossAffectionPatcher.TOOLTIP, enemyBossAffectionEnhanceCheckBox, 30, 0, 5000);
        }

        private void necroMagicEnhanceCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(NecroMagicPatcher.TOOLTIP, necroMagicEnhanceCheckBox, 30, 0, 5000);
        }

        private void poisonEffectEnhanceCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(PoisonEffectPatcher.TOOLTIP, poisonEffectEnhanceCheckBox, 30, 0, 5000);
        }

        private void evilTouchDemiDartEnhanceCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(EvilTouchDemiDartPatcher.TOOLTIP, evilTouchDemiDartEnhanceCheckBox, 30, 0, 5000);
        }

        private void subzeroIcePunchEnhanceCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(SubzeroIcePunchPatcher.TOOLTIP, subzeroIcePunchEnhanceCheckBox, 30, 0, 5000);
        }

        private void duoScissorClawEnhanceCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(DuoScissorClawPatcher.TOOLTIP, duoScissorClawEnhanceCheckBox, 30, 0, 5000);
        }

        private void mpOnGuardEnhanceCheckBox_MouseHover(object sender, EventArgs e)
        {
            Main.HintToolTip.Show(MpOnGaurdPatcher.TOOLTIP, mpOnGuardEnhanceCheckBox, 30, 0, 5000);
        }
        #endregion
    }
}
