using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dw2_exp_multiplier
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void about_Click(object sender, EventArgs e)
        {
            var aboutForm = new About();
            aboutForm.Width = this.Width;
            aboutForm.Height = this.Height;
            aboutForm.ShowDialog();
        }

        private void dw2BrowseButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Open DW2 Bin File";
            ofd.Filter = "Digimon World 2 File|*.bin";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                dw2TextBox.Text = ofd.FileName;
            }
        }

        private void enemysetBrowseButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Open ENEMYSET.BIN File";
            ofd.Filter = "ENEMYSET.BIN File|*.bin";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                enemysetTextBox.Text = ofd.FileName;
            }
        }

        private void checkSaveButton()
        {
            if (dw2TextBox.Text.Length > 0 && enemysetTextBox.Text.Length > 0)
            {
                saveButton.Enabled = true;
                importButton.Enabled = true;
            }
            else
            {
                saveButton.Enabled = false;
                importButton.Enabled = false;
            }
        }

        private void dw2TextBox_TextChanged(object sender, EventArgs e)
        {
            this.checkSaveButton();
            if (dw2TextBox.Text.Length > 0)
            {
                exportButton.Enabled = true;
            }
            else
            {
                exportButton.Enabled = false;
            }
        }

        private void enemysetTextBox_TextChanged(object sender, EventArgs e)
        {
            this.checkSaveButton();
        }

        private void dw2TextBox_DragDrop(object sender, DragEventArgs e)
        {
            dw2TextBox.Text = this.ValidateFilename(e);
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
            enemysetTextBox.Text = this.ValidateFilename(e);
        }

        private string ValidateFilename(DragEventArgs e)
        {
            string filename = (e.Data.GetData(DataFormats.FileDrop) as string[])[0];
            if (filename.Contains(".bin") || filename.Contains(".BIN"))
            {
                return filename;
            }
            return "";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] buffer;
                using (var br = new BinaryReader(File.OpenRead(enemysetTextBox.Text)))
                {
                    var ms = new MemoryStream();
                    br.BaseStream.CopyTo(ms);
                    buffer = ms.ToArray();
                    br.Dispose();
                    br.Close();
                }
                
                using (var bw = new BinaryWriter(File.Create(dw2TextBox.Text)))
                {
                    bw.Write(buffer);
                    bw.Dispose();
                    bw.Close();
                }

                MessageBox.Show("The file has been Saved Successfully");
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("The File \"" + dw2TextBox.Text + "\" is not found", "File Error");
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show("The File \"" + dw2TextBox.Text + "\" is being used by anothe program", "File Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Error");
            }
        }
        
    }

}
