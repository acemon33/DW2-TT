using System.ComponentModel;

namespace dw2_exp_multiplier.View
{
    partial class MiscView
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
            this.battlePatchesGroupBox = new System.Windows.Forms.GroupBox();
            this.battleFixPatcherCheckBox = new System.Windows.Forms.CheckBox();
            this.battleEnhancementPatcherCheckBox = new System.Windows.Forms.CheckBox();
            this.noEncounterPatcherCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.digiLinePatcherCheckBox = new System.Windows.Forms.CheckBox();
            this.dnaLabsPatcherCheckBox = new System.Windows.Forms.CheckBox();
            this.digimonGiftPatcherCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.multiplierLabel = new System.Windows.Forms.Label();
            this.multiplier = new System.Windows.Forms.NumericUpDown();
            this.EnemyBossGroupBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.extremeModeCheckBox = new System.Windows.Forms.CheckBox();
            this.bossMultiplier = new System.Windows.Forms.NumericUpDown();
            this.dw2Label = new System.Windows.Forms.Label();
            this.digibeetleGroupBox = new System.Windows.Forms.GroupBox();
            this.digibeetlPictureBox = new System.Windows.Forms.PictureBox();
            this.digibeetleLabel = new System.Windows.Forms.Label();
            this.digibeetleComboBox = new System.Windows.Forms.ComboBox();
            this.dw2BrowseButton = new System.Windows.Forms.Button();
            this.dw2TextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.unHideAAA = new System.Windows.Forms.CheckBox();
            this.enemysetLabel = new System.Windows.Forms.Label();
            this.exportButton = new System.Windows.Forms.Button();
            this.enemysetTextBox = new System.Windows.Forms.TextBox();
            this.importButton = new System.Windows.Forms.Button();
            this.enemysetBrowseButton = new System.Windows.Forms.Button();
            this.battlePatchesGroupBox.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.multiplier)).BeginInit();
            this.EnemyBossGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bossMultiplier)).BeginInit();
            this.digibeetleGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.digibeetlPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // battlePatchesGroupBox
            // 
            this.battlePatchesGroupBox.Controls.Add(this.battleFixPatcherCheckBox);
            this.battlePatchesGroupBox.Controls.Add(this.battleEnhancementPatcherCheckBox);
            this.battlePatchesGroupBox.Enabled = false;
            this.battlePatchesGroupBox.Location = new System.Drawing.Point(0, 228);
            this.battlePatchesGroupBox.Name = "battlePatchesGroupBox";
            this.battlePatchesGroupBox.Size = new System.Drawing.Size(200, 121);
            this.battlePatchesGroupBox.TabIndex = 47;
            this.battlePatchesGroupBox.TabStop = false;
            this.battlePatchesGroupBox.Text = "Battle Patches";
            // 
            // battleFixPatcherCheckBox
            // 
            this.battleFixPatcherCheckBox.Location = new System.Drawing.Point(9, 24);
            this.battleFixPatcherCheckBox.Name = "battleFixPatcherCheckBox";
            this.battleFixPatcherCheckBox.Size = new System.Drawing.Size(105, 24);
            this.battleFixPatcherCheckBox.TabIndex = 23;
            this.battleFixPatcherCheckBox.Text = "Fix";
            this.battleFixPatcherCheckBox.UseVisualStyleBackColor = true;
            this.battleFixPatcherCheckBox.MouseHover += new System.EventHandler(this.battleFixPatcherCheckBox_MouseHover);
            // 
            // battleEnhancementPatcherCheckBox
            // 
            this.battleEnhancementPatcherCheckBox.Location = new System.Drawing.Point(9, 54);
            this.battleEnhancementPatcherCheckBox.Name = "battleEnhancementPatcherCheckBox";
            this.battleEnhancementPatcherCheckBox.Size = new System.Drawing.Size(105, 24);
            this.battleEnhancementPatcherCheckBox.TabIndex = 23;
            this.battleEnhancementPatcherCheckBox.Text = "Enhancement";
            this.battleEnhancementPatcherCheckBox.UseVisualStyleBackColor = true;
            this.battleEnhancementPatcherCheckBox.MouseHover += new System.EventHandler(this.battleEnhancementPatcherCheckBox_MouseHover);
            // 
            // noEncounterPatcherCheckBox
            // 
            this.noEncounterPatcherCheckBox.Enabled = false;
            this.noEncounterPatcherCheckBox.Location = new System.Drawing.Point(239, 355);
            this.noEncounterPatcherCheckBox.Name = "noEncounterPatcherCheckBox";
            this.noEncounterPatcherCheckBox.Size = new System.Drawing.Size(187, 24);
            this.noEncounterPatcherCheckBox.TabIndex = 48;
            this.noEncounterPatcherCheckBox.Text = "No Enemy Encounter";
            this.noEncounterPatcherCheckBox.UseVisualStyleBackColor = true;
            this.noEncounterPatcherCheckBox.MouseHover += new System.EventHandler(this.noEncounterPatcherCheckBox_MouseHover);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.digiLinePatcherCheckBox);
            this.groupBox5.Controls.Add(this.dnaLabsPatcherCheckBox);
            this.groupBox5.Controls.Add(this.digimonGiftPatcherCheckBox);
            this.groupBox5.Enabled = false;
            this.groupBox5.Location = new System.Drawing.Point(233, 228);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 121);
            this.groupBox5.TabIndex = 45;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Misc. Patches";
            // 
            // digiLinePatcherCheckBox
            // 
            this.digiLinePatcherCheckBox.Location = new System.Drawing.Point(15, 44);
            this.digiLinePatcherCheckBox.Name = "digiLinePatcherCheckBox";
            this.digiLinePatcherCheckBox.Size = new System.Drawing.Size(125, 24);
            this.digiLinePatcherCheckBox.TabIndex = 23;
            this.digiLinePatcherCheckBox.Text = "Digi-Line";
            this.digiLinePatcherCheckBox.UseVisualStyleBackColor = true;
            this.digiLinePatcherCheckBox.MouseHover += new System.EventHandler(this.digiLinePatcherCheckBox_MouseHover);
            // 
            // dnaLabsPatcherCheckBox
            // 
            this.dnaLabsPatcherCheckBox.Location = new System.Drawing.Point(15, 16);
            this.dnaLabsPatcherCheckBox.Name = "dnaLabsPatcherCheckBox";
            this.dnaLabsPatcherCheckBox.Size = new System.Drawing.Size(125, 24);
            this.dnaLabsPatcherCheckBox.TabIndex = 23;
            this.dnaLabsPatcherCheckBox.Text = "DNA Labs";
            this.dnaLabsPatcherCheckBox.UseVisualStyleBackColor = true;
            this.dnaLabsPatcherCheckBox.MouseHover += new System.EventHandler(this.dnaLabsPatcherCheckBox_MouseHover);
            // 
            // digimonGiftPatcherCheckBox
            // 
            this.digimonGiftPatcherCheckBox.Location = new System.Drawing.Point(15, 74);
            this.digimonGiftPatcherCheckBox.Name = "digimonGiftPatcherCheckBox";
            this.digimonGiftPatcherCheckBox.Size = new System.Drawing.Size(125, 24);
            this.digimonGiftPatcherCheckBox.TabIndex = 23;
            this.digimonGiftPatcherCheckBox.Text = "Digimon Gift";
            this.digimonGiftPatcherCheckBox.UseVisualStyleBackColor = true;
            this.digimonGiftPatcherCheckBox.MouseHover += new System.EventHandler(this.digimonGiftPatcherCheckBox_MouseHover);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.multiplierLabel);
            this.groupBox2.Controls.Add(this.multiplier);
            this.groupBox2.Location = new System.Drawing.Point(1, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 55);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Exp / Bits Multiplier";
            // 
            // multiplierLabel
            // 
            this.multiplierLabel.AutoSize = true;
            this.multiplierLabel.Location = new System.Drawing.Point(10, 27);
            this.multiplierLabel.Name = "multiplierLabel";
            this.multiplierLabel.Size = new System.Drawing.Size(85, 13);
            this.multiplierLabel.TabIndex = 10;
            this.multiplierLabel.Text = "Multiply By       x";
            // 
            // multiplier
            // 
            this.multiplier.DecimalPlaces = 1;
            this.multiplier.Enabled = false;
            this.multiplier.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.multiplier.Location = new System.Drawing.Point(96, 25);
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
            // EnemyBossGroupBox
            // 
            this.EnemyBossGroupBox.Controls.Add(this.label1);
            this.EnemyBossGroupBox.Controls.Add(this.extremeModeCheckBox);
            this.EnemyBossGroupBox.Controls.Add(this.bossMultiplier);
            this.EnemyBossGroupBox.Location = new System.Drawing.Point(1, 143);
            this.EnemyBossGroupBox.Name = "EnemyBossGroupBox";
            this.EnemyBossGroupBox.Size = new System.Drawing.Size(200, 79);
            this.EnemyBossGroupBox.TabIndex = 41;
            this.EnemyBossGroupBox.TabStop = false;
            this.EnemyBossGroupBox.Text = "Enemy Boss Stats Multiplier";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Multiply By       x";
            // 
            // extremeModeCheckBox
            // 
            this.extremeModeCheckBox.Enabled = false;
            this.extremeModeCheckBox.Location = new System.Drawing.Point(13, 53);
            this.extremeModeCheckBox.Name = "extremeModeCheckBox";
            this.extremeModeCheckBox.Size = new System.Drawing.Size(134, 24);
            this.extremeModeCheckBox.TabIndex = 23;
            this.extremeModeCheckBox.Text = "Extreme Mode";
            this.extremeModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // bossMultiplier
            // 
            this.bossMultiplier.DecimalPlaces = 1;
            this.bossMultiplier.Enabled = false;
            this.bossMultiplier.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.bossMultiplier.Location = new System.Drawing.Point(96, 27);
            this.bossMultiplier.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.bossMultiplier.Name = "bossMultiplier";
            this.bossMultiplier.Size = new System.Drawing.Size(46, 20);
            this.bossMultiplier.TabIndex = 21;
            this.bossMultiplier.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dw2Label
            // 
            this.dw2Label.AutoSize = true;
            this.dw2Label.Location = new System.Drawing.Point(3, 9);
            this.dw2Label.Name = "dw2Label";
            this.dw2Label.Size = new System.Drawing.Size(47, 13);
            this.dw2Label.TabIndex = 31;
            this.dw2Label.Text = "DW2 Bin";
            // 
            // digibeetleGroupBox
            // 
            this.digibeetleGroupBox.Controls.Add(this.digibeetlPictureBox);
            this.digibeetleGroupBox.Controls.Add(this.digibeetleLabel);
            this.digibeetleGroupBox.Controls.Add(this.digibeetleComboBox);
            this.digibeetleGroupBox.Location = new System.Drawing.Point(233, 82);
            this.digibeetleGroupBox.Name = "digibeetleGroupBox";
            this.digibeetleGroupBox.Size = new System.Drawing.Size(201, 140);
            this.digibeetleGroupBox.TabIndex = 40;
            this.digibeetleGroupBox.TabStop = false;
            this.digibeetleGroupBox.Text = "Digi-Beetle";
            // 
            // digibeetlPictureBox
            // 
            this.digibeetlPictureBox.Location = new System.Drawing.Point(67, 44);
            this.digibeetlPictureBox.Name = "digibeetlPictureBox";
            this.digibeetlPictureBox.Size = new System.Drawing.Size(90, 90);
            this.digibeetlPictureBox.TabIndex = 19;
            this.digibeetlPictureBox.TabStop = false;
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
            // 
            // dw2BrowseButton
            // 
            this.dw2BrowseButton.Location = new System.Drawing.Point(230, 6);
            this.dw2BrowseButton.Name = "dw2BrowseButton";
            this.dw2BrowseButton.Size = new System.Drawing.Size(64, 23);
            this.dw2BrowseButton.TabIndex = 30;
            this.dw2BrowseButton.Text = "Browse";
            this.dw2BrowseButton.UseVisualStyleBackColor = true;
            this.dw2BrowseButton.Click += new System.EventHandler(this.dw2BrowseButton_Click);
            // 
            // dw2TextBox
            // 
            this.dw2TextBox.AllowDrop = true;
            this.dw2TextBox.Location = new System.Drawing.Point(97, 6);
            this.dw2TextBox.Name = "dw2TextBox";
            this.dw2TextBox.Size = new System.Drawing.Size(127, 20);
            this.dw2TextBox.TabIndex = 32;
            this.dw2TextBox.TextChanged += new System.EventHandler(this.dw2TextBox_TextChanged);
            this.dw2TextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.dw2TextBox_DragDrop);
            this.dw2TextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.dw2TextBox_DragOver);
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(300, 6);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(134, 23);
            this.saveButton.TabIndex = 33;
            this.saveButton.Text = "Apply && Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // unHideAAA
            // 
            this.unHideAAA.Enabled = false;
            this.unHideAAA.Location = new System.Drawing.Point(239, 385);
            this.unHideAAA.Name = "unHideAAA";
            this.unHideAAA.Size = new System.Drawing.Size(134, 24);
            this.unHideAAA.TabIndex = 39;
            this.unHideAAA.Text = "Unhide AAA Folder";
            this.unHideAAA.UseVisualStyleBackColor = true;
            // 
            // enemysetLabel
            // 
            this.enemysetLabel.AutoSize = true;
            this.enemysetLabel.Location = new System.Drawing.Point(3, 38);
            this.enemysetLabel.Name = "enemysetLabel";
            this.enemysetLabel.Size = new System.Drawing.Size(78, 13);
            this.enemysetLabel.TabIndex = 34;
            this.enemysetLabel.Text = "ENEMYSET BIN";
            // 
            // exportButton
            // 
            this.exportButton.Enabled = false;
            this.exportButton.Location = new System.Drawing.Point(370, 33);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(64, 23);
            this.exportButton.TabIndex = 38;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // enemysetTextBox
            // 
            this.enemysetTextBox.AllowDrop = true;
            this.enemysetTextBox.Location = new System.Drawing.Point(96, 35);
            this.enemysetTextBox.Name = "enemysetTextBox";
            this.enemysetTextBox.Size = new System.Drawing.Size(128, 20);
            this.enemysetTextBox.TabIndex = 35;
            this.enemysetTextBox.TextChanged += new System.EventHandler(this.enemysetTextBox_TextChanged);
            this.enemysetTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.enemysetTextBox_DragDrop);
            this.enemysetTextBox.DragOver += new System.Windows.Forms.DragEventHandler(this.enemysetTextBox_DragOver);
            // 
            // importButton
            // 
            this.importButton.Enabled = false;
            this.importButton.Location = new System.Drawing.Point(300, 33);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(64, 23);
            this.importButton.TabIndex = 37;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // enemysetBrowseButton
            // 
            this.enemysetBrowseButton.Location = new System.Drawing.Point(230, 33);
            this.enemysetBrowseButton.Name = "enemysetBrowseButton";
            this.enemysetBrowseButton.Size = new System.Drawing.Size(64, 23);
            this.enemysetBrowseButton.TabIndex = 36;
            this.enemysetBrowseButton.Text = "Browse";
            this.enemysetBrowseButton.UseVisualStyleBackColor = true;
            this.enemysetBrowseButton.Click += new System.EventHandler(this.enemysetBrowseButton_Click);
            // 
            // MiscView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.battlePatchesGroupBox);
            this.Controls.Add(this.noEncounterPatcherCheckBox);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.EnemyBossGroupBox);
            this.Controls.Add(this.dw2Label);
            this.Controls.Add(this.digibeetleGroupBox);
            this.Controls.Add(this.dw2BrowseButton);
            this.Controls.Add(this.dw2TextBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.unHideAAA);
            this.Controls.Add(this.enemysetLabel);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.enemysetTextBox);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.enemysetBrowseButton);
            this.Name = "MiscView";
            this.Size = new System.Drawing.Size(600, 468);
            this.battlePatchesGroupBox.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.multiplier)).EndInit();
            this.EnemyBossGroupBox.ResumeLayout(false);
            this.EnemyBossGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bossMultiplier)).EndInit();
            this.digibeetleGroupBox.ResumeLayout(false);
            this.digibeetleGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.digibeetlPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.GroupBox battlePatchesGroupBox;
        private System.Windows.Forms.CheckBox battleFixPatcherCheckBox;
        private System.Windows.Forms.CheckBox noEncounterPatcherCheckBox;
        private System.Windows.Forms.CheckBox battleEnhancementPatcherCheckBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox digiLinePatcherCheckBox;
        private System.Windows.Forms.CheckBox digimonGiftPatcherCheckBox;
        private System.Windows.Forms.CheckBox dnaLabsPatcherCheckBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label multiplierLabel;
        private System.Windows.Forms.NumericUpDown multiplier;
        private System.Windows.Forms.GroupBox EnemyBossGroupBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox extremeModeCheckBox;
        private System.Windows.Forms.NumericUpDown bossMultiplier;
        private System.Windows.Forms.Label dw2Label;
        private System.Windows.Forms.GroupBox digibeetleGroupBox;
        private System.Windows.Forms.PictureBox digibeetlPictureBox;
        private System.Windows.Forms.Label digibeetleLabel;
        private System.Windows.Forms.ComboBox digibeetleComboBox;
        private System.Windows.Forms.Button dw2BrowseButton;
        private System.Windows.Forms.TextBox dw2TextBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.CheckBox unHideAAA;
        private System.Windows.Forms.Label enemysetLabel;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.TextBox enemysetTextBox;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button enemysetBrowseButton;

        #endregion
    }
}