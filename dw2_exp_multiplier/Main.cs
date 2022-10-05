using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

/*
 * @author acemon33
 */

namespace dw2_exp_multiplier
{
    public partial class Main : Form
    {
        List<Enemyset> EnemysetList = new List<Enemyset>();
        
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
                EnemysetManager.ReadBin(enemysetTextBox.Text, ref this.EnemysetList);
                // EnemysetManager.MultiplyExpBits(Convert.ToUInt16(multiplier.Value), ref this.EnemysetList);
                EnemysetManager.WriteFile(dw2TextBox.Text, ref this.EnemysetList);

                MessageBox.Show("The file has been Saved Successfully");
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("The File \"" + dw2TextBox.Text + "\" is not found", "File Error");
            }
            catch (IOException ex)
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
