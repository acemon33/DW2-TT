using System;
using System.IO;
using System.Windows.Forms;
using dw2_exp_multiplier.Base;
using dw2_exp_multiplier.View;


namespace dw2_exp_multiplier
{
    public partial class Main : Form
    {
        #region Fields Region
        public static string Title;
        public static ToolTip HintToolTip;
        private static Main main;
        private string CurrentDirectory;
        #endregion

        public Main()
        {
            InitializeComponent();
            Main.Title = this.Text;
            main = this;
            HintToolTip = new ToolTip();
            this.tabControl1.TabPages.RemoveByKey("tabPage3");
            fileManagerToolStripMenuItem.Checked = false;
            this.CurrentDirectory = Directory.GetCurrentDirectory();
        }

        public static Main GetMain() { return main; }
        
        #region Button Events Region
        private void about_Click(object sender, EventArgs e)
        {
            var aboutForm = new About();
            aboutForm.ShowDialog();
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
            vanillaToolStripMenuItem.Checked = false;
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

        #region Option Menus Region
        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs = null;
            try
            {
                string filename = this.miscView.GetDw2BinPath();
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                DW2Image dw2Image = new DW2Image(ref fs, filename.ToLower().EndsWith(".vcd"), (Main.GetMain().IsUSVersionChecked()) ? DW2Slus.US_VERSION : DW2Slus.JAP_VERSION);

                FolderPicker fbd = new FolderPicker();
                fbd.Title = "Open Folder";
                fbd.InputPath = this.CurrentDirectory;

                if (fbd.ShowDialog(this.Handle) == true)
                {
                    this.CurrentDirectory = fbd.ResultPath;
                    byte[] buffer = dw2Image.ReadMainFile();
                    File.WriteAllBytes($"{this.CurrentDirectory}\\{FileManagerView.SLUS_NAME}.BIN", buffer);

                    foreach (var fileIndex in Configuration.backupRestoreFileIndexes)
                    {
                        buffer = dw2Image.ReadFile(fileIndex);
                        File.WriteAllBytes($"{this.CurrentDirectory}\\{fileIndex}.BIN", buffer);
                    }
                }
                MessageBox.Show("Files have been Exported Successfully", "Success", MessageBoxButtons.OK);
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

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs = null;
            try
            {
                string filename = this.miscView.GetDw2BinPath();
                fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                DW2Image dw2Image = new DW2Image(ref fs, filename.ToLower().EndsWith(".vcd"), (Main.GetMain().IsUSVersionChecked()) ? DW2Slus.US_VERSION : DW2Slus.JAP_VERSION);

                FolderPicker fbd = new FolderPicker();
                fbd.Title = "Open Folder";
                fbd.InputPath = this.CurrentDirectory;

                if (fbd.ShowDialog(this.Handle) == true)
                {
                    this.CurrentDirectory = fbd.ResultPath;

                    string file = $"{this.CurrentDirectory}\\{FileManagerView.SLUS_NAME}.BIN";
                    if (!File.Exists(file)) throw new FileNotFoundException($"File: {file} Not Found");
                    byte[] buffer = File.ReadAllBytes(file);
                    dw2Image.WriteMainFile(ref buffer);

                    foreach (var fileIndex in Configuration.backupRestoreFileIndexes)
                    {
                        file = $"{this.CurrentDirectory}\\{fileIndex}.BIN";
                        if (!File.Exists(file)) throw new FileNotFoundException($"File: {file} Not Found");
                        buffer = File.ReadAllBytes(file);
                        dw2Image.WriteFile(ref buffer, fileIndex);
                    }
                }
                MessageBox.Show("Files have been Imported Successfully", "Success", MessageBoxButtons.OK);
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

        public void UnHideOptions()
        {
            this.backupToolStripMenuItem.Enabled = true;
            this.restoreToolStripMenuItem.Enabled = true;
        }
        
        public void HideOptions()
        {
            this.backupToolStripMenuItem.Enabled = false;
            this.restoreToolStripMenuItem.Enabled = false;
        }

        private void fileManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileManagerToolStripMenuItem.Checked)
            {
                this.tabControl1.Controls.Add(this.tabPage3);
                this.tabControl1.SelectedIndex = 2;
            }
            else
            {
                this.tabControl1.TabPages.RemoveByKey("tabPage3");
            }
        }
        public bool IsUSVersionChecked()
        {
            return this.usVersionToolStripMenuItem.Checked;
        }

        private void usVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.usVersionToolStripMenuItem.Checked = true;
            this.japVersionToolStripMenuItem.Checked = false;
        }

        private void japVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.usVersionToolStripMenuItem.Checked = false;
            this.japVersionToolStripMenuItem.Checked = true;
        }
        #endregion
    }

}
