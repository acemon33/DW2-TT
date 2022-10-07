using System;
using System.IO;

/*
 * @author acemon33
 */

namespace dw2_exp_multiplier
{
    public class PsxSector
    {
        public static readonly int SECTOR = 2352;
        public static readonly int DATA_SECTOR = 2048;

        public static byte[] ReadSector(ref FileStream br, Int64 offset, int numberOfSector)
        {
            br.Position = offset * PsxSector.SECTOR;
            byte[] data = new byte[PsxSector.DATA_SECTOR * numberOfSector];
            for (int i = 0; i < numberOfSector; i++)
            {
                br.Position += 24;
                br.Read(data, i * PsxSector.DATA_SECTOR, PsxSector.DATA_SECTOR);
                br.Position += 280;
            }
            return data;
        }
        
        public static byte[] ReadSector(ref FileStream br, int numberOfSector)
        {
            byte[] data = new byte[PsxSector.DATA_SECTOR * numberOfSector];
            for (int i = 0; i < numberOfSector; i++)
            {
                br.Position += 24;
                br.Read(data, i * PsxSector.DATA_SECTOR, PsxSector.DATA_SECTOR);
                br.Position += 280;
            }
            return data;
        }
        
        public static void WriteSector(ref FileStream bw, ref byte[] data, Int64 offset, int numberOfSector)
        {
            bw.Position = offset * PsxSector.SECTOR;
            for (int i = 0; i < numberOfSector; i++)
            {
                bw.Position += 24;
                bw.Write(data, i * PsxSector.DATA_SECTOR, PsxSector.DATA_SECTOR);
                bw.Position += 280;
            }
        }
    }
    
}
