using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using dw2_exp_multiplier.Entity;


namespace dw2_exp_multiplier.View
{
    public partial class SaveEditorView : UserControl
    {
        #region Field Region
        private SaveFile saveFile;
        
        TableLayoutPanel itemTableLayoutPanel = new TableLayoutPanel();
        TableLayoutPanel importantItemTableLayoutPanel = new TableLayoutPanel();
        TableLayoutPanel serverItemTableLayoutPanel = new TableLayoutPanel();
        
        private List<TextBox> itemList = new List<TextBox>();
        private List<TextBox> importantItemList = new List<TextBox>();
        private List<TextBox> serverItemList = new List<TextBox>();
        #endregion
        
        public SaveEditorView()
        {
            InitializeComponent();
            this.LoadForm();
            saveFileTextBox.Text = "BASLUS-01193 DMW2";
            this.saveFile = new SaveFile(File.ReadAllBytes(saveFileTextBox.Text));
            this.slotComboBox.SelectedIndex = 0;
            this.LoadCurrentSlot();
        }

        private void LoadForm()
        {
            int n = 48;
            this.itemPanel.Controls.Add(itemTableLayoutPanel);
            itemTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            itemTableLayoutPanel.Name = "itemTableLayoutPanel";
            itemTableLayoutPanel.Size = new System.Drawing.Size(239, 27 * n + 20);
            itemTableLayoutPanel.ColumnCount = 2;
            for (int i = 0; i < n; i++)
            {
                itemTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                itemTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                var l1 = new Label();
                l1.Text = "Item slot #" + (i + 1);
                var t1 = new TextBox();
                itemTableLayoutPanel.Controls.Add(l1, 0, i);
                itemTableLayoutPanel.Controls.Add(t1, 1, i);
                t1.TextChanged += itemTextBox_TextChanged;
                t1.Tag = i;
                this.itemList.Add(t1);
            }
            itemTableLayoutPanel.RowCount = n;
            for(int i = 0; i < n; i++) itemTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));

            n = 28;
            this.importantItemPanel.Controls.Add(importantItemTableLayoutPanel);
            importantItemTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            importantItemTableLayoutPanel.Name = "importantItemTableLayoutPanel";
            importantItemTableLayoutPanel.Size = new System.Drawing.Size(200, 27 * n + 20);
            importantItemTableLayoutPanel.ColumnCount = 2;
            for (int i = 0; i < n; i++)
            {
                importantItemTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
                importantItemTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                var l1 = new Label();
                l1.Size = new Size(150, l1.Size.Height);
                l1.Text = "Important Item #" + (i + 1);
                var t1 = new TextBox();
                importantItemTableLayoutPanel.Controls.Add(l1, 0, i);
                importantItemTableLayoutPanel.Controls.Add(t1, 1, i);
                t1.TextChanged += importantItemTextBox_TextChanged;
                t1.Tag = i;
                this.importantItemList.Add(t1);
            }
            importantItemTableLayoutPanel.RowCount = n;
            for(int i = 0; i < n; i++) importantItemTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            
            n = 236;
            this.serverItemPanel.Controls.Add(serverItemTableLayoutPanel);
            serverItemTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            serverItemTableLayoutPanel.Name = "serverItemTableLayoutPanel";
            serverItemTableLayoutPanel.Size = new System.Drawing.Size(200, 27 * n + 20);
            serverItemTableLayoutPanel.ColumnCount = 2;
            for (int i = 0; i < n; i++)
            {
                serverItemTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
                serverItemTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                var l1 = new Label();
                l1.Size = new Size(150, l1.Size.Height);
                l1.Text = "Server Item #" + (i + 1);
                var t1 = new TextBox();
                serverItemTableLayoutPanel.Controls.Add(l1, 0, i);
                serverItemTableLayoutPanel.Controls.Add(t1, 1, i);
                t1.TextChanged += serverItemTextBox_TextChanged;
                t1.Tag = i;
                this.serverItemList.Add(t1);
            }
            serverItemTableLayoutPanel.RowCount = n;
            for(int i = 0; i < n; i++) serverItemTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
        }

        #region Buttons Region
        private void openFileButton_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Open DW2 Raw Save File";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                saveFileTextBox.Text = ofd.FileName;
                this.saveFile = new SaveFile(File.ReadAllBytes(saveFileTextBox.Text));
                this.slotComboBox.SelectedIndex = 0;
                this.LoadCurrentSlot();
            }
        }

        private void saveFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllBytes(saveFileTextBox.Text, this.saveFile.ToArray());
                MessageBox.Show("Saved Successfully", Main.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Main.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        
        private void LoadCurrentSlot()
        {
            var currentSlot = this.saveFile.saveSlot[this.slotComboBox.SelectedIndex];
            
                    // Misc.
            lastLocationTextBox.Text = currentSlot.locationId1.ToString("X2");
            heroNameTextBox.Text = currentSlot.StringHeroName;
            time1TextBox.Text = currentSlot.time1.ToString("X2");
            time2TextBox.Text = currentSlot.time2.ToString("X2");
            time3TextBox.Text = currentSlot.time3.ToString("X2");
            time4TextBox.Text = currentSlot.time4.ToString("X2");
            bitsTextBox.Text = currentSlot.bits.ToString();
            rankTextBox.Text = currentSlot.rank.ToString("X2");

                    // Digi-Beetle Parts
            DigiBeetleNameTextBox.Text = currentSlot.StringDigiBeetleName;
            bodyNameTextBox.Text = currentSlot.digi_beetle_part[0].ToString("X4");
            engineNameTextBox.Text = currentSlot.digi_beetle_part[1].ToString("X4");
            memoryNameTextBox.Text = currentSlot.digi_beetle_part[2].ToString("X4");
            batteryNameTextBox.Text = currentSlot.digi_beetle_part[3].ToString("X4");
            toolBoxNameTextBox.Text = currentSlot.digi_beetle_part[4].ToString("X4");
            legNameTextBox.Text = currentSlot.digi_beetle_part[5].ToString("X4");
            armNameTextBox.Text = currentSlot.digi_beetle_part[6].ToString("X4");
            handNameTextBox.Text = currentSlot.digi_beetle_part[7].ToString("X4");
            cannon1NameTextBox.Text = currentSlot.digi_beetle_part[8].ToString("X4");
            cannon2NameTextBox.Text = currentSlot.digi_beetle_part[9].ToString("X4");
            cannon3NameTextBox.Text = currentSlot.digi_beetle_part[10].ToString("X4");
            cannon4NameTextBox.Text = currentSlot.digi_beetle_part[11].ToString("X4");
            cannon5NameTextBox.Text = currentSlot.digi_beetle_part[12].ToString("X4");
            device1NameTextBox.Text = currentSlot.digi_beetle_part[13].ToString("X4");
            device2NameTextBox.Text = currentSlot.digi_beetle_part[14].ToString("X4");
            device3NameTextBox.Text = currentSlot.digi_beetle_part[15].ToString("X4");
            device4NameTextBox.Text = currentSlot.digi_beetle_part[16].ToString("X4");
            device5NameTextBox.Text = currentSlot.digi_beetle_part[17].ToString("X4");
            device6NameTextBox.Text = currentSlot.digi_beetle_part[18].ToString("X4");
            bodyFlagTextBox.Text = currentSlot.digi_beetle_part_flag[0].ToString("X2");
            engineFlagTextBox.Text = currentSlot.digi_beetle_part_flag[1].ToString("X2");
            memoryFlagTextBox.Text = currentSlot.digi_beetle_part_flag[2].ToString("X2");
            batteryFlagTextBox.Text = currentSlot.digi_beetle_part_flag[3].ToString("X2");
            toolBoxFlagTextBox.Text = currentSlot.digi_beetle_part_flag[4].ToString("X2");
            legFlagTextBox.Text = currentSlot.digi_beetle_part_flag[5].ToString("X2");
            armFlagTextBox.Text = currentSlot.digi_beetle_part_flag[6].ToString("X2");
            handFlagTextBox.Text = currentSlot.digi_beetle_part_flag[7].ToString("X2");
            cannon1FlagTextBox.Text = currentSlot.digi_beetle_part_flag[8].ToString("X2");
            cannon2FlagTextBox.Text = currentSlot.digi_beetle_part_flag[9].ToString("X2");
            cannon3FlagTextBox.Text = currentSlot.digi_beetle_part_flag[10].ToString("X2");
            cannon4FlagTextBox.Text = currentSlot.digi_beetle_part_flag[11].ToString("X2");
            cannon5FlagTextBox.Text = currentSlot.digi_beetle_part_flag[12].ToString("X2");
            device1FlagTextBox.Text = currentSlot.digi_beetle_part_flag[13].ToString("X2");
            device2FlagTextBox.Text = currentSlot.digi_beetle_part_flag[14].ToString("X2");
            device3FlagTextBox.Text = currentSlot.digi_beetle_part_flag[15].ToString("X2");
            device4FlagTextBox.Text = currentSlot.digi_beetle_part_flag[16].ToString("X2");
            device5FlagTextBox.Text = currentSlot.digi_beetle_part_flag[17].ToString("X2");
            device6FlagTextBox.Text = currentSlot.digi_beetle_part_flag[18].ToString("X2");

            for (int i = 0; i < 48; i++)
                this.itemList[i].Text = currentSlot.items[i].ToString("X4");
            for (int i = 0; i < 28; i++)
                this.importantItemList[i].Text = currentSlot.important_item[i].ToString("X4");
            for (int i = 0; i < 236; i++)
                this.serverItemList[i].Text = currentSlot.server_item[i].ToString("X4");
        }

        private void slotComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadCurrentSlot();
        }

        #region Misc. Change Event Region
        private void lastLocationTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].locationId1 = Convert.ToByte(lastLocationTextBox.Text, 16);
        }

        private void heroNameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].StringHeroName = heroNameTextBox.Text;
        }

        private void time1TextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].time1 = Convert.ToByte(time1TextBox.Text, 16);
        }

        private void time2TextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].time2 = Convert.ToByte(time2TextBox.Text, 16);
        }

        private void time3TextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].time3 = Convert.ToByte(time3TextBox.Text, 16);
        }

        private void time4TextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].time4 = Convert.ToByte(time4TextBox.Text, 16);
        }

        private void bitsTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].bits = Convert.ToInt32(bitsTextBox.Text);
        }

        private void rankTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].rank = Convert.ToByte(rankTextBox.Text, 16);
        }


        #endregion

        #region Digi-Beetle Change Event Region
        private void DigiBeetleNameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].StringDigiBeetleName = DigiBeetleNameTextBox.Text;
        }

        private void bodyNameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[0] = Convert.ToUInt16(bodyNameTextBox.Text, 16);
        }

        private void engineNameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[1] = Convert.ToUInt16(engineNameTextBox.Text, 16);
        }

        private void memoryNameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[2] = Convert.ToUInt16(memoryNameTextBox.Text, 16);
        }

        private void batteryNameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[3] = Convert.ToUInt16(batteryNameTextBox.Text, 16);
        }

        private void toolBoxNameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[4] = Convert.ToUInt16(toolBoxNameTextBox.Text, 16);
        }

        private void legNameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[5] = Convert.ToUInt16(legNameTextBox.Text, 16);
        }

        private void armNameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[6] = Convert.ToUInt16(armNameTextBox.Text, 16);
        }

        private void handNameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[7] = Convert.ToUInt16(handNameTextBox.Text, 16);
        }

        private void cannon1NameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[8] = Convert.ToUInt16(cannon1NameTextBox.Text, 16);
        }

        private void cannon2NameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[9] = Convert.ToUInt16(cannon2NameTextBox.Text, 16);
        }

        private void cannon3NameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[10] = Convert.ToUInt16(cannon3NameTextBox.Text, 16);
        }

        private void cannon4NameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[11] = Convert.ToUInt16(cannon4NameTextBox.Text, 16);
        }

        private void cannon5NameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[12] = Convert.ToUInt16(cannon5NameTextBox.Text, 16);
        }

        private void device1NameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[13] = Convert.ToUInt16(device1NameTextBox.Text, 16);
        }

        private void device2NameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[14] = Convert.ToUInt16(device2NameTextBox.Text, 16);
        }

        private void device3NameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[15] = Convert.ToUInt16(device3NameTextBox.Text, 16);
        }

        private void device4NameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[16] = Convert.ToUInt16(device4NameTextBox.Text, 16);
        }

        private void device5NameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[17] = Convert.ToUInt16(device5NameTextBox.Text, 16);
        }

        private void device6NameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[18] = Convert.ToUInt16(device6NameTextBox.Text, 16);
        }

        private void bodyFlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[0] = Convert.ToByte(bodyFlagTextBox.Text, 16);
        }

        private void engineFlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[1] = Convert.ToByte(engineFlagTextBox.Text, 16);
        }

        private void batteryFlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[2] = Convert.ToByte(batteryFlagTextBox.Text, 16);
        }

        private void memoryFlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[3] = Convert.ToByte(memoryFlagTextBox.Text, 16);
        }

        private void toolBoxFlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[4] = Convert.ToByte(toolBoxFlagTextBox.Text, 16);
        }

        private void legFlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[5] = Convert.ToByte(legFlagTextBox.Text, 16);
        }

        private void armFlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[6] = Convert.ToByte(armFlagTextBox.Text, 16);
        }

        private void handFlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[7] = Convert.ToByte(handFlagTextBox.Text, 16);
        }

        private void cannon1FlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[8] = Convert.ToByte(cannon1FlagTextBox.Text, 16);
        }

        private void cannon2FlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[9] = Convert.ToByte(cannon2FlagTextBox.Text, 16);
        }

        private void cannon3FlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[10] = Convert.ToByte(cannon3FlagTextBox.Text, 16);
        }

        private void cannon4FlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[11] = Convert.ToByte(cannon4FlagTextBox.Text, 16);
        }

        private void cannon5FlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[12] = Convert.ToByte(cannon5FlagTextBox.Text, 16);
        }

        private void device1FlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[13] = Convert.ToByte(device1FlagTextBox.Text, 16);
        }

        private void device2FlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[14] = Convert.ToByte(device2FlagTextBox.Text, 16);
        }

        private void device3FlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[15] = Convert.ToByte(device3FlagTextBox.Text, 16);
        }

        private void device4FlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[16] = Convert.ToByte(device4FlagTextBox.Text, 16);
        }

        private void device5FlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[17] = Convert.ToByte(device5FlagTextBox.Text, 16);
        }

        private void device6FlagTextBox_TextChanged(object sender, EventArgs e)
        {
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[18] = Convert.ToByte(device6FlagTextBox.Text, 16);
        }
        #endregion

        #region All Items Region
        private void itemTextBox_TextChanged(object sender, EventArgs e)
        {
            var t = (sender as TextBox).Text;
            if (t.Length < 1) return;
            var i = (int) ((sender as TextBox).Tag);
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].items[i] = Convert.ToUInt16(t, 16);
        }
        
        private void importantItemTextBox_TextChanged(object sender, EventArgs e)
        {
            var t = (sender as TextBox).Text;
            if (t.Length < 1) return;
            var i = (int) ((sender as TextBox).Tag);
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].important_item[i] = Convert.ToUInt16(t, 16);
        }
        
        private void serverItemTextBox_TextChanged(object sender, EventArgs e)
        {
            var t = (sender as TextBox).Text;
            if (t.Length < 1) return;
            var i = (int) ((sender as TextBox).Tag);
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].server_item[i] = Convert.ToUInt16(t, 16);
        }
        #endregion

    }
    
}
