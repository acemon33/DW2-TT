using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.View
{
    public partial class FileManagerView : UserControl
    {
        private static readonly int SLUS_INDEX = -1;
        public static readonly string SLUS_NAME = "SLUS";
        private string CurrentDirectory;
        private string GameCurrentDirectory;

        public FileManagerView()
        {
            InitializeComponent();
            this.CurrentDirectory = Directory.GetCurrentDirectory();
            this.GameCurrentDirectory = Directory.GetCurrentDirectory();
        }

        #region Control Events Region
        private void importButton_Click(object sender, EventArgs e)
        {
            FolderPicker fbd = new FolderPicker();
            fbd.Title = "Open Folder";
            fbd.InputPath = this.CurrentDirectory;
            if (fbd.ShowDialog(this.Handle) == true)
            {
                this.CurrentDirectory = fbd.ResultPath;
                string filename = dw2TextBox.Text;
                FileStream fs = null;
                try
                {
                    fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
                    DW2Image dw2Image = new DW2Image(ref fs, filename.ToLower().EndsWith(".vcd"), (Main.GetMain().IsUSVersionChecked()) ? DW2Slus.US_VERSION : DW2Slus.JAP_VERSION);

                    Dictionary<int, string> fileIndexes = this.ParseIndex(this.CurrentDirectory);

                    foreach (var file in fileIndexes)
                    {
                        if (!File.Exists(file.Value)) throw new FileNotFoundException("File: " + file.Value + " Not Found");
                        byte[] buffer = File.ReadAllBytes(file.Value);
                        if (file.Key == SLUS_INDEX)
                            dw2Image.WriteMainFile(ref buffer);
                        else
                            dw2Image.WriteFile(ref buffer, file.Key);
                    }
                    MessageBox.Show("File(s) Inserted Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            FolderPicker fbd = new FolderPicker();
            fbd.Title = "Open Folder";
            fbd.InputPath = this.CurrentDirectory;
            if (fbd.ShowDialog(this.Handle) == true)
            {
                this.CurrentDirectory = fbd.ResultPath;
                string filename = dw2TextBox.Text;
                FileStream fs = null;
                try
                {
                    fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    DW2Image dw2Image = new DW2Image(ref fs, filename.ToLower().EndsWith(".vcd"), (Main.GetMain().IsUSVersionChecked()) ? DW2Slus.US_VERSION : DW2Slus.JAP_VERSION);
     
                    Dictionary<int, string> fileIndexes = this.ParseIndex(this.CurrentDirectory);
     
                    foreach (var file in fileIndexes)
                    {
                        byte[] buffer = null;
                        if (file.Key == SLUS_INDEX)
                            buffer = dw2Image.ReadMainFile();
                        else
                            buffer = dw2Image.ReadFile(file.Key);
                        File.WriteAllBytes(file.Value, buffer);
                    }
                    
                    MessageBox.Show("File(s) Extracted Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                finally
                {
                    fs.Close();
                }
            }
        }
        
        private void dw2BrowseButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Open DW2 Bin File";
            ofd.Filter = "Digimon World 2 File|*.bin";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                dw2TextBox.Text = ofd.FileName;
                this.GameCurrentDirectory = Directory.GetParent(dw2TextBox.Text).ToString();
            }
        }

        private void dw2TextBox_DragDrop(object sender, DragEventArgs e)
        {
            dw2TextBox.Text = this.ValidateFilename(ref e);
            if (dw2TextBox.Text.Length > 0)
            {
                this.GameCurrentDirectory = Directory.GetParent(dw2TextBox.Text).ToString();
            }
        }


        private void dw2TextBox_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        #endregion

        #region Helper Methods Region
        private Dictionary<int, string> ParseIndex(string folder)
        {
            Dictionary<int, string> fileIndexes = new Dictionary<int, string>();
            foreach (var i in indexTextBox.Text.Split('\r'))
            {
                var index = i.Trim('\n');
                if (index.ToUpper().Equals(SLUS_NAME))
                {
                    fileIndexes.Add(SLUS_INDEX, folder + "\\" + SLUS_NAME + ".BIN");
                }
                else
                {
                    if (index.Length < 1) continue;
                    fileIndexes.Add(Int32.Parse(index), folder + "\\" + index + ".BIN");
                }
            }
            return fileIndexes;
        }
        
        private string ValidateFilename(ref DragEventArgs e)
        {
            string filename = (e.Data.GetData(DataFormats.FileDrop) as string[])[0];
            if (filename.Contains(".bin") || filename.Contains(".BIN")) return filename;
            return "";
        }
        #endregion
    }

}