using System.ComponentModel;

namespace dw2_exp_multiplier.View
{
    partial class FileManagerView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.importButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.indexTextBox = new System.Windows.Forms.TextBox();
            this.dw2TextBox = new System.Windows.Forms.TextBox();
            this.dw2BrowseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(113, 53);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(88, 23);
            this.importButton.TabIndex = 5;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(15, 53);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(88, 23);
            this.exportButton.TabIndex = 4;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // indexTextBox
            // 
            this.indexTextBox.Location = new System.Drawing.Point(207, 79);
            this.indexTextBox.Multiline = true;
            this.indexTextBox.Name = "indexTextBox";
            this.indexTextBox.Size = new System.Drawing.Size(88, 158);
            this.indexTextBox.TabIndex = 3;
            // 
            // dw2TextBox
            // 
            this.dw2TextBox.AllowDrop = true;
            this.dw2TextBox.Location = new System.Drawing.Point(15, 24);
            this.dw2TextBox.Name = "dw2TextBox";
            this.dw2TextBox.Size = new System.Drawing.Size(186, 20);
            this.dw2TextBox.TabIndex = 7;
            this.dw2TextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.dw2TextBox_DragDrop);
            this.dw2TextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.dw2TextBox_DragOver);
            // 
            // dw2BrowseButton
            // 
            this.dw2BrowseButton.Location = new System.Drawing.Point(207, 24);
            this.dw2BrowseButton.Name = "dw2BrowseButton";
            this.dw2BrowseButton.Size = new System.Drawing.Size(88, 23);
            this.dw2BrowseButton.TabIndex = 8;
            this.dw2BrowseButton.Text = "Browse";
            this.dw2BrowseButton.UseVisualStyleBackColor = true;
            this.dw2BrowseButton.Click += new System.EventHandler(this.dw2BrowseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Indexes: ";
            // 
            // FileManagerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dw2BrowseButton);
            this.Controls.Add(this.dw2TextBox);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.indexTextBox);
            this.Name = "FileManagerView";
            this.Size = new System.Drawing.Size(392, 309);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button dw2BrowseButton;

        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.TextBox indexTextBox;
        private System.Windows.Forms.TextBox dw2TextBox;

        #endregion

        private System.Windows.Forms.Label label1;
    }
}