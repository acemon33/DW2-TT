using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace dw2_exp_multiplier
{
    class Configuration
    {
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

        public static Dictionary<UInt32, string> GameFlagNameList = new Dictionary<UInt32, string>();
        public static List<UInt32> GameFlagsLimiter = new List<UInt32>();
        public static Dictionary<string, List<GameFlag>> GameStoryProgress = new Dictionary<string, List<GameFlag>>();

        public static Dictionary<string, byte> RankList = new Dictionary<string, byte>();

        public static Dictionary<string, string> SavePointLocationList = new Dictionary<string, string>();

        public static String mode = "Vanilla";

        public static void SetVanillaMode() { mode = "Vanilla"; }
        public static void SetAlternativeMode() { mode = "Alternative"; }
        public static void load()
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load("Resources\\" + mode + "\\config.xml");
                
                GameStoryProgress.Clear();
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

                GameFlagsLimiter.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/game-flags/flags/flag"))
                {
                    UInt32 from = Convert.ToUInt32(x.SelectSingleNode("from").InnerText, 16);
                    UInt32 to = Convert.ToUInt32(x.SelectSingleNode("to").InnerText, 16);
                    for (uint i = from; i <= to; i++)
                        GameFlagsLimiter.Add(i);
                }

                RankList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/ranks/rank"))
                {
                    string title = x.Attributes["title"].Value;
                    byte value = Convert.ToByte(x.Attributes["value"].Value, 16);
                    RankList[title] = value;
                }

                SavePointLocationList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/last-save-points/point"))
                {
                    var name = x.Attributes["name"].Value;
                    var site = x.Attributes["area"].Value + " " + x.Attributes["location"].Value;
                    SavePointLocationList[name] = site;
                }

                DigiBeetleBodyList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/bodies/body"))
                {
                    var name = x.Attributes["name"].Value;
                    var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                    DigiBeetleBodyList[name] = value;
                }

                DigiBeetleEngineList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/engines/engine"))
                {
                    var name = x.Attributes["name"].Value;
                    var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                    DigiBeetleEngineList[name] = value;
                }

                DigiBeetleBatteryList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/batteries/battery"))
                {
                    var name = x.Attributes["name"].Value;
                    var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                    DigiBeetleBatteryList[name] = value;
                }

                DigiBeetleMemoryList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/memories/memory"))
                {
                    var name = x.Attributes["name"].Value;
                    var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                    DigiBeetleMemoryList[name] = value;
                }

                DigiBeetleToolBoxList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/tool-boxes/tool-box"))
                {
                    var name = x.Attributes["name"].Value;
                    var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                    DigiBeetleToolBoxList[name] = value;
                }

                DigiBeetleCannonList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/cannons/cannon"))
                {
                    var name = x.Attributes["name"].Value;
                    var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                    DigiBeetleCannonList[name] = value;
                }

                DigiBeetleLegList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/legs/leg"))
                {
                    var name = x.Attributes["name"].Value;
                    var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                    DigiBeetleLegList[name] = value;
                }

                DigiBeetleHandList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/hands/hand"))
                {
                    var name = x.Attributes["name"].Value;
                    var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                    DigiBeetleHandList[name] = value;
                }

                DigiBeetleArmList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/arms/arm"))
                {
                    var name = x.Attributes["name"].Value;
                    var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                    DigiBeetleArmList[name] = value;
                }

                DigiBeetleDeviceList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/digi-beetle-parts/devices/device"))
                {
                    var name = x.Attributes["name"].Value;
                    var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                    DigiBeetleDeviceList[name] = value;
                }

                ItemList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/items/item"))
                {
                    var name = x.Attributes["name"].Value;
                    var value = Convert.ToUInt16(x.Attributes["value"].Value, 16);
                    ItemList.Add(new BinaryStruct(name, value));
                }

                ImportantItemList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/important-items/important-item"))
                {
                    var name = x.Attributes["name"].Value;
                    ImportantItemList.Add(name);
                }

                GameFlagNameList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/game-flags/names/flag"))
                {
                    var address = Convert.ToUInt32(x.Attributes["address"].Value, 16);
                    var name = x.Attributes["name"].Value;
                    GameFlagNameList[address] = name;
                }

                XmlDocument xml2 = new XmlDocument();
                xml.Load("Resources\\" + mode + "\\data.xml");
                DigimonList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/digimons/digimon"))
                {
                    var name = x.Attributes["name"].Value;
                    var value = Convert.ToByte(x.Attributes["id"].Value, 16);
                    DigimonList.Add(new BinaryStruct(name, value));
                }

                TechList.Clear();
                foreach (XmlNode x in xml.SelectNodes("dw2-utility/techs/tech"))
                {
                    var name = x.Attributes["name"].Value;
                    var value = Convert.ToByte(x.Attributes["id"].Value, 16);
                    TechList.Add(new BinaryStruct(name, value));
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
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

}
