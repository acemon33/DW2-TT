using System;
using System.IO;
using System.Reflection;

/*
 * @author acemon33
 */

namespace dw2_exp_multiplier
{
    public class Stmapdat
    {
        public static byte[] ReadFile(string filename)
        {
            return File.ReadAllBytes(filename);
        }

        public static bool WriteBin(ref FileStream br, ref byte[] buffer)
        {
            byte[] temp = new byte[DW2Slus.GetSize(FileIndex.STMAPDAT_BIN) * PsxSector.SECTOR];
            Buffer.BlockCopy(buffer, 0, temp, 0, buffer.Length);
            PsxSector.WriteSector(ref br, ref temp, DW2Slus.GetLba(FileIndex.STMAPDAT_BIN), DW2Slus.GetSize(FileIndex.STMAPDAT_BIN));
            return true;
        }

        public static byte[] Read(int index)
        {
            string filename;
            switch (index)
            {
                case 1: filename = "dw2_exp_multiplier.STMAPDAT-VANILLA.BIN"; break;
                case 2: filename = "dw2_exp_multiplier.STMAPDAT-DEBUG.BIN"; break;
                case 3: filename = "dw2_exp_multiplier.STMAPDAT-EXIT.BIN"; break;
                default: filename = "dw2_exp_multiplier.STMAPDAT-VANILLA.BIN"; break;
            }
            MemoryStream ms = new MemoryStream();
            Assembly.GetExecutingAssembly().GetManifestResourceStream(filename).CopyTo(ms);            
            return ms.ToArray();
        }
    }
}
