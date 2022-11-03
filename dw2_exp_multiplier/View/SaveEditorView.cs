using System;
using System.IO;
using System.Windows.Forms;
using dw2_exp_multiplier.Entity;


namespace dw2_exp_multiplier.View
{
    public partial class SaveEditorView : UserControl
    {
        private SaveFile saveFile;
        public SaveEditorView()
        {
            InitializeComponent();
            saveFileTextBox.Text = "BASLUS-01193 DMW2";
            this.saveFile = new SaveFile(File.ReadAllBytes(saveFileTextBox.Text));
            this.slotComboBox.SelectedIndex = 0;
            this.LoadSaveFile();
        }

        private void saveFileButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Open DW2 Raw Save File";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                saveFileTextBox.Text = ofd.FileName;
                this.saveFile = new SaveFile(File.ReadAllBytes(saveFileTextBox.Text));
                this.slotComboBox.SelectedIndex = 0;
                this.LoadSaveFile();
            }
        }

        private void LoadSaveFile()
        {
            var currentSlot = this.saveFile.saveSlot[this.slotComboBox.SelectedIndex];
            lastLocationTextBox.Text = currentSlot.locationId1.ToString("X2");
            heroNameTextBox.Text = currentSlot.stringHeroName;
            time1TextBox.Text = currentSlot.time1.ToString("X2");
            time2TextBox.Text = currentSlot.time2.ToString("X2");
            time3TextBox.Text = currentSlot.time3.ToString("X2");
            time4TextBox.Text = currentSlot.time4.ToString("X2");
            bitsTextBox.Text = currentSlot.bits.ToString();
            rankTextBox.Text = currentSlot.rank.ToString("X2");
        }

        private void slotComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadSaveFile();
        }

        private void saveFileButton_Click_1(object sender, EventArgs e)
        {
            File.WriteAllBytes("test", this.saveFile.ToArray());
        }
        
    }
    
}
