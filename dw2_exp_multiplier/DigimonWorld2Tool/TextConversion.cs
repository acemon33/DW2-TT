using System;
using System.Collections.Generic;
using System.Linq;


namespace DigimonWorld2Tool.Utility
{
    public class TextConversion
    {
        private static readonly Dictionary<string, string> CharacterLookupTable = new Dictionary<string, string>()
        {
            {"00", "0"},
            {"01", "1"},
            {"02", "2"},
            {"03", "3"},
            {"04", "4"},
            {"05", "5"},
            {"06", "6"},
            {"07", "7"},
            {"08", "8"},
            {"09", "9"},
            {"0A", "A"},
            {"0B", "B"},
            {"0C", "C"},
            {"0D", "D"},
            {"0E", "E"},
            {"0F", "F"},
            {"10", "G"},
            {"11", "H"},
            {"12", "I"},
            {"13", "J"},
            {"14", "K"},
            {"15", "L"},
            {"16", "M"},
            {"17", "N"},
            {"18", "O"},
            {"19", "P"},
            {"1A", "Q"},
            {"1B", "R"},
            {"1C", "S"},
            {"1D", "T"},
            {"1E", "U"},
            {"1F", "V"},
            {"20", "W"},
            {"21", "X"},
            {"22", "Y"},
            {"23", "Z"},
            {"24", "a"},
            {"25", "b"},
            {"26", "c"},
            {"27", "d"},
            {"28", "e"},
            {"29", "f"},
            {"2A", "g"},
            {"2B", "h"},
            {"2C", "i"},
            {"2D", "j"},
            {"2E", "k"},
            {"2F", "l"},
            {"30", "m"},
            {"31", "n"},
            {"32", "o"},
            {"33", "p"},
            {"34", "q"},
            {"35", "r"},
            {"36", "s"},
            {"37", "t"},
            {"38", "u"},
            {"39", "v"},
            {"3A", "w"},
            {"3B", "x"},
            {"3C", "y"},
            {"3D", "z"},
            {"3E", "○"},
            {"3F", "×"},
            {"40", "△"},
            {"41", "□"},
            {"42", "&"},
            {"44", "?"},
            {"45", "!"},
            {"46", "/"},
            {"49", "-"},
            {"47", "♪"},
            {"4F", "Ω"},
            {"54", ","},
            {"55", "."},
            {"56", "'"},
            {"57","\""},
            {"58", ";"},
            {"59", ":"},
            {"5A", "%"},
            {"5B", "+"},
            {"FD", " "},
            {"5D", "#"}, 
            {"5E", "õ"}, 
            {"5F", "ô"}, 
            {"60", "ó"}, 
            {"61", "í"}, 
            {"62", "é"}, 
            {"63", "ê"}, 
            {"64", "á"}, 
            {"65", "à"}, 
            {"66", "â"}, 
            {"67", "ã"}, 
            {"68", "ú"}, 
            {"69", "ç"},
            {"F000", "Name"},
            {"F006", "Digimon"},
            {"F007", "you"},
            {"F008", "the"},
            {"F009", "Digi-Bettle"},
            {"F00A", "Domain"},
            {"F00B", "Guard"},
            {"F00C", "Tamer"},
            {"F00D", "here"},
            {"F00E", "have"},
            {"F00F", "Knights"},
            {"F010", "and"},
            {"F011", "thing"},
            {"F012", "Security"},
            {"F013", "that"},
            {"F014", "Bertran"},
            {"F015", "Tournament"},
            {"F016", "Crimson"},
            {"F017", "Vendor"},
            {"F018", "something"},
            {"F019", "Item"},
            {"F01A", "Falcon"},
            {"F01B", "for"},
            {"F01C", "That's"},
            {"F01D", "Commander"},
            {"F01E", "Blood"},
            {"F01F", "Leader"},
            {"F020", "Attendant"},
            {"F021", "Cecilia"},
            {"F022", "all"},
            {"F023", "mission"},
            {"F024", "this"},
            {"F025", "MasterTyrannomon"},
            {"F026", "Archive"},
            {"F027", "Black"},
            {"F028", "I'll"},
            {"F029", "are"},
            {"F02A", "Sword"},
            {"F02B", "right"},
            {"F02C", "digivolve"},
            {"F02D", "enter"},
            {"F02E", "What"},
            {"F02F", "will"},
            {"F030", "come"},
            {"F031", "You"},
            {"F032", "Coliseum"},
            {"F033", "about"},
            {"F034", "don't"},
            {"F035", "anything"},
            {"F036", "Vandar"},
            {"F037", "Parts"},
            {"F038", "where"},
            {"F039", "The"},
            {"F03A", "know"},
            {"F03B", "Leomon"},
            {"F03C", "want"},
            {"F03D", "Oldman"},
            {"F03E", "like"},
            {"F03F", "need"},
            {"F040", "Chief"},
            {"F041", "with"},
            {"F042", "Thank"},
            {"F043", "strange"},
            {"F044", "Island"},
            {"F045", "can"},
            {"F046", "realy"},
            {"F047", "Blue"},
            {"F048", "time"},
        };

        private static string GetReplacementChar(string index, Dictionary<string, string> lookupDict, string modifier)
        {
            if (lookupDict.ContainsKey(index))
                return lookupDict[index];
            else
                return $"[{modifier} Unknown]";
        }

        public static string ByteArrayToHexString(byte[] data, char seperator = ' ')
        {
            string result = "";
            foreach (var item in data)
            {
                result += $"{item:X2}{seperator}";
            }
            return result;
        }

        public static string DigiStringToASCII(byte[] input)
        {
            if (input == null)
                return "No input data given";
            string converted = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == 0xFF) break;
                if (input[i] == 0xF0)
                {
                    string s1 = input[i++].ToString("X2");
                    string s2 = input[i].ToString("X2");
                    converted += GetReplacementChar(s1 + s2, CharacterLookupTable, "");
                }
                else
                    converted += GetReplacementChar(input[i].ToString("X2"), CharacterLookupTable, "");
            }
                
            return converted;
        }
        
        public static byte[] ASCIIToDigiString(string input)
        {
            byte[] data = new byte[input.Length];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Convert.ToByte(CharacterLookupTable.FirstOrDefault(o => o.Value == input[i].ToString()).Key, 16);
            }
            return data;
        }
        
    }
}
