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
    
        public static byte[] ReadSector(ref BinaryReader br, Int64 offset, int numberOfSector)
        {
            br.BaseStream.Position = offset * PsxSector.SECTOR;
            byte[] data = new byte[PsxSector.DATA_SECTOR * numberOfSector];
            for (int i = 0; i < numberOfSector; i++)
            {
                br.BaseStream.Position += 24;
                br.BaseStream.Read(data, i * PsxSector.DATA_SECTOR, PsxSector.DATA_SECTOR);
                br.BaseStream.Position += 280;
            }
            return data;
        }
        
        public static void WriteSector(ref BinaryWriter bw, ref byte[] data, Int64 offset, int numberOfSector)
        {
            bw.BaseStream.Position = offset;
            for (int i = 0; i < numberOfSector; i++)
            {
                bw.BaseStream.Position += 24;
                bw.BaseStream.Write(data, i * PsxSector.DATA_SECTOR, PsxSector.DATA_SECTOR);
                bw.BaseStream.Position += 280;
            }
        }
    }
}
