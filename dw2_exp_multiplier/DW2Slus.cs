using System;
using System.IO;

/*
 * @author acemon33
 */

namespace dw2_exp_multiplier
{
    public class DW2Slus
    {
        public static readonly int LBA_OFFSET = 0xDC80;
        public static readonly int NUMBER_OF_SECTOR = 318;

        private static byte[] slus;
        private static UInt32[] lba = new UInt32[3675];
        private static UInt16[] size = new UInt16[3675];
        
        public static bool ValidImageFile(string filename)
        {
                // SLUS_011.93 in bytes
            byte[] dw2Id = { 0x53, 0x4C, 0x55, 0x53, 0x5F, 0x30, 0x31, 0x31, 0x2E, 0x39, 0x33 };
            byte[] buffer = new byte[11];
            var br = new BinaryReader(File.OpenRead(filename));
            br.BaseStream.Position = 0xCAB9;
            br.BaseStream.Read(buffer, 0, 11);
            
            for (int i = 0; i < 11; i++) if (buffer[i] != dw2Id[i]) return false;
            
            br.BaseStream.Position = DW2Slus.LBA_OFFSET;
            slus = PsxSector.ReadSector(ref br, DW2Slus.NUMBER_OF_SECTOR);
            br.Close();
            br.Dispose();
            
            Buffer.BlockCopy(slus, 0x33F94, lba, 0, 0x396B);
            Buffer.BlockCopy(slus, 0x37900, size, 0, 0x1CB5);
            
            return true;
        }

        public static UInt32 GetLba(int index)
        {
            return lba[index]; 
        }
        
        public static UInt16 GetSize(int index)
        {
            return size[index]; 
        }
        
    }
    
}
