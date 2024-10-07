using System;
using System.IO;

/*
 * @author acemon33
 */

namespace dw2_exp_multiplier.Base
{
    public class PsxSector
    {
        public static readonly int SECTOR = 2352;
        public static readonly int DATA_SECTOR = 2048;

        public static byte[] ReadSector(ref FileStream br, Int64 offset, int numberOfSector, int preOffset)
        {
            br.Position = (offset * PsxSector.SECTOR) + preOffset;
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
        
        public static void WriteSector(ref FileStream bw, ref byte[] data, Int64 offset, int numberOfSector, int preOffset)
        {
            bw.Position = (offset * PsxSector.SECTOR) + preOffset;
            byte[] temp = new byte[numberOfSector * PsxSector.SECTOR];      // guarantee to fit data into a sector
            Buffer.BlockCopy(data, 0, temp, 0, data.Length);
            for (int i = 0; i < numberOfSector; i++)
            {
                bw.Position += 24;
                bw.Write(temp, i * PsxSector.DATA_SECTOR, PsxSector.DATA_SECTOR);
                bw.Position += 280;
            }
        }
    }
    
}
