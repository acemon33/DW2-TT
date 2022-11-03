using System;
using DigimonWorld2Tool.Utility;


namespace dw2_exp_multiplier.Entity
{
    public class Digimon
    {
        
        public static readonly int LENGTH = 0x5C;
        
        public byte[] info = new byte[0x5C];

        public Digimon(byte[] data)
        {
            Buffer.BlockCopy(data, 0, this.info, 0, this.info.Length);
        }
        
        public Byte[] ToArray()
        {
            return info;
        }
    }
    
    public class SaveSlot
    {
        public static readonly int LENGTH = 0x1058;
        
        public byte locationId1;
        public byte []padding1 = new byte[3];
        public byte time1;
        public byte time2;
        public byte time3;
        public byte time4;
        public Int32 bits;
        public byte[] padding2 = new byte[5];
        public byte locationId2;
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
        public Digimon[] digimon = new Digimon[36];
        public ushort padding7;
        public ushort[] server_item = new ushort[236];
        public ushort[] important_item = new ushort[28];
        public byte[] padding8 = new byte[32];
        public byte[] may_game_flag2 = new byte[17];
        public byte[] padding9 = new byte[13];
        public byte[] may_game_flag3 = new byte[32];
        public byte story_progress;
        public byte[] padding10 = new byte[19];

        public string stringHeroName;

        public SaveSlot(byte[] data)
        {
            this.locationId1 = data[0];
            Buffer.BlockCopy(data, 1, this.padding1, 0, this.padding1.Length);
            this.time1 = data[4];
            this.time2 = data[5];
            this.time3 = data[6];
            this.time4 = data[7];
            this.bits = BitConverter.ToInt32(data, 8);
            Buffer.BlockCopy(data, 12, this.padding2, 0, this.padding2.Length);
            this.locationId2 = data[17];
            this.rank = data[18];
            this.padding3 = data[19];
            Buffer.BlockCopy(data, 20, this.heroName, 0, this.heroName.Length);
            Buffer.BlockCopy(data, 26, this.padding4, 0, this.padding4.Length);
            Buffer.BlockCopy(data, 36, this.unknown, 0, this.unknown.Length);
            Buffer.BlockCopy(data, 44, this.digi_beetle_part, 0, this.digi_beetle_part.Length * 2);
            Buffer.BlockCopy(data, 82, this.digi_beetle_part_flag, 0, this.digi_beetle_part_flag.Length);
            Buffer.BlockCopy(data, 102, this.items, 0, this.items.Length * 2);
            Buffer.BlockCopy(data, 198, this.may_game_flag1, 0, this.may_game_flag1.Length);
            Buffer.BlockCopy(data, 203, this.padding5, 0, this.padding5.Length);
            Buffer.BlockCopy(data, 209, this.digi_beetle_name, 0, this.digi_beetle_name.Length);
            Buffer.BlockCopy(data, 217, this.padding6, 0, this.padding6.Length);

            byte[] temp = new byte[Digimon.LENGTH];
            for (int i = 0; i < this.digimon.Length; i++)
            {
                Buffer.BlockCopy(data, i * Digimon.LENGTH + 228, temp, 0, temp.Length);
                this.digimon[i] = new Digimon(temp);
            }
            
            this.padding7 = BitConverter.ToUInt16(data, 3540);
            Buffer.BlockCopy(data, 3542, this.server_item, 0, this.server_item.Length * 2);
            Buffer.BlockCopy(data, 4014, this.important_item, 0, this.important_item.Length * 2);
            Buffer.BlockCopy(data, 4070, this.padding8, 0, this.padding8.Length);
            Buffer.BlockCopy(data, 4102, this.may_game_flag2, 0, this.may_game_flag2.Length);
            Buffer.BlockCopy(data, 4119, this.padding9, 0, this.padding9.Length);
            Buffer.BlockCopy(data, 4132, this.may_game_flag3, 0, this.may_game_flag3.Length);
            this.story_progress = data[4164];
            Buffer.BlockCopy(data, 4165, this.padding10, 0, this.padding10.Length);

            this.stringHeroName = TextConversion.DigiStringToASCII(this.heroName);
        }
        
        public Byte[] ToArray()
        {
            byte[] data = new byte[0x3FFF];
            return data;
        }
    }
    
    public class SaveFile
    {
        public Int32[] header = new Int32[131];
        public SaveSlot[] saveSlot = new SaveSlot[3];
        public Int32[] padding = new Int32[826];
        public ushort checksum1;
        public ushort checksum2;

        public SaveFile(byte[] data)
        {
            Buffer.BlockCopy(data, 0, this.header, 0, this.header.Length * 4);
            
            byte[] temp = new byte[SaveSlot.LENGTH];
            for (int i = 0; i < 3; i++)
            {
                Buffer.BlockCopy(data, i * SaveSlot.LENGTH + (this.header.Length * 4), temp, 0, temp.Length);
                this.saveSlot[i] = new SaveSlot(temp);
            }
            
            Buffer.BlockCopy(data, 0, this.padding, 0, this.padding.Length);
            this.checksum1 = BitConverter.ToUInt16(data, 0x3FFC);
            this.checksum2 = BitConverter.ToUInt16(data, 0x3FFE);
        }

        public Byte[] ToArray()
        {
            byte[] data = new byte[0x3FFF];
            
            Buffer.BlockCopy(this.header, 0, data, 0, this.header.Length * 4);
            
            for (int i = 0; i < 3; i++)
            {
                Buffer.BlockCopy(this.saveSlot[i].ToArray(), 0, data,  i * SaveSlot.LENGTH + (this.header.Length * 4), SaveSlot.LENGTH);
            }
            
            Buffer.BlockCopy(data, 0, this.padding, 0, this.padding.Length);
            Buffer.BlockCopy(data, 0x3FFC, BitConverter.GetBytes(checksum1), 0, 2);
            Buffer.BlockCopy(data, 0x3FFE, BitConverter.GetBytes(checksum1), 0, 2);
            
            return data;
        }
        
    }

}
