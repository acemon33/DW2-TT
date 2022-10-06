using System.IO;

/*
 * @author acemon33
 */

namespace dw2_exp_multiplier
{
    public class DW2Slus
    {
        public static bool ValidImageFile(string filename)
        {
                // SLUS_011.93 in bytes
            byte[] dw2Id = { 0x53, 0x4C, 0x55, 0x53, 0x5F, 0x30, 0x31, 0x31, 0x2E, 0x39, 0x33 };
            byte[] buffer = new byte[11];
            using (var br = new BinaryReader(File.OpenRead(filename)))
            {
                br.BaseStream.Position = 0xCAB9;
                br.BaseStream.Read(buffer, 0, 11);
            }
            for (int i = 0; i < 11; i++) if (buffer[i] != dw2Id[i]) return false;
            return true;
        }
        
    }
    
}
