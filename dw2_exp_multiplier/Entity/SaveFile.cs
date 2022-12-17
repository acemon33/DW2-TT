using System;
using System.Collections.Generic;
using System.Xml;
using DigimonWorld2Tool.Utility;


namespace dw2_exp_multiplier.Entity
{
    public class Digimon
    {
        public static readonly int LENGTH = 0x5C;

        public byte status;
        public byte id;
        public byte padding0;
        public byte[] padding1 = new byte[10];
        public byte current_lvl;
        public byte padding2;
        public byte max_lvl;
        public UInt32 exp;
        public ushort max_hp;
        public ushort hp;
        public ushort max_mp;
        public ushort mp;
        public ushort attack;
        public ushort defense;
        public ushort speed;
        public byte[] tech = new byte[12];
        public byte[] inherit_tech = new byte[22];
        public byte[] padding3 = new byte[8];
        public byte[] name = new byte[14];
        public ushort padding4;

        public string StringName;

        public Digimon(byte[] data)
        {
            this.status = data[0];
            this.id = data[1];
            this.padding0 = data[2];
            Buffer.BlockCopy(data, 3, this.padding1, 0, this.padding1.Length);
            this.current_lvl = data[13];
            this.padding2 = data[14];
            this.max_lvl = data[15];
            this.exp = BitConverter.ToUInt32(data, 16);
            this.max_hp = BitConverter.ToUInt16(data, 20);
            this.hp = BitConverter.ToUInt16(data, 22);
            this.max_mp = BitConverter.ToUInt16(data, 24);
            this.mp = BitConverter.ToUInt16(data, 26);
            this.attack = BitConverter.ToUInt16(data, 28);
            this.defense = BitConverter.ToUInt16(data, 30);
            this.speed = BitConverter.ToUInt16(data, 32);
            Buffer.BlockCopy(data, 34, this.tech, 0, this.tech.Length);
            Buffer.BlockCopy(data, 46, this.inherit_tech, 0, this.inherit_tech.Length);
            Buffer.BlockCopy(data, 68, this.padding3, 0, this.padding3.Length);
            Buffer.BlockCopy(data, 76, this.name, 0, this.name.Length);
            this.padding4 = BitConverter.ToUInt16(data, 90);

            this.StringName = TextConversion.DigiStringToASCII(this.name);
        }
        
        public Byte[] ToArray()
        {
            byte[] data = new byte[Digimon.LENGTH];

            data[0] = this.status;
            data[1] = this.id;
            data[2] = this.padding0;
            Buffer.BlockCopy(this.padding1, 0, data, 3, this.padding1.Length);
            data[13] = this.current_lvl;
            data[14] = this.padding2;
            data[15] = this.max_lvl;
            Buffer.BlockCopy(BitConverter.GetBytes(this.exp), 0, data, 16, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(this.max_hp), 0, data, 20, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(this.hp), 0, data, 22, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(this.max_mp), 0, data, 24, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(this.mp), 0, data, 26, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(this.attack), 0, data, 28, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(this.defense), 0, data, 30, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(this.speed), 0, data, 32, 2);
            Buffer.BlockCopy(this.tech, 0, data, 34, this.tech.Length);
            Buffer.BlockCopy(this.inherit_tech, 0, data, 46, this.inherit_tech.Length);
            Buffer.BlockCopy(this.padding3, 0, data, 68, this.padding3.Length);
            
            this.name = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            var temp = TextConversion.ASCIIToDigiString(this.StringName);
            Buffer.BlockCopy(temp, 0, this.name, 0, temp.Length);
            Buffer.BlockCopy(this.name, 0, data, 76, this.name.Length);
            
            Buffer.BlockCopy(BitConverter.GetBytes(this.padding4), 0, data, 90, 2);
            
            return data;
        }
    }
    
    public class SaveSlot
    {
        public static readonly int LENGTH = 0x1058;
        public static readonly int DIGIMON_COUNT = 36;
        
        public byte locationId1;
        public byte []padding1 = new byte[12];
        public byte locationId2;
        public ushort padding0;
        public byte time1;
        public byte time2;
        public byte time3;
        public byte time4;
        public Int32 bits;
        public byte[] padding2 = new byte[5];
        public byte unknown0;
        public byte rank;
        public byte padding3;
        public byte[] heroName = new byte[6];
        public byte[] padding4 = new byte[10];
        public byte[] unknown = new byte[8];
        public ushort[] digi_beetle_part = new ushort[19];
        public byte[] digi_beetle_part_flag = new byte[20];
        public ushort[] items = new ushort[48];
        public byte[] may_game_flag1 = new byte[5];
        public byte[] padding5 = new byte[6];
        public byte[] digi_beetle_name = new byte[8];
        public byte[] padding6 = new byte[11];
        public Digimon[] digimon = new Digimon[DIGIMON_COUNT];
        public ushort padding7;
        public ushort[] server_item = new ushort[236];
        public ushort[] important_item = new ushort[28];
        public byte[] padding8 = new byte[32];
        public byte[] may_game_flag2 = new byte[17];
        public byte[] padding9 = new byte[13];
        public byte[] may_game_flag3 = new byte[32];
        public byte story_progress;
        public byte[] padding10 = new byte[7];

        public string StringHeroName;
        public string StringDigiBeetleName;
        
        public Dictionary<UInt32, byte> GameFlags = new Dictionary<UInt32, byte>();
        public static Dictionary<UInt32, string> GameFlagNameList = new Dictionary<UInt32, string>();
        public static List<UInt32> GameFlagsLimiter = new List<UInt32>();
        public static Dictionary<string, List<GameFlag>> GameStoryProgress = new Dictionary<string, List<GameFlag>>();

        public static Dictionary<string, byte> RankList = new Dictionary<string, byte>();

        public static Dictionary<string, string> SavePointLocationList = new Dictionary<string, string>();
        public string lastSavePoint;

        public static Dictionary<string, ushort> DigiBeetleBodyList = new Dictionary<string, ushort>();
        public static Dictionary<string, ushort> DigiBeetleEngineList = new Dictionary<string, ushort>();
        public static Dictionary<string, ushort> DigiBeetleBatteryList = new Dictionary<string, ushort>();
        public static Dictionary<string, ushort> DigiBeetleMemoryList = new Dictionary<string, ushort>();
        public static Dictionary<string, ushort> DigiBeetleToolBoxList = new Dictionary<string, ushort>();
        public static Dictionary<string, ushort> DigiBeetleCannonList = new Dictionary<string, ushort>();
        public static Dictionary<string, ushort> DigiBeetleLegList = new Dictionary<string, ushort>();
        public static Dictionary<string, ushort> DigiBeetleHandList = new Dictionary<string, ushort>();
        public static Dictionary<string, ushort> DigiBeetleArmList = new Dictionary<string, ushort>();
        public static Dictionary<string, ushort> DigiBeetleDeviceList = new Dictionary<string, ushort>();
        
        public static List<BinaryStruct> ItemList = new List<BinaryStruct>();
        public static List<string> ImportantItemList = new List<string>();
        public static List<BinaryStruct> DigimonList = new List<BinaryStruct>();
        public static List<BinaryStruct> TechList = new List<BinaryStruct>();
        
        public SaveSlot(byte[] data)
        {
            this.locationId1 = data[0x0];
            Buffer.BlockCopy(data, 1, this.padding1, 0, this.padding1.Length);
            this.locationId2 = data[0xD];
            this.padding0 = BitConverter.ToUInt16(data, 0xE);
            this.time1 = data[0x10];
            this.time2 = data[0x11];
            this.time3 = data[0x12];
            this.time4 = data[0x13];
            this.bits = BitConverter.ToInt32(data, 0x14);
            Buffer.BlockCopy(data, 0x18, this.padding2, 0, this.padding2.Length);
            this.unknown0 = data[0x1D];
            this.rank = data[0x1E];
            this.padding3 = data[0x1F];
            Buffer.BlockCopy(data, 0x20, this.heroName, 0, this.heroName.Length);
            Buffer.BlockCopy(data, 0x26, this.padding4, 0, this.padding4.Length);
            Buffer.BlockCopy(data, 0x30, this.unknown, 0, this.unknown.Length);
            
            Buffer.BlockCopy(data, 0x38, this.digi_beetle_part, 0, this.digi_beetle_part.Length * 2);
            Buffer.BlockCopy(data, 0x5E, this.digi_beetle_part_flag, 0, this.digi_beetle_part_flag.Length);
            Buffer.BlockCopy(data, 0x72, this.items, 0, this.items.Length * 2);
            Buffer.BlockCopy(data, 0xD2, this.may_game_flag1, 0, this.may_game_flag1.Length);
            Buffer.BlockCopy(data, 0xD7, this.padding5, 0, this.padding5.Length);
            Buffer.BlockCopy(data, 0xDD, this.digi_beetle_name, 0, this.digi_beetle_name.Length);
            Buffer.BlockCopy(data, 0xE5, this.padding6, 0, this.padding6.Length);

            byte[] temp = new byte[Digimon.LENGTH];
            for (int i = 0; i < this.digimon.Length; i++)
            {
                Buffer.BlockCopy(data, i * Digimon.LENGTH + 0xF0, temp, 0, temp.Length);
                this.digimon[i] = new Digimon(temp);
            }
            
            this.padding7 = BitConverter.ToUInt16(data, 0xDE0);
            Buffer.BlockCopy(data, 0xDE2, this.server_item, 0, this.server_item.Length * 2);
            Buffer.BlockCopy(data, 0xFBA, this.important_item, 0, this.important_item.Length * 2);
            Buffer.BlockCopy(data, 0xFF2, this.padding8, 0, this.padding8.Length);
            Buffer.BlockCopy(data, 0x1012, this.may_game_flag2, 0, this.may_game_flag2.Length);
            Buffer.BlockCopy(data, 0x1023, this.padding9, 0, this.padding9.Length);
            Buffer.BlockCopy(data, 0x1030, this.may_game_flag3, 0, this.may_game_flag3.Length);
            this.story_progress = data[0x1050];
            Buffer.BlockCopy(data, 0x1051, this.padding10, 0, this.padding10.Length);

            this.StringHeroName = TextConversion.DigiStringToASCII(this.heroName);
            this.StringDigiBeetleName = TextConversion.DigiStringToASCII(this.digi_beetle_name);

            foreach (var i in GameFlagsLimiter)
                GameFlags[i] = data[i];
            
            this.lastSavePoint = this.locationId1.ToString("X2") + " " + this.locationId2.ToString("X2");
        }
        
        public Byte[] ToArray()
        {
            byte[] data = new byte[0x3FFF]; // todo: plus 1

            string[] sl = this.lastSavePoint.Split(' ');
            this.locationId1 = Convert.ToByte(sl[0], 16);
            this.locationId2 = Convert.ToByte(sl[1], 16);
            
            data[0] = this.locationId1;
            Buffer.BlockCopy(this.padding1, 0, data, 1, this.padding1.Length);
            data[0xD] = this.locationId2;
            Buffer.BlockCopy(BitConverter.GetBytes(this.padding0), 0, data, 0xE, 2);
            data[0x10] = this.time1;
            data[0x11] = this.time2;
            data[0x12] = this.time3;
            data[0x13] = this.time4;
            Buffer.BlockCopy(BitConverter.GetBytes(this.bits), 0, data, 0x14, 4);
            Buffer.BlockCopy(this.padding2, 0, data, 0x18, this.padding2.Length);
            data[0x1D] = this.unknown0;
            data[0x1E] = this.rank;
            data[0x1F] = this.padding3;
            
            this.heroName = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            var temp = TextConversion.ASCIIToDigiString(this.StringHeroName);
            Buffer.BlockCopy(temp, 0, this.heroName, 0, temp.Length);
            Buffer.BlockCopy(this.heroName, 0, data, 0x20, this.heroName.Length);
            Buffer.BlockCopy(this.padding4, 0, data, 0x26, this.padding4.Length);
            Buffer.BlockCopy(this.unknown, 0, data, 0x30, this.unknown.Length);

            Buffer.BlockCopy(this.digi_beetle_part, 0, data, 0x38, this.digi_beetle_part.Length * 2);
            Buffer.BlockCopy(this.digi_beetle_part_flag, 0, data, 0x5E, this.digi_beetle_part_flag.Length);
            Buffer.BlockCopy(this.items, 0, data, 0x72, this.items.Length * 2);
            Buffer.BlockCopy(this.may_game_flag1, 0, data, 0xD2, this.may_game_flag1.Length);
            Buffer.BlockCopy(this.padding5, 0, data, 0xD7, this.padding5.Length);
            
            this.digi_beetle_name = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            temp = TextConversion.ASCIIToDigiString(this.StringDigiBeetleName);
            Buffer.BlockCopy(temp, 0, this.digi_beetle_name, 0, temp.Length);
            Buffer.BlockCopy(this.digi_beetle_name, 0, data, 0xDD, this.digi_beetle_name.Length);
            
            Buffer.BlockCopy(this.padding6, 0, data, 0xE5, this.padding6.Length);
            for (int i = 0; i < this.digimon.Length; i++)
            {
                Buffer.BlockCopy(this.digimon[i].ToArray(), 0, data,  i * Digimon.LENGTH + 0xF0, Digimon.LENGTH);
            }
            Buffer.BlockCopy(BitConverter.GetBytes(this.padding7), 0, data, 0xDE0, 2);
            Buffer.BlockCopy(this.server_item, 0, data, 0xDE2, this.server_item.Length * 2);
            Buffer.BlockCopy(this.important_item, 0, data, 0xFBA, this.important_item.Length * 2);
            Buffer.BlockCopy(this.padding8, 0, data, 0xFF2, this.padding8.Length);
            Buffer.BlockCopy(this.may_game_flag2, 0, data, 0x1012, this.may_game_flag2.Length);
            Buffer.BlockCopy(this.padding9, 0, data, 0x1023, this.padding9.Length);
            Buffer.BlockCopy(this.may_game_flag3, 0, data, 0x1030, this.may_game_flag3.Length);
            data[0x1050] = this.story_progress;
            Buffer.BlockCopy(padding10, 0, data, 0x1051, padding10.Length);

            foreach (var i in GameFlagsLimiter)
                data[i] = GameFlags[i];
            
            return data;
        }

        public static void load1()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("config.xml");
            foreach (XmlNode mission in xml.SelectNodes("dw2-utility/missions/mission"))
            {
                List<GameFlag> list = new List<GameFlag>();
                string name = mission.SelectSingleNode("name").InnerText;
                foreach (XmlNode flag in mission.SelectNodes("flags/flag"))
                {
                    uint address = Convert.ToUInt32(flag.Attributes["address"].Value, 16);
                    int value = Convert.ToInt32(flag.Attributes["value"].Value, 16);
                    list.Add(new GameFlag(address, value));
                }

                GameStoryProgress[name] = list;
            }

            foreach (XmlNode x in xml.SelectNodes("dw2-utility/game-flags/flags/flag"))
            {
                UInt32 from = Convert.ToUInt32(x.SelectSingleNode("from").InnerText, 16);
                UInt32 to = Convert.ToUInt32(x.SelectSingleNode("to").InnerText, 16);
                for (uint i = from; i <= to; i++)
                    SaveSlot.GameFlagsLimiter.Add(i);
            }

            foreach (XmlNode x in xml.SelectNodes("dw2-utility/ranks/rank"))
            {
                string title = x.Attributes["title"].Value;
                byte value = Convert.ToByte(x.Attributes["value"].Value, 16);
                RankList[title] = value;
            }

            foreach (XmlNode x in xml.SelectNodes("dw2-utility/last-save-points/point"))
            {
                var name = x.Attributes["name"].Value;
                var site = x.Attributes["area"].Value + " " + x.Attributes["location"].Value;
                SavePointLocationList[name] = site;
            }

            foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/bodies/body"))
            {
                var name = x.Attributes["name"].Value;
                var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                DigiBeetleBodyList[name] = value;
            }

            foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/engines/engine"))
            {
                var name = x.Attributes["name"].Value;
                var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                DigiBeetleEngineList[name] = value;
            }
            
            foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/batteries/battery"))
            {
                var name = x.Attributes["name"].Value;
                var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                DigiBeetleBatteryList[name] = value;
            }
            
            foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/memories/memory"))
            {
                var name = x.Attributes["name"].Value;
                var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                DigiBeetleMemoryList[name] = value;
            }
            
            foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/tool-boxes/tool-box"))
            {
                var name = x.Attributes["name"].Value;
                var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                DigiBeetleToolBoxList[name] = value;
            }
            
            foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/cannons/cannon"))
            {
                var name = x.Attributes["name"].Value;
                var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                DigiBeetleCannonList[name] = value;
            }
            
            foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/legs/leg"))
            {
                var name = x.Attributes["name"].Value;
                var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                DigiBeetleLegList[name] = value;
            }
            
            foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/hands/hand"))
            {
                var name = x.Attributes["name"].Value;
                var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                DigiBeetleHandList[name] = value;
            }
            
            foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/arms/arm"))
            {
                var name = x.Attributes["name"].Value;
                var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                DigiBeetleArmList[name] = value;
            }
            
            foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/devices/device"))
            {
                var name = x.Attributes["name"].Value;
                var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                DigiBeetleDeviceList[name] = value;
            }
            
            foreach (XmlNode x in xml.SelectNodes("dw2-utility/items/item"))
            {
                var name = x.Attributes["name"].Value;
                var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                ItemList.Add(new BinaryStruct(name, value));
            }
            
            foreach (XmlNode x in xml.SelectNodes("dw2-utility/important-items/important-item"))
            {
                var name = x.Attributes["name"].Value;
                ImportantItemList.Add(name);
            }
            
            foreach (XmlNode x in xml.SelectNodes("dw2-utility/game-flags/names/flag"))
            {
                var address = Convert.ToUInt32(x.Attributes["address"].Value, 16);
                var name = x.Attributes["name"].Value;
                GameFlagNameList[address] = name;
            }
            
            XmlDocument xml2 = new XmlDocument();
            xml.Load("data.xml");
            foreach (XmlNode x in xml.SelectNodes("dw2-utility/digimons/digimon"))
            {
                var name = x.Attributes["name"].Value;
                var value = Convert.ToByte(x.Attributes["id"].Value, 16);
                DigimonList.Add(new BinaryStruct(name, value));
            }
            
            foreach (XmlNode x in xml.SelectNodes("dw2-utility/techs/tech"))
            {
                var name = x.Attributes["name"].Value;
                var value = Convert.ToByte(x.Attributes["id"].Value, 16);
                TechList.Add(new BinaryStruct(name, value));
            }

        }

    }
    
    public class SaveFile
    {
        public static readonly int SAVE_SLOT_COUNT = 3;
        
        public Int32[] header = new Int32[128];
        public SaveSlot[] saveSlot = new SaveSlot[SAVE_SLOT_COUNT];
        public Int32[] padding = new Int32[829];
        public ushort checksum1;
        public ushort checksum2;

        public SaveFile(byte[] data)
        {
            Buffer.BlockCopy(data, 0, this.header, 0, this.header.Length * 4);
            
            byte[] temp = new byte[SaveSlot.LENGTH];
            for (int i = 0; i < SAVE_SLOT_COUNT; i++)
            {
                Buffer.BlockCopy(data, i * SaveSlot.LENGTH + (this.header.Length * 4), temp, 0, temp.Length);
                this.saveSlot[i] = new SaveSlot(temp);
            }
            
            Buffer.BlockCopy(data, 0x3314, this.padding, 0, this.padding.Length);
            this.checksum1 = BitConverter.ToUInt16(data, 0x3FFC);
            this.checksum2 = BitConverter.ToUInt16(data, 0x3FFE);
        }

        public Byte[] ToArray()
        {
            byte[] data = new byte[0x4000];
            
            Buffer.BlockCopy(this.header, 0, data, 0, this.header.Length * 4);
            
            for (int i = 0; i < SAVE_SLOT_COUNT; i++)
            {
                Buffer.BlockCopy(this.saveSlot[i].ToArray(), 0, data,  i * SaveSlot.LENGTH + (this.header.Length * 4), SaveSlot.LENGTH);
            }
            
            Buffer.BlockCopy(this.padding, 0,data, 0x3314,  this.padding.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(checksum1), 0, data, 0x3FFC, 2);
            this.checksum2 = this.CalculateChecksum(data);
            Buffer.BlockCopy(BitConverter.GetBytes(checksum2), 0, data, 0x3FFE, 2);
            return data;
        }

        public ushort CalculateChecksum(byte[] data)
        {
            int data_1 = 0, calculated_value = 0, ptr = 0, data_2;

            int counter = 0x1fff;
            do
            {
                if (counter == 0) break;
                counter -= 2;
                data_1 = BitConverter.ToUInt16(data, ptr);
                data_2 = ptr + 2;
                if(counter == -1) break;
                ptr += 4;
                calculated_value = BitConverter.ToUInt16(data, data_2) + (calculated_value ^ data_1);
            } while(true);
            int result = calculated_value ^ data_1;
            byte[] a = BitConverter.GetBytes(result);
            ushort b = BitConverter.ToUInt16(a, 0);
            return b;
        }

    }

    public struct BinaryStruct
    {
        public string Key { get; }
        public ushort Value { get; }

        public BinaryStruct(string key, ushort value)
        {
            Key = key;
            Value = value;
        }
    }

    public struct GameFlag
    {
        public uint address;
        public int value;

        public GameFlag(uint address, int value)
        {
            this.address = address;
            this.value = value;
        }
    }

    public enum DIGI_LINE_STATUS
    {
        B1 = 0x3,
        B2 = 0x4,
        B3 = 0x5,
        BENCH = 0x2,
        SERVER = 0x1,
        NONE = 0x0
    }
    
}
