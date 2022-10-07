using System;
using System.Collections.Generic;
using System.Drawing;
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
        Dictionary<string, Bitmap> imageList = new Dictionary<string, Bitmap>();
        
        public Main()
        {
            InitializeComponent();
            this.stmapdatComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            digibeetleComboBox.DisplayMember = "Value";
            digibeetleComboBox.ValueMember = "Key";
            digibeetleComboBox.DataSource = new BindingSource(DigiBeetlePatcher.getDigiBeetleIds(ref this.imageList, "digi-beetle.zip"), null);
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
                multiplier.Enabled = true;
            }
            else
            {
                saveButton.Enabled = false;
                importButton.Enabled = false;
                multiplier.Enabled = false;
            }
        }

        private void dw2TextBox_TextChanged(object sender, EventArgs e)
        {
            this.checkSaveButton();
            if (dw2TextBox.Text.Length > 0)
            {
                exportButton.Enabled = true;
                unHideAAA.Enabled = true;
                stmapdatComboBox.Enabled = true;
                digibeetleComboBox.Enabled = true;
            }
            else
            {
                exportButton.Enabled = false;
                unHideAAA.Enabled = false;
                stmapdatComboBox.Enabled = false;
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
            if (filename.Contains(".bin") || filename.Contains(".BIN"))
            {
                return filename;
            }
            return "";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.EnemysetList.Clear();
            if (!DW2Slus.ValidImageFile(dw2TextBox.Text))
            {
                MessageBox.Show("The file: \"" + dw2TextBox.Text + "\" is not Digimon World 2 Image File!!", "DW2 invalid file");
                return;
            }
            if (!EnemysetManager.ReadFile(enemysetTextBox.Text, ref this.EnemysetList)) return;
            EnemysetManager.MultiplyExpBits(Convert.ToUInt16(multiplier.Value), ref this.EnemysetList);
            if (Control.ModifierKeys == Keys.Control)
            {
                if (!EnemysetManager.WriteFile("ENEMYSET_TEST.BIN", ref this.EnemysetList)) return;
            }
            else
            {
                if (!EnemysetManager.WriteBin(dw2TextBox.Text, ref this.EnemysetList)) return;
            }
            if (unHideAAA.Checked)
            {
                bool hide = (Control.ModifierKeys == Keys.Shift) ? true: false;
                DW2Slus.UnhideAAAFolder(dw2TextBox.Text, hide);
            }
            if (stmapdatComboBox.SelectedIndex > 0)
            {
                byte[] data = Stmapdat.Read(stmapdatComboBox.SelectedIndex);
                Stmapdat.WriteBin(dw2TextBox.Text, ref data);
            }
            if (digibeetleComboBox.SelectedIndex > 0)
            {
                ushort id = Convert.ToUInt16(((KeyValuePair<string, string>)digibeetleComboBox.SelectedItem).Key, 16);
                DigiBeetlePatcher.patch(dw2TextBox.Text, id);
            }
            MessageBox.Show("The file has been Saved Successfully");
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            this.EnemysetList.Clear();
            if (!DW2Slus.ValidImageFile(dw2TextBox.Text))
            {
                MessageBox.Show("The file: \"" + dw2TextBox.Text + "\" is not Digimon World 2 Image File!!", "DW2 invalid file");
                return;
            }
            if (!EnemysetManager.ReadBin(dw2TextBox.Text, ref this.EnemysetList)) return;
            string filename = Path.GetDirectoryName(dw2TextBox.Text)+ "\\ENEMYSET.BIN";
            if (!EnemysetManager.WriteFile(filename, ref this.EnemysetList)) return;
            enemysetTextBox.Text = filename;
            MessageBox.Show("ENEMYSET.BIN has been Exported Successfully");
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            this.EnemysetList.Clear();
            if (!DW2Slus.ValidImageFile(dw2TextBox.Text))
            {
                MessageBox.Show("The file: \"" + dw2TextBox.Text + "\" is not Digimon World 2 Image File!!", "DW2 invalid file");
                return;
            }
            if (!EnemysetManager.ReadFile(enemysetTextBox.Text, ref this.EnemysetList)) return;
            if (!EnemysetManager.WriteBin(dw2TextBox.Text, ref this.EnemysetList)) return;
            MessageBox.Show("ENEMYSET.BIN has been Imported Successfully");
        }

        private void digibeetleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = ((KeyValuePair<string, string>)digibeetleComboBox.SelectedItem).Key;
            if (imageList.ContainsKey(id)) this.digibeetlPictureBox.Image = imageList[id];
            else this.digibeetlPictureBox.Image = null;
        }
    }
    
}
