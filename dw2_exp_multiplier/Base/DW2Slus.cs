using System;
using System.IO;
using System.Reflection;

/*
 * @author acemon33
 */

namespace dw2_exp_multiplier.Base
{
    public class DW2Slus
    {
        private static readonly int LBA_OFFSET = 0xDC80;
        private static readonly int NUMBER_OF_SECTOR = 318;

        private static readonly int US_NUMBER_OF_INDEX = 3675;
        private static readonly int US_LBA_OFFSET = 0x33F94;
        private static readonly int US_LBA_LENGTH = 0x396B;
        private static readonly int US_SIZE_OFFSET = 0x37900;
        private static readonly int US_SIZE_LENGTH = 0x1CB5;

        private static readonly int JAP_NUMBER_OF_INDEX = 3655;
        private static readonly int JAP_LBA_OFFSET = 0x316A8;
        private static readonly int JAP_LBA_LENGTH = 0x391B;
        private static readonly int JAP_SIZE_OFFSET = 0x34FC4;
        private static readonly int JAP_SIZE_LENGTH = 0x1C8D;

        public static readonly int US_VERSION = 1;
        public static readonly int JAP_VERSION = 2;

        private byte[] slus;
        private UInt32[] lba;
        private UInt16[] size;

        private int number_of_index;
        private int lba_offset;
        private int lba_length;
        private int size_offset;
        private int size_length;
        private int version;

        public DW2Slus(byte[] data, int version)
        {
            if (version == DW2Slus.US_VERSION)
            {
                this.version = US_VERSION;
                this.lba = new UInt32[US_NUMBER_OF_INDEX];
                this.size = new UInt16[US_NUMBER_OF_INDEX];
                this.number_of_index = US_NUMBER_OF_INDEX;
                this.lba_offset = US_LBA_OFFSET;
                this.lba_length = US_LBA_LENGTH;
                this.size_offset = US_SIZE_OFFSET;
                this.size_length = US_SIZE_LENGTH;
            }
            else
            {
                this.version = JAP_VERSION;
                this.lba = new UInt32[JAP_NUMBER_OF_INDEX];
                this.size = new UInt16[JAP_NUMBER_OF_INDEX];
                this.number_of_index = JAP_NUMBER_OF_INDEX;
                this.lba_offset = JAP_LBA_OFFSET;
                this.lba_length = JAP_LBA_LENGTH;
                this.size_offset = JAP_SIZE_OFFSET;
                this.size_length = JAP_SIZE_LENGTH;
            }

            this.slus = data;

            this.lba = new UInt32[this.number_of_index];
            this.size = new UInt16[this.number_of_index];
            Buffer.BlockCopy(this.slus, this.lba_offset, this.lba, 0, this.lba_length);
            Buffer.BlockCopy(this.slus, this.size_offset, this.size, 0, this.size_length);
        }

        public static DW2Slus ValidImageFile(ref FileStream br, int preOffset, int version)
        {
            byte[] dw2Id = { 0x53, 0x4C, 0x55, 0x53, 0x5F, 0x30, 0x31, 0x31, 0x2E, 0x39, 0x33 }; // SLUS_011.93 in bytes
            byte[] buffer = new byte[dw2Id.Length];

            br.Position = 0xCAB9 + preOffset;

            if (version == 0)
                throw new FileLoadException("The file: \"" + br.Name + "\" is not Digimon World 2 Image File!!", "DW2 invalid file");

            br.Position = DW2Slus.LBA_OFFSET + preOffset;
            return new(PsxSector.ReadSector(ref br, DW2Slus.NUMBER_OF_SECTOR), version);
        }

        public UInt32 GetLba(int index)
        {
            return lba[index]; 
        }
        
        public UInt16 GetSize(int index)
        {
            return size[index]; 
        }
        
        public void SetLba(int index, UInt32 value)
        {
            lba[index] = value; 
        }
        
        public void SetSize(int index, UInt16 value)
        {
            size[index] = value; 
        }
        
        public static void UnhideAAAFolder(ref FileStream br, bool hid = false)
        {
            byte[] unHidPattern = { 0x32, 0x00, 0x56, 0x01, 0x00, 0x00, 0x00, 0x00, 0x01,
                0x56, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x64, 0x05, 0x0C,
                0x17, 0x08, 0x26, 0x24, 0x02, 0x00, 0x00, 0x01, 0x00, 0x00, 0x01, 0x03,
                0x41, 0x41, 0x41, 0x00, 0x00, 0x00, 0x00, 0x8D, 0x55, 0x58, 0x41 };
            byte[] hidPattern = new byte[unHidPattern.Length];
            byte[] currentPattern;
            
            br.Position = 0xCB42;

            currentPattern = (!hid) ? unHidPattern : hidPattern;
            
            br.Write(currentPattern, 0,currentPattern.Length);
        }

        public static void UnhideAAAFolderJap(ref FileStream br, bool hid = false)
        {
            byte[] unHidPattern =
            {
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x32, 0x00, 0x2D, 0x50, 0x00, 0x00, 0x00, 0x00, 0x50, 0x2D,
                0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x64, 0x05, 0x0C, 0x17, 0x08, 0x26, 0x24, 0x02,
                0x00, 0x00, 0x01, 0x00, 0x00, 0x01, 0x03, 0x41, 0x41, 0x41, 0x00, 0x00, 0x00, 0x00, 0x8D, 0x55,
                0x58, 0x41
            };
            byte[] hidPattern = new byte[unHidPattern.Length];
            byte[] currentPattern;
            
            br.Position = 0xCC82;

            currentPattern = (!hid) ? unHidPattern : hidPattern;
            
            br.Write(currentPattern, 0,currentPattern.Length);
        }

        public int GetVersion() { return version; }

        public void Update(ref FileStream fs, int preOffset)
        {
            Buffer.BlockCopy(this.lba, 0, this.slus, this.lba_offset, this.lba_length);
            Buffer.BlockCopy(this.size, 0, this.slus, this.size_offset, this.size_length);
            fs.Position = DW2Slus.LBA_OFFSET + preOffset;
            PsxSector.WriteSector(ref fs, ref this.slus, DW2Slus.NUMBER_OF_SECTOR);
        }
    }
    
}
