using System;
using System.IO;

/*
 * @author acemon33
 */

namespace dw2_exp_multiplier.Base
{
    public class DW2Slus
    {
        public static readonly int LBA_OFFSET = 0xDC80;
        public static readonly int NUMBER_OF_SECTOR = 318;

        private static byte[] slus;
        private static UInt32[] lba = new UInt32[3675];
        private static UInt16[] size = new UInt16[3675];
        
        public static bool ValidImageFile(ref FileStream br)
        {
                // SLUS_011.93 in bytes
            byte[] dw2Id = { 0x53, 0x4C, 0x55, 0x53, 0x5F, 0x30, 0x31, 0x31, 0x2E, 0x39, 0x33 };
            byte[] buffer = new byte[dw2Id.Length];
            
            br.Position = 0xCAB9;
            br.Read(buffer, 0, dw2Id.Length);
            
            for (int i = 0; i < dw2Id.Length; i++) if (buffer[i] != dw2Id[i]) throw new FileLoadException("The file: \"" + br.Name + "\" is not Digimon World 2 Image File!!", "DW2 invalid file");
            
            br.Position = DW2Slus.LBA_OFFSET;
            slus = PsxSector.ReadSector(ref br, DW2Slus.NUMBER_OF_SECTOR);

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
    }
    
}
