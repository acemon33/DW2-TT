using System;
using System.IO;
using System.Windows.Forms;
using dw2_exp_multiplier.Base;

namespace dw2_exp_multiplier.View
{
    public partial class FileManagerView : UserControl
    {
        public FileManagerView()
        {
            InitializeComponent();
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = dw2TextBox.Text;
                FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
                DW2Slus.ValidImageFile(ref fs);
                int index = Int32.Parse(indexTextBox.Text);
                byte[] buffer = File.ReadAllBytes(ofd.FileName);
                PsxSector.WriteSector(ref fs, ref buffer, DW2Slus.GetLba(index), DW2Slus.GetSize(index));
                fs.Close();
                MessageBox.Show("Success");
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = dw2TextBox.Text;
                FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
                DW2Slus.ValidImageFile(ref fs);
                int index = Int32.Parse(indexTextBox.Text);
                byte[] buffer = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(index), DW2Slus.GetSize(index));
                File.WriteAllBytes(ofd.FileName, buffer);
                fs.Close();
                MessageBox.Show("Success");
            }
        }
        
        private void relocateButton_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void dw2BrowseButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Open DW2 Bin File";
            ofd.Filter = "Digimon World 2 File|*.bin";
            if (ofd.ShowDialog() == DialogResult.OK) dw2TextBox.Text = ofd.FileName;
        }
    }

}