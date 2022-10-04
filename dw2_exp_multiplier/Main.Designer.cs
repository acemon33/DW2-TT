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
            ((System.ComponentModel.ISupportInitialize) (this.multiplier)).BeginInit();
            this.menuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // dw2BrowseButton
            // 
            this.dw2BrowseButton.Location = new System.Drawing.Point(238, 71);
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
            this.dw2Label.Location = new System.Drawing.Point(11, 74);
            this.dw2Label.Name = "dw2Label";
            this.dw2Label.Size = new System.Drawing.Size(47, 13);
            this.dw2Label.TabIndex = 1;
            this.dw2Label.Text = "DW2 Bin";
            // 
            // dw2TextBox
            // 
            this.dw2TextBox.AllowDrop = true;
            this.dw2TextBox.Location = new System.Drawing.Point(97, 71);
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
            this.saveButton.Location = new System.Drawing.Point(308, 71);
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
            this.enemysetTextBox.Location = new System.Drawing.Point(97, 100);
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
            this.enemysetLabel.Location = new System.Drawing.Point(11, 103);
            this.enemysetLabel.Name = "enemysetLabel";
            this.enemysetLabel.Size = new System.Drawing.Size(78, 13);
            this.enemysetLabel.TabIndex = 5;
            this.enemysetLabel.Text = "ENEMYSET BIN";
            // 
            // enemysetBrowseButton
            // 
            this.enemysetBrowseButton.Location = new System.Drawing.Point(238, 98);
            this.enemysetBrowseButton.Name = "enemysetBrowseButton";
            this.enemysetBrowseButton.Size = new System.Drawing.Size(64, 23);
            this.enemysetBrowseButton.TabIndex = 7;
            this.enemysetBrowseButton.Text = "Browse";
            this.enemysetBrowseButton.UseVisualStyleBackColor = true;
            this.enemysetBrowseButton.Click += new System.EventHandler(this.enemysetBrowseButton_Click);
            // 
            // multiplier
            // 
            this.multiplier.Location = new System.Drawing.Point(97, 147);
            this.multiplier.Minimum = new decimal(new int[] {2, 0, 0, 0});
            this.multiplier.Name = "multiplier";
            this.multiplier.Size = new System.Drawing.Size(46, 20);
            this.multiplier.TabIndex = 8;
            this.multiplier.Value = new decimal(new int[] {2, 0, 0, 0});
            // 
            // multiplierLabel
            // 
            this.multiplierLabel.AutoSize = true;
            this.multiplierLabel.Location = new System.Drawing.Point(11, 149);
            this.multiplierLabel.Name = "multiplierLabel";
            this.multiplierLabel.Size = new System.Drawing.Size(85, 13);
            this.multiplierLabel.TabIndex = 10;
            this.multiplierLabel.Text = "Multiply By       x";
            // 
            // menuBar
            // 
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.about});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(448, 25);
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
            this.importButton.Location = new System.Drawing.Point(308, 98);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(64, 23);
            this.importButton.TabIndex = 12;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            // 
            // exportButton
            // 
            this.exportButton.Enabled = false;
            this.exportButton.Location = new System.Drawing.Point(378, 98);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(64, 23);
            this.exportButton.TabIndex = 13;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 182);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.menuBar);
            this.Controls.Add(this.multiplierLabel);
            this.Controls.Add(this.multiplier);
            this.Controls.Add(this.enemysetBrowseButton);
            this.Controls.Add(this.enemysetTextBox);
            this.Controls.Add(this.enemysetLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.dw2TextBox);
            this.Controls.Add(this.dw2Label);
            this.Controls.Add(this.dw2BrowseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "DW2 EXP MULTIPLIER";
            ((System.ComponentModel.ISupportInitialize) (this.multiplier)).EndInit();
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

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
    }
}

