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
            this.dw2BrowseButton = new System.Windows.Forms.Button();
            this.dw2Label = new System.Windows.Forms.Label();
            this.dw2TextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.enemysetTextBox = new System.Windows.Forms.TextBox();
            this.enemysetLabel = new System.Windows.Forms.Label();
            this.enemysetBrowseButton = new System.Windows.Forms.Button();
            this.multiplier = new System.Windows.Forms.NumericUpDown();
            this.multiplierLabel = new System.Windows.Forms.Label();
            this.menuBar = new System.Windows.Forms.ToolStrip();
            this.about = new System.Windows.Forms.ToolStripButton();
            this.importButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.unHideAAA = new System.Windows.Forms.CheckBox();
            this.digibeetleLabel = new System.Windows.Forms.Label();
            this.digibeetleComboBox = new System.Windows.Forms.ComboBox();
            this.digibeetleGroupBox = new System.Windows.Forms.GroupBox();
            this.digibeetlPictureBox = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.multiplier)).BeginInit();
            this.menuBar.SuspendLayout();
            this.digibeetleGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.digibeetlPictureBox)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dw2BrowseButton
            // 
            this.dw2BrowseButton.Location = new System.Drawing.Point(243, 14);
            this.dw2BrowseButton.Name = "dw2BrowseButton";
            this.dw2BrowseButton.Size = new System.Drawing.Size(64, 23);
            this.dw2BrowseButton.TabIndex = 0;
            this.dw2BrowseButton.Text = "Browse";
            this.dw2BrowseButton.UseVisualStyleBackColor = true;
            this.dw2BrowseButton.Click += new System.EventHandler(this.dw2BrowseButton_Click);
            // 
            // dw2Label
            // 
            this.dw2Label.AutoSize = true;
            this.dw2Label.Location = new System.Drawing.Point(16, 17);
            this.dw2Label.Name = "dw2Label";
            this.dw2Label.Size = new System.Drawing.Size(47, 13);
            this.dw2Label.TabIndex = 1;
            this.dw2Label.Text = "DW2 Bin";
            // 
            // dw2TextBox
            // 
            this.dw2TextBox.AllowDrop = true;
            this.dw2TextBox.Location = new System.Drawing.Point(102, 14);
            this.dw2TextBox.Name = "dw2TextBox";
            this.dw2TextBox.Size = new System.Drawing.Size(135, 20);
            this.dw2TextBox.TabIndex = 2;
            this.dw2TextBox.TextChanged += new System.EventHandler(this.dw2TextBox_TextChanged);
            this.dw2TextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.dw2TextBox_DragDrop);
            this.dw2TextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.dw2TextBox_DragOver);
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(313, 14);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(134, 23);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Apply && Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // enemysetTextBox
            // 
            this.enemysetTextBox.AllowDrop = true;
            this.enemysetTextBox.Location = new System.Drawing.Point(102, 43);
            this.enemysetTextBox.Name = "enemysetTextBox";
            this.enemysetTextBox.Size = new System.Drawing.Size(135, 20);
            this.enemysetTextBox.TabIndex = 6;
            this.enemysetTextBox.TextChanged += new System.EventHandler(this.enemysetTextBox_TextChanged);
            this.enemysetTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.enemysetTextBox_DragDrop);
            this.enemysetTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.enemysetTextBox_DragOver);
            // 
            // enemysetLabel
            // 
            this.enemysetLabel.AutoSize = true;
            this.enemysetLabel.Location = new System.Drawing.Point(16, 46);
            this.enemysetLabel.Name = "enemysetLabel";
            this.enemysetLabel.Size = new System.Drawing.Size(78, 13);
            this.enemysetLabel.TabIndex = 5;
            this.enemysetLabel.Text = "ENEMYSET BIN";
            // 
            // enemysetBrowseButton
            // 
            this.enemysetBrowseButton.Location = new System.Drawing.Point(243, 41);
            this.enemysetBrowseButton.Name = "enemysetBrowseButton";
            this.enemysetBrowseButton.Size = new System.Drawing.Size(64, 23);
            this.enemysetBrowseButton.TabIndex = 7;
            this.enemysetBrowseButton.Text = "Browse";
            this.enemysetBrowseButton.UseVisualStyleBackColor = true;
            this.enemysetBrowseButton.Click += new System.EventHandler(this.enemysetBrowseButton_Click);
            // 
            // multiplier
            // 
            this.multiplier.DecimalPlaces = 1;
            this.multiplier.Enabled = false;
            this.multiplier.Location = new System.Drawing.Point(102, 90);
            this.multiplier.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.multiplier.Name = "multiplier";
            this.multiplier.Size = new System.Drawing.Size(46, 20);
            this.multiplier.TabIndex = 8;
            this.multiplier.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // multiplierLabel
            // 
            this.multiplierLabel.AutoSize = true;
            this.multiplierLabel.Location = new System.Drawing.Point(16, 92);
            this.multiplierLabel.Name = "multiplierLabel";
            this.multiplierLabel.Size = new System.Drawing.Size(85, 13);
            this.multiplierLabel.TabIndex = 10;
            this.multiplierLabel.Text = "Multiply By       x";
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.about});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(460, 25);
            this.menuBar.TabIndex = 11;
            this.menuBar.Text = "MenuBar";
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
            // importButton
            // 
            this.importButton.Enabled = false;
            this.importButton.Location = new System.Drawing.Point(313, 41);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(64, 23);
            this.importButton.TabIndex = 12;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.Enabled = false;
            this.exportButton.Location = new System.Drawing.Point(383, 41);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(64, 23);
            this.exportButton.TabIndex = 13;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // unHideAAA
            // 
            this.unHideAAA.Enabled = false;
            this.unHideAAA.Location = new System.Drawing.Point(17, 120);
            this.unHideAAA.Name = "unHideAAA";
            this.unHideAAA.Size = new System.Drawing.Size(134, 24);
            this.unHideAAA.TabIndex = 14;
            this.unHideAAA.Text = "Unhide AAA Folder";
            this.unHideAAA.UseVisualStyleBackColor = true;
            // 
            // digibeetleLabel
            // 
            this.digibeetleLabel.AutoSize = true;
            this.digibeetleLabel.Location = new System.Drawing.Point(16, 22);
            this.digibeetleLabel.Name = "digibeetleLabel";
            this.digibeetleLabel.Size = new System.Drawing.Size(18, 13);
            this.digibeetleLabel.TabIndex = 18;
            this.digibeetleLabel.Text = "ID";
            // 
            // digibeetleComboBox
            // 
            this.digibeetleComboBox.Enabled = false;
            this.digibeetleComboBox.FormattingEnabled = true;
            this.digibeetleComboBox.Location = new System.Drawing.Point(40, 19);
            this.digibeetleComboBox.Name = "digibeetleComboBox";
            this.digibeetleComboBox.Size = new System.Drawing.Size(155, 21);
            this.digibeetleComboBox.TabIndex = 17;
            this.digibeetleComboBox.SelectedIndexChanged += new System.EventHandler(this.digibeetleComboBox_SelectedIndexChanged);
            // 
            // digibeetleGroupBox
            // 
            this.digibeetleGroupBox.Controls.Add(this.digibeetlPictureBox);
            this.digibeetleGroupBox.Controls.Add(this.digibeetleLabel);
            this.digibeetleGroupBox.Controls.Add(this.digibeetleComboBox);
            this.digibeetleGroupBox.Location = new System.Drawing.Point(246, 90);
            this.digibeetleGroupBox.Name = "digibeetleGroupBox";
            this.digibeetleGroupBox.Size = new System.Drawing.Size(201, 140);
            this.digibeetleGroupBox.TabIndex = 20;
            this.digibeetleGroupBox.TabStop = false;
            this.digibeetleGroupBox.Text = "Digi-Beetle Patcher";
            // 
            // digibeetlPictureBox
            // 
            this.digibeetlPictureBox.Location = new System.Drawing.Point(67, 44);
            this.digibeetlPictureBox.Name = "digibeetlPictureBox";
            this.digibeetlPictureBox.Size = new System.Drawing.Size(90, 90);
            this.digibeetlPictureBox.TabIndex = 19;
            this.digibeetlPictureBox.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(461, 415);
            this.tabControl1.TabIndex = 21;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dw2Label);
            this.tabPage1.Controls.Add(this.digibeetleGroupBox);
            this.tabPage1.Controls.Add(this.dw2BrowseButton);
            this.tabPage1.Controls.Add(this.dw2TextBox);
            this.tabPage1.Controls.Add(this.saveButton);
            this.tabPage1.Controls.Add(this.unHideAAA);
            this.tabPage1.Controls.Add(this.enemysetLabel);
            this.tabPage1.Controls.Add(this.exportButton);
            this.tabPage1.Controls.Add(this.enemysetTextBox);
            this.tabPage1.Controls.Add(this.importButton);
            this.tabPage1.Controls.Add(this.enemysetBrowseButton);
            this.tabPage1.Controls.Add(this.multiplier);
            this.tabPage1.Controls.Add(this.multiplierLabel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(453, 389);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Misc.";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(453, 389);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Save Editor";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 443);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Tag = "";
            this.Text = "DW2-TT";
            ((System.ComponentModel.ISupportInitialize)(this.multiplier)).EndInit();
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.digibeetleGroupBox.ResumeLayout(false);
            this.digibeetleGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.digibeetlPictureBox)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.PictureBox digibeetlPictureBox;
        private System.Windows.Forms.GroupBox digibeetleGroupBox;

        private System.Windows.Forms.GroupBox groupBox1;

        private System.Windows.Forms.Label digibeetleLabel;
        private System.Windows.Forms.ComboBox digibeetleComboBox;

        private System.Windows.Forms.ComboBox comboBox1;

        private System.Windows.Forms.CheckBox unHideAAA;

        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button exportButton;

        #endregion

        private System.Windows.Forms.Button dw2BrowseButton;
        private System.Windows.Forms.Label dw2Label;
        private System.Windows.Forms.TextBox dw2TextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox enemysetTextBox;
        private System.Windows.Forms.Label enemysetLabel;
        private System.Windows.Forms.Button enemysetBrowseButton;
        private System.Windows.Forms.NumericUpDown multiplier;
        private System.Windows.Forms.Label multiplierLabel;
        private System.Windows.Forms.ToolStrip menuBar;
        private System.Windows.Forms.ToolStripButton about;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private View.SaveEditorView saveEditorView1;
    }
}

