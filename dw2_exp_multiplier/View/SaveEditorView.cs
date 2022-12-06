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
        TableLayoutPanel GameFlagsTableLayoutPanel = new TableLayoutPanel();
        
        private List<ComboBox> itemList = new List<ComboBox>();
        private List<ComboBox> importantItemList = new List<ComboBox>();
        private List<NumericUpDown> serverItemList = new List<NumericUpDown>();
        private List<ComboBox> digimonTechList = new List<ComboBox>();
        private List<ComboBox> digimonInheritedTechList = new List<ComboBox>();
        private Dictionary<uint, TextBox> GameFlagsTextBoxList = new Dictionary<uint, TextBox>();
        #endregion

        #region Init Region
        public SaveEditorView()
        {
            InitializeComponent();
            SaveSlot.load1();

            this.lastLocationComboBox.DisplayMember = "Key";
            this.lastLocationComboBox.ValueMember = "Value";
            this.rankComboBox.DisplayMember = "Key";
            this.rankComboBox.ValueMember = "Value";
            
            this.bodyNameComboBox.DisplayMember = "Key";
            this.bodyNameComboBox.ValueMember = "Value";
            this.engineNameComboBox.DisplayMember = "Key";
            this.engineNameComboBox.ValueMember = "Value";
            this.batteryNameComboBox.DisplayMember = "Key";
            this.batteryNameComboBox.ValueMember = "Value";
            this.memoryComboBox.DisplayMember = "Key";
            this.memoryComboBox.ValueMember = "Value";
            this.toolBoxComboBox.DisplayMember = "Key";
            this.toolBoxComboBox.ValueMember = "Value";
            this.cannon1ComboBox.DisplayMember = "Key";
            this.cannon1ComboBox.ValueMember = "Value";
            this.cannon2ComboBox.DisplayMember = "Key";
            this.cannon2ComboBox.ValueMember = "Value";
            this.cannon3ComboBox.DisplayMember = "Key";
            this.cannon3ComboBox.ValueMember = "Value";
            this.cannon4ComboBox.DisplayMember = "Key";
            this.cannon4ComboBox.ValueMember = "Value";
            this.cannon5ComboBox.DisplayMember = "Key";
            this.cannon5ComboBox.ValueMember = "Value";
            this.legComboBox.DisplayMember = "Key";
            this.legComboBox.ValueMember = "Value";
            this.handComboBox.DisplayMember = "Key";
            this.handComboBox.ValueMember = "Value";
            this.armComboBox.DisplayMember = "Key";
            this.armComboBox.ValueMember = "Value";
            this.device1ComboBox.DisplayMember = "Key";
            this.device1ComboBox.ValueMember = "Value";
            this.device2ComboBox.DisplayMember = "Key";
            this.device2ComboBox.ValueMember = "Value";
            this.device3ComboBox.DisplayMember = "Key";
            this.device3ComboBox.ValueMember = "Value";
            this.device4ComboBox.DisplayMember = "Key";
            this.device4ComboBox.ValueMember = "Value";
            this.device5ComboBox.DisplayMember = "Key";
            this.device5ComboBox.ValueMember = "Value";
            this.device6ComboBox.DisplayMember = "Key";
            this.device6ComboBox.ValueMember = "Value";
            
            this.digimonOriginalNameComboBox.DisplayMember = "Key";
            this.digimonOriginalNameComboBox.ValueMember = "Value";
            
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
                var t1 = new ComboBox();
                t1.DisplayMember = "Key";
                t1.ValueMember = "Value";
                t1.DataSource = new BindingSource(SaveSlot.ItemList, null);
                t1.SelectedIndexChanged += itemTextBox_TextChanged;
                t1.Tag = i;
                itemTableLayoutPanel.Controls.Add(l1, 0, i);
                itemTableLayoutPanel.Controls.Add(t1, 1, i);
                this.itemList.Add(t1);
            }
            itemTableLayoutPanel.RowCount = n;
            for(int i = 0; i < n; i++) itemTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));

            string[] haveNotHaveList = new string[] {"Not Have", "Have"};
            n = 28;
            this.importantItemPanel.Controls.Add(importantItemTableLayoutPanel);
            importantItemTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            importantItemTableLayoutPanel.Name = "importantItemTableLayoutPanel";
            importantItemTableLayoutPanel.Size = new System.Drawing.Size(200, 27 * n + 20);
            importantItemTableLayoutPanel.ColumnCount = 2;
            for (int i = 0; i < n; i++)
            {
                importantItemTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
                importantItemTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                var l1 = new Label();
                l1.Size = new Size(150, l1.Size.Height);
                l1.Text = SaveSlot.ImportantItemList[i];
                var t1 = new ComboBox();
                importantItemTableLayoutPanel.Controls.Add(l1, 0, i);
                importantItemTableLayoutPanel.Controls.Add(t1, 1, i);
                t1.TextChanged += importantItemTextBox_TextChanged;
                t1.Tag = i;
                t1.Items.AddRange(haveNotHaveList);
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
                l1.Text = SaveSlot.ItemList[i].Key;
                var t1 = new NumericUpDown();
                serverItemTableLayoutPanel.Controls.Add(l1, 0, i);
                serverItemTableLayoutPanel.Controls.Add(t1, 1, i);
                t1.TextChanged += serverItemTextBox_TextChanged;
                t1.Tag = i;
                t1.Maximum = 99;
                this.serverItemList.Add(t1);
            }
            serverItemTableLayoutPanel.RowCount = n;
            for(int i = 0; i < n; i++) serverItemTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));

            n = 12;
            for (int i = 0; i < n; i++)
            {
                var l1 = new Label();
                l1.Text = "Tech #" + (i + 1);
                var t1 = new ComboBox();
                digimonTechTableLayoutPanel.Controls.Add(l1, 0, i);
                digimonTechTableLayoutPanel.Controls.Add(t1, 1, i);
                t1.SelectedIndexChanged += techTextBox_TextChanged;
                t1.Tag = i;
                t1.DisplayMember = "Key";
                t1.ValueMember = "Value";
                t1.DropDownWidth = 130;
                t1.DataSource = new BindingSource(SaveSlot.TechList, null);
                this.digimonTechList.Add(t1);
            }

            n = 22;
            for (int i = 0; i < n; i++)
            {
                var l1 = new Label();
                l1.Text = "Inherit Tech #" + (i + 1);
                var t1 = new ComboBox();
                digimonTechTableLayoutPanel.Controls.Add(l1, 2, i);
                digimonTechTableLayoutPanel.Controls.Add(t1, 3, i);
                t1.SelectedIndexChanged += inheritTechTextBox_TextChanged;
                t1.Tag = i;
                t1.DisplayMember = "Key";
                t1.ValueMember = "Value";
                t1.DropDownWidth = 130;
                t1.DataSource = new BindingSource(SaveSlot.TechList, null);
                this.digimonInheritedTechList.Add(t1);
            }
            
            this.digimonListBox.DisplayMember = "Key";
            this.digimonListBox.ValueMember = "Value";
            
            n = SaveSlot.GameFlagsLimiter.Count;
            this.gameStoryPanel.Controls.Add(GameFlagsTableLayoutPanel);
            GameFlagsTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            GameFlagsTableLayoutPanel.Name = "gameStoryTableLayoutPanel";
            GameFlagsTableLayoutPanel.Size = new System.Drawing.Size(239, 27 * n + 20);
            GameFlagsTableLayoutPanel.ColumnCount = 2;
            GameFlagsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            GameFlagsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            for (int i = 0; i < n; i++)
            {
                uint address = SaveSlot.GameFlagsLimiter[i];
                var l1 = new Label();
                l1.Text = "Game Flag #" + address;
                var t1 = new TextBox();
                GameFlagsTableLayoutPanel.Controls.Add(l1, 0, i);
                GameFlagsTableLayoutPanel.Controls.Add(t1, 1, i);
                t1.TextChanged += gameFlagTextBox_TextChanged;
                t1.Tag = address;
                this.GameFlagsTextBoxList[address] = t1;
            }
            GameFlagsTableLayoutPanel.RowCount = n;
            for(int i = 0; i < n; i++)
                GameFlagsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            
            this.lastLocationComboBox.DataSource = new BindingSource(SaveSlot.SavePointLocationList, null);
            this.rankComboBox.DataSource = new BindingSource(SaveSlot.RankList, null);
            
            this.bodyNameComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleBodyList, null);
            this.engineNameComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleEngineList, null);
            this.batteryNameComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleBatteryList, null);
            this.memoryComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleMemoryList, null);
            this.toolBoxComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleToolBoxList, null);
            this.cannon1ComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleCannonList, null);
            this.cannon2ComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleCannonList, null);
            this.cannon3ComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleCannonList, null);
            this.cannon4ComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleCannonList, null);
            this.cannon5ComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleCannonList, null);
            this.legComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleLegList, null);
            this.handComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleHandList, null);
            this.armComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleArmList, null);
            this.device1ComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleDeviceList, null);
            this.device2ComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleDeviceList, null);
            this.device3ComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleDeviceList, null);
            this.device4ComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleDeviceList, null);
            this.device5ComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleDeviceList, null);
            this.device6ComboBox.DataSource = new BindingSource(SaveSlot.DigiBeetleDeviceList, null);
            
            this.digimonOriginalNameComboBox.DataSource = new BindingSource(SaveSlot.DigimonList, null);
            this.digimonOriginalNameComboBox.SelectedIndex = -1;
        }
        
        private void LoadCurrentSlot()
        {
            var currentSlot = this.saveFile.saveSlot[this.slotComboBox.SelectedIndex];

                    // Misc.
            lastLocationComboBox.SelectedValue = currentSlot.lastSavePoint;
            heroNameTextBox.Text = currentSlot.StringHeroName;
            time1TextBox.Text = currentSlot.time1.ToString("X2");
            time2TextBox.Text = currentSlot.time2.ToString("X2");
            time3TextBox.Text = currentSlot.time3.ToString("X2");
            time4TextBox.Text = currentSlot.time4.ToString("X2");
            bitsTextBox.Text = currentSlot.bits.ToString();
            rankComboBox.SelectedValue = currentSlot.rank;

                    // Digi-Beetle Parts
            DigiBeetleNameTextBox.Text = currentSlot.StringDigiBeetleName;
            bodyNameComboBox.SelectedValue = currentSlot.digi_beetle_part[0];
            engineNameComboBox.SelectedValue = currentSlot.digi_beetle_part[1];
            memoryComboBox.SelectedValue = currentSlot.digi_beetle_part[2];
            batteryNameComboBox.SelectedValue = currentSlot.digi_beetle_part[3];
            toolBoxComboBox.SelectedValue = currentSlot.digi_beetle_part[4];
            legComboBox.SelectedValue = currentSlot.digi_beetle_part[5];
            armComboBox.SelectedValue = currentSlot.digi_beetle_part[6];
            handComboBox.SelectedValue = currentSlot.digi_beetle_part[7];
            cannon1ComboBox.SelectedValue = currentSlot.digi_beetle_part[8];
            cannon2ComboBox.SelectedValue = currentSlot.digi_beetle_part[9];
            cannon3ComboBox.SelectedValue = currentSlot.digi_beetle_part[10];
            cannon4ComboBox.SelectedValue = currentSlot.digi_beetle_part[11];
            cannon5ComboBox.SelectedValue = currentSlot.digi_beetle_part[12];
            device1ComboBox.SelectedValue = currentSlot.digi_beetle_part[13];
            device2ComboBox.SelectedValue = currentSlot.digi_beetle_part[14];
            device3ComboBox.SelectedValue = currentSlot.digi_beetle_part[15];
            device4ComboBox.SelectedValue = currentSlot.digi_beetle_part[16];
            device5ComboBox.SelectedValue = currentSlot.digi_beetle_part[17];
            device6ComboBox.SelectedValue = currentSlot.digi_beetle_part[18];
            bodyFlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[0];
            engineFlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[1];
            memoryFlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[2];
            batteryFlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[3];
            toolBoxFlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[4];
            legFlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[5];
            armFlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[6];
            handFlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[7];
            cannon1FlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[8];
            cannon2FlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[9];
            cannon3FlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[10];
            cannon4FlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[11];
            cannon5FlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[12];
            device1FlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[13];
            device2FlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[14];
            device3FlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[15];
            device4FlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[16];
            device5FlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[17];
            device6FlagTextBox.SelectedIndex = currentSlot.digi_beetle_part_flag[18];

                    // All Items
            for (int i = 0; i < 48; i++)
                this.itemList[i].SelectedValue = currentSlot.items[i];
            for (int i = 0; i < 28; i++)
                this.importantItemList[i].SelectedIndex = currentSlot.important_item[i];
            for (int i = 0; i < 236; i++)
                this.serverItemList[i].Value = currentSlot.server_item[i];
            
            this.digimonListBox.Items.Clear();
            for (int i = 0; i < Entity.SaveSlot.DIGIMON_COUNT; i++)
                this.digimonListBox.Items.Add(new { Key = "Slot #" + i, Value = currentSlot.digimon[i]});

            foreach (var i in SaveSlot.GameFlagsLimiter)
                this.GameFlagsTextBoxList[i].Text = currentSlot.GameFlags[i].ToString("X2");
        }
        
        private void slotComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.LoadCurrentSlot();
        }
        #endregion

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

        #region Misc. Change Event Region
        private void lastLocationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].lastSavePoint = (this.lastLocationComboBox.SelectedItem as dynamic).Value;
        }

        private void heroNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].StringHeroName = heroNameTextBox.Text;
        }

        private void time1TextBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].time1 = Convert.ToByte(time1TextBox.Text, 16);
        }

        private void time2TextBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].time2 = Convert.ToByte(time2TextBox.Text, 16);
        }

        private void time3TextBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].time3 = Convert.ToByte(time3TextBox.Text, 16);
        }

        private void time4TextBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].time4 = Convert.ToByte(time4TextBox.Text, 16);
        }

        private void bitsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].bits = Convert.ToInt32(bitsTextBox.Text);
        }

        private void rankComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].rank = (this.rankComboBox.SelectedItem as dynamic).Value;
        }
        #endregion

        #region Digi-Beetle Change Event Region
        private void DigiBeetleNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].StringDigiBeetleName = DigiBeetleNameTextBox.Text;
        }

        private void bodyNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[0] = (this.bodyNameComboBox.SelectedItem as dynamic).Value;
        }

        private void engineNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[1] = (this.engineNameComboBox.SelectedItem as dynamic).Value;
        }

        private void memoryComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[2] = (this.memoryComboBox.SelectedItem as dynamic).Value;
        }

        private void batteryComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[3] = (this.batteryNameComboBox.SelectedItem as dynamic).Value;
        }

        private void toolBoxComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[4] = (this.toolBoxComboBox.SelectedItem as dynamic).Value;
        }

        private void legComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[5] = (this.legComboBox.SelectedItem as dynamic).Value;
        }

        private void armComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[6] = (this.armComboBox.SelectedItem as dynamic).Value;
        }

        private void handComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[7] = (this.handComboBox.SelectedItem as dynamic).Value;
        }

        private void cannon1ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[8] = (this.cannon1ComboBox.SelectedItem as dynamic).Value;
        }

        private void cannon2ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[9] = (this.cannon2ComboBox.SelectedItem as dynamic).Value;
        }

        private void cannon3ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[10] = (this.cannon3ComboBox.SelectedItem as dynamic).Value;
        }

        private void cannon4ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[11] = (this.cannon4ComboBox.SelectedItem as dynamic).Value;
        }

        private void cannon5ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[12] = (this.cannon5ComboBox.SelectedItem as dynamic).Value;
        }

        private void device1ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[13] = (this.device1ComboBox.SelectedItem as dynamic).Value;
        }

        private void device2ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[14] = (this.device2ComboBox.SelectedItem as dynamic).Value;
        }

        private void device3ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[15] = (this.device3ComboBox.SelectedItem as dynamic).Value;
        }

        private void device4ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[16] = (this.device4ComboBox.SelectedItem as dynamic).Value;
        }

        private void device5ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[17] = (this.device5ComboBox.SelectedItem as dynamic).Value;
        }

        private void device6ComboBox_TextChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0) return;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part[18] = (this.device6ComboBox.SelectedItem as dynamic).Value;
        }

        private void bodyFlagTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.slotComboBox.SelectedIndex < 0) return;
            var comboBox = sender as ComboBox;
            var i = Convert.ToInt16(comboBox.Tag);
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digi_beetle_part_flag[i] = Convert.ToByte(comboBox.SelectedIndex);
        }
        #endregion

        #region All Items Change Event Region
        private void itemTextBox_TextChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox.SelectedIndex < 0) return;
            var i = (int) comboBox.Tag;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].items[i] = (comboBox.SelectedItem as dynamic).Value;
        }
        
        private void importantItemTextBox_TextChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox.SelectedIndex < 0) return;
            var i = (int) comboBox.Tag;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].important_item[i] = (ushort) comboBox.SelectedIndex;
        }
        
        private void serverItemTextBox_TextChanged(object sender, EventArgs e)
        {
            var control = sender as NumericUpDown;
            var i = (int) control.Tag;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].server_item[i] = Convert.ToUInt16(control.Value);
        }
        #endregion

        #region Digimon Change Event Region
        private void digimonListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.digimonListBox.SelectedIndex > -1)
            {
                var currentSlot = this.saveFile.saveSlot[this.slotComboBox.SelectedIndex];
                var i = this.digimonListBox.SelectedIndex;

                this.digimonStatustextBox.Text = currentSlot.digimon[i].status.ToString("X2");
                this.digimonIdTextBox.Text = currentSlot.digimon[i].id.ToString("X2");
                this.digimonNameTextBox.Text = currentSlot.digimon[i].StringName;
                this.digimonExpTextBox.Text = currentSlot.digimon[i].exp.ToString();
                this.digimonLevelTextBox.Text = currentSlot.digimon[i].current_lvl.ToString();
                this.digimonMaxLevelTextBox.Text = currentSlot.digimon[i].max_lvl.ToString();
                this.digimonOriginalNameComboBox.SelectedValue = (ushort) currentSlot.digimon[i].id;
                
                this.digimonHpTextBox.Text = currentSlot.digimon[i].hp.ToString();
                this.digimonMapHpTextBox.Text = currentSlot.digimon[i].max_hp.ToString();
                this.digimonMpTextBox.Text = currentSlot.digimon[i].mp.ToString();
                this.digimonMaxMpTextBox.Text = currentSlot.digimon[i].max_mp.ToString();
                this.digimonAttackTextBox.Text = currentSlot.digimon[i].attack.ToString();
                this.digimonDefenseTextBox.Text = currentSlot.digimon[i].defense.ToString();
                this.digimonSpeedTextBox.Text = currentSlot.digimon[i].speed.ToString();

                for (int j = 0; j < this.digimonTechList.Count; j++)
                    this.digimonTechList[j].SelectedValue = (ushort) currentSlot.digimon[i].tech[j];
                for (int j = 0; j < this.digimonInheritedTechList.Count; j++)
                    this.digimonInheritedTechList[j].SelectedValue = (ushort) currentSlot.digimon[i].inherit_tech[j];
            }
        }
        private void techTextBox_TextChanged(object sender, EventArgs e)
        {
            var t = (sender as ComboBox);
            if (slotComboBox.SelectedIndex < 0|| t.SelectedItem == null) return;
            var i = (int) (t.Tag);
            var j = this.digimonListBox.SelectedIndex;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digimon[j].tech[i] = Convert.ToByte((t.SelectedItem as dynamic).Value);
        }

        private void inheritTechTextBox_TextChanged(object sender, EventArgs e)
        {
            var t = (sender as ComboBox);
            if (slotComboBox.SelectedIndex < 0|| t.SelectedItem == null) return;
            var i = (int) (t.Tag);
            var j = this.digimonListBox.SelectedIndex;
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].digimon[j].inherit_tech[i] = Convert.ToByte((t.SelectedItem as dynamic).Value);
        }

        private void digimonLevelTextBox_TextChanged(object sender, EventArgs e)
        {
            var i = this.slotComboBox.SelectedIndex;
            var j = this.digimonListBox.SelectedIndex;
            this.saveFile.saveSlot[i].digimon[j].current_lvl = Convert.ToByte(digimonLevelTextBox.Text);
        }

        private void digimonMaxLevelTextBox_TextChanged(object sender, EventArgs e)
        {
            var i = this.slotComboBox.SelectedIndex;
            var j = this.digimonListBox.SelectedIndex;
            this.saveFile.saveSlot[i].digimon[j].max_lvl = Convert.ToByte(digimonMaxLevelTextBox.Text);
        }

        private void digimonNameTextBox_TextChanged(object sender, EventArgs e)
        {
            var i = this.slotComboBox.SelectedIndex;
            var j = this.digimonListBox.SelectedIndex;
            this.saveFile.saveSlot[i].digimon[j].StringName = digimonNameTextBox.Text;
        }

        private void digimonExpTextBox_TextChanged(object sender, EventArgs e)
        {
            var i = this.slotComboBox.SelectedIndex;
            var j = this.digimonListBox.SelectedIndex;
            this.saveFile.saveSlot[i].digimon[j].exp = Convert.ToUInt32(digimonExpTextBox.Text);
        }

        private void digimonHpTextBox_TextChanged(object sender, EventArgs e)
        {
            var i = this.slotComboBox.SelectedIndex;
            var j = this.digimonListBox.SelectedIndex;
            this.saveFile.saveSlot[i].digimon[j].hp = Convert.ToUInt16(digimonHpTextBox.Text);
        }

        private void digimonMapHpTextBox_TextChanged(object sender, EventArgs e)
        {
            var i = this.slotComboBox.SelectedIndex;
            var j = this.digimonListBox.SelectedIndex;
            this.saveFile.saveSlot[i].digimon[j].max_hp = Convert.ToUInt16(digimonMapHpTextBox.Text);
        }

        private void digimonMpTextBox_TextChanged(object sender, EventArgs e)
        {
            var i = this.slotComboBox.SelectedIndex;
            var j = this.digimonListBox.SelectedIndex;
            this.saveFile.saveSlot[i].digimon[j].mp = Convert.ToUInt16(digimonMpTextBox.Text);
        }

        private void digimonMaxMpTextBox_TextChanged(object sender, EventArgs e)
        {
            var i = this.slotComboBox.SelectedIndex;
            var j = this.digimonListBox.SelectedIndex;
            this.saveFile.saveSlot[i].digimon[j].max_mp = Convert.ToUInt16(digimonMaxMpTextBox.Text);
        }

        private void digimonAttackTextBox_TextChanged(object sender, EventArgs e)
        {
            var i = this.slotComboBox.SelectedIndex;
            var j = this.digimonListBox.SelectedIndex;
            this.saveFile.saveSlot[i].digimon[j].attack = Convert.ToUInt16(digimonAttackTextBox.Text);
        }

        private void digimonDefenseTextBox_TextChanged(object sender, EventArgs e)
        {
            var i = this.slotComboBox.SelectedIndex;
            var j = this.digimonListBox.SelectedIndex;
            this.saveFile.saveSlot[i].digimon[j].defense = Convert.ToUInt16(digimonDefenseTextBox.Text);
        }

        private void digimonSpeedTextBox_TextChanged(object sender, EventArgs e)
        {
            var i = this.slotComboBox.SelectedIndex;
            var j = this.digimonListBox.SelectedIndex;
            this.saveFile.saveSlot[i].digimon[j].speed = Convert.ToUInt16(digimonSpeedTextBox.Text);
        }
        
        private void digimonOriginalNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (slotComboBox.SelectedIndex < 0 || digimonOriginalNameComboBox.SelectedItem == null) return;
            var i = this.slotComboBox.SelectedIndex;
            var j = this.digimonListBox.SelectedIndex;
            ushort id = (digimonOriginalNameComboBox.SelectedItem as dynamic).Value;
            this.saveFile.saveSlot[i].digimon[j].id = Convert.ToByte(id);
            this.digimonIdTextBox.Text = id.ToString("X2");
        }
        #endregion

        #region Game Story Region
        private void gameFlagTextBox_TextChanged(object sender, EventArgs e)
        {
            var t = (sender as TextBox).Text;
            if (t.Length < 1) return;
            var i = (uint) ((sender as TextBox).Tag);
            this.saveFile.saveSlot[this.slotComboBox.SelectedIndex].GameFlags[i] = Convert.ToByte(t, 16);
        }
        #endregion
        
    }
    
}
