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

            data[0] = this.locationId1;
            Buffer.BlockCopy(this.padding1, 0, data, 1, this.padding1.Length);
            data[4] = this.time1;
            data[5] = this.time2;
            data[6] = this.time3;
            data[7] = this.time4;
            Buffer.BlockCopy(BitConverter.GetBytes(this.bits), 0, data, 8, 4);
            Buffer.BlockCopy(this.padding2, 0, data, 12, this.padding2.Length);
            data[17] = this.locationId2;
            data[18] = this.rank;
            data[19] = this.padding3;
            
            this.heroName = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            var temp = TextConversion.ASCIIToDigiString(this.stringHeroName);
            Buffer.BlockCopy(temp, 0, this.heroName, 0, temp.Length);
            Buffer.BlockCopy(this.heroName, 0, data, 20, this.heroName.Length);
            
            Buffer.BlockCopy(this.padding4, 0, data, 26, this.padding4.Length);
            Buffer.BlockCopy(this.unknown, 0, data, 36, this.unknown.Length);
            Buffer.BlockCopy(this.digi_beetle_part, 0, data, 44, this.digi_beetle_part.Length * 2);
            Buffer.BlockCopy(this.digi_beetle_part_flag, 0, data, 82, this.digi_beetle_part_flag.Length);
            Buffer.BlockCopy(this.items, 0, data, 102, this.items.Length * 2);
            Buffer.BlockCopy(this.may_game_flag1, 0, data, 198, this.may_game_flag1.Length);
            Buffer.BlockCopy(this.padding5, 0, data, 203, this.padding5.Length);
            Buffer.BlockCopy(this.digi_beetle_name, 0, data, 209, this.digi_beetle_name.Length);
            Buffer.BlockCopy(this.padding6, 0, data, 217, this.padding6.Length);
            for (int i = 0; i < this.digimon.Length; i++)
            {
                Buffer.BlockCopy(this.digimon[i].ToArray(), 0, data,  i * Digimon.LENGTH + 228, Digimon.LENGTH);
            }
            Buffer.BlockCopy(BitConverter.GetBytes(this.padding7), 0, data, 3540, 2);
            Buffer.BlockCopy(this.server_item, 0, data, 3542, this.server_item.Length * 2);
            Buffer.BlockCopy(this.important_item, 0, data, 4014, this.important_item.Length * 2);
            Buffer.BlockCopy(this.padding8, 0, data, 4070, this.padding8.Length);
            Buffer.BlockCopy(this.may_game_flag2, 0, data, 4102, this.may_game_flag2.Length);
            Buffer.BlockCopy(this.may_game_flag3, 0, data, 4132, this.may_game_flag3.Length);
            data[4164] = this.story_progress;
            Buffer.BlockCopy(padding10, 0, data, 4165, padding10.Length);

            return data;
        }
    }
    
    public class SaveFile
    {
        public static readonly int SAVE_SLOT_COUNT = 3;
        
        public Int32[] header = new Int32[131];
        public SaveSlot[] saveSlot = new SaveSlot[SAVE_SLOT_COUNT];
        public Int32[] padding = new Int32[826];
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
            Buffer.BlockCopy(BitConverter.GetBytes(checksum2), 0, data, 0x3FFE, 2);
            
            return data;
        }

        public void CalculateChecksum()
        {
            var data = this.ToArray();
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
            this.checksum2 = b;
        }

    }

}
