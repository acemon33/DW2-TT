using System;
using System.Windows.Forms;
using dw2_exp_multiplier.View;


namespace dw2_exp_multiplier
{
    public partial class Main : Form
    {
        #region Fields Region
        public static string Title;
        public static ToolTip HintToolTip;
        #endregion

        public Main()
        {
            InitializeComponent();
            Main.Title = this.Text;
            HintToolTip = new ToolTip();
            if (!(Control.ModifierKeys == Keys.Shift))
                tabControl1.TabPages.RemoveByKey("tabPage3");
        }

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
