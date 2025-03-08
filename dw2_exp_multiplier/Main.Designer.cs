namespace dw2_exp_multiplier
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuBar = new System.Windows.Forms.ToolStrip();
            this.VersionDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.usVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.japVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveEditorModeMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.vanillaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.improvementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resurrectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alternativeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.faithfulToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.about = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.miscView = new dw2_exp_multiplier.View.MiscView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.saveEditorView = new dw2_exp_multiplier.View.SaveEditorView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.fileManagerView = new dw2_exp_multiplier.View.FileManagerView();
            this.forcePatchingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBar.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VersionDropDownButton,
            this.saveEditorModeMenu,
            this.optionsToolStripDropDownButton,
            this.about});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(460, 25);
            this.menuBar.TabIndex = 11;
            this.menuBar.Text = "MenuBar";
            // 
            // VersionDropDownButton
            // 
            this.VersionDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.VersionDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usVersionToolStripMenuItem,
            this.japVersionToolStripMenuItem});
            this.VersionDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("VersionDropDownButton.Image")));
            this.VersionDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.VersionDropDownButton.Name = "VersionDropDownButton";
            this.VersionDropDownButton.Size = new System.Drawing.Size(58, 22);
            this.VersionDropDownButton.Text = "Version";
            // 
            // usVersionToolStripMenuItem
            // 
            this.usVersionToolStripMenuItem.Checked = true;
            this.usVersionToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.usVersionToolStripMenuItem.Name = "usVersionToolStripMenuItem";
            this.usVersionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.usVersionToolStripMenuItem.Text = "US Version";
            this.usVersionToolStripMenuItem.Click += new System.EventHandler(this.usVersionToolStripMenuItem_Click);
            // 
            // japVersionToolStripMenuItem
            // 
            this.japVersionToolStripMenuItem.Name = "japVersionToolStripMenuItem";
            this.japVersionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.japVersionToolStripMenuItem.Text = "JAP Version";
            this.japVersionToolStripMenuItem.Click += new System.EventHandler(this.japVersionToolStripMenuItem_Click);
            // 
            // saveEditorModeMenu
            // 
            this.saveEditorModeMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveEditorModeMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vanillaToolStripMenuItem,
            this.improvementToolStripMenuItem,
            this.resurrectionToolStripMenuItem,
            this.alternativeToolStripMenuItem,
            this.faithfulToolStripMenuItem});
            this.saveEditorModeMenu.Image = ((System.Drawing.Image)(resources.GetObject("saveEditorModeMenu.Image")));
            this.saveEditorModeMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveEditorModeMenu.Name = "saveEditorModeMenu";
            this.saveEditorModeMenu.Size = new System.Drawing.Size(112, 22);
            this.saveEditorModeMenu.Text = "Save Editor Mode";
            // 
            // vanillaToolStripMenuItem
            // 
            this.vanillaToolStripMenuItem.Checked = true;
            this.vanillaToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vanillaToolStripMenuItem.Name = "vanillaToolStripMenuItem";
            this.vanillaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.vanillaToolStripMenuItem.Text = "Vanilla / Hard";
            this.vanillaToolStripMenuItem.Click += new System.EventHandler(this.vanillaToolStripMenuItem_Click);
            // 
            // improvementToolStripMenuItem
            // 
            this.improvementToolStripMenuItem.Name = "improvementToolStripMenuItem";
            this.improvementToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.improvementToolStripMenuItem.Text = "Improvement";
            this.improvementToolStripMenuItem.Click += new System.EventHandler(this.improvementToolStripMenuItem_Click);
            // 
            // resurrectionToolStripMenuItem
            // 
            this.resurrectionToolStripMenuItem.Name = "resurrectionToolStripMenuItem";
            this.resurrectionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.resurrectionToolStripMenuItem.Text = "Resurrection";
            this.resurrectionToolStripMenuItem.Click += new System.EventHandler(this.resurrectionToolStripMenuItem_Click);
            // 
            // alternativeToolStripMenuItem
            // 
            this.alternativeToolStripMenuItem.Name = "alternativeToolStripMenuItem";
            this.alternativeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.alternativeToolStripMenuItem.Text = "Alternative";
            this.alternativeToolStripMenuItem.Click += new System.EventHandler(this.alternativeToolStripMenuItem_Click);
            // 
            // faithfulToolStripMenuItem
            // 
            this.faithfulToolStripMenuItem.Name = "faithfulToolStripMenuItem";
            this.faithfulToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.faithfulToolStripMenuItem.Text = "Faithful";
            this.faithfulToolStripMenuItem.Click += new System.EventHandler(this.faithfulToolStripMenuItem_Click);
            // 
            // optionsToolStripDropDownButton
            // 
            this.optionsToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.optionsToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupToolStripMenuItem,
            this.restoreToolStripMenuItem,
            this.fileManagerToolStripMenuItem,
            this.forcePatchingToolStripMenuItem});
            this.optionsToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optionsToolStripDropDownButton.Name = "optionsToolStripDropDownButton";
            this.optionsToolStripDropDownButton.Size = new System.Drawing.Size(62, 22);
            this.optionsToolStripDropDownButton.Text = "Options";
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Enabled = false;
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.backupToolStripMenuItem.Text = "Backup";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Enabled = false;
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // fileManagerToolStripMenuItem
            // 
            this.fileManagerToolStripMenuItem.CheckOnClick = true;
            this.fileManagerToolStripMenuItem.Name = "fileManagerToolStripMenuItem";
            this.fileManagerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fileManagerToolStripMenuItem.Text = "File Manager";
            this.fileManagerToolStripMenuItem.Click += new System.EventHandler(this.fileManagerToolStripMenuItem_Click);
            // 
            // about
            // 
            this.about.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.about.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(44, 22);
            this.about.Text = "About";
            this.about.ToolTipText = "About";
            this.about.Click += new System.EventHandler(this.about_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(461, 439);
            this.tabControl1.TabIndex = 21;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.miscView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(453, 413);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Misc.";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // miscView
            // 
            this.miscView.Location = new System.Drawing.Point(3, 3);
            this.miscView.Name = "miscView";
            this.miscView.Size = new System.Drawing.Size(447, 410);
            this.miscView.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.saveEditorView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(453, 413);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Save Editor";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // saveEditorView
            // 
            this.saveEditorView.Location = new System.Drawing.Point(3, 5);
            this.saveEditorView.Name = "saveEditorView";
            this.saveEditorView.Size = new System.Drawing.Size(444, 402);
            this.saveEditorView.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.fileManagerView);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(453, 413);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "File Manager";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // fileManagerView
            // 
            this.fileManagerView.Location = new System.Drawing.Point(6, 6);
            this.fileManagerView.Name = "fileManagerView";
            this.fileManagerView.Size = new System.Drawing.Size(441, 401);
            this.fileManagerView.TabIndex = 0;
            // 
            // forcePatchingToolStripMenuItem
            // 
            this.forcePatchingToolStripMenuItem.CheckOnClick = true;
            this.forcePatchingToolStripMenuItem.Name = "forcePatchingToolStripMenuItem";
            this.forcePatchingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.forcePatchingToolStripMenuItem.Text = "Force Patching";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 470);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Tag = "";
            this.Text = "DW2-TT";
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStrip menuBar;
        private System.Windows.Forms.ToolStripButton about;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripDropDownButton saveEditorModeMenu;
        private System.Windows.Forms.ToolStripMenuItem vanillaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alternativeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem improvementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resurrectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem faithfulToolStripMenuItem;
        private dw2_exp_multiplier.View.SaveEditorView saveEditorView;
        private dw2_exp_multiplier.View.FileManagerView fileManagerView;
        private dw2_exp_multiplier.View.MiscView miscView;
        private System.Windows.Forms.ToolStripDropDownButton optionsToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton VersionDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem usVersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem japVersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forcePatchingToolStripMenuItem;
    }
}

