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
        
        public static long WriteSectorAtEnd(ref FileStream bw, ref byte[] data, int numberOfSector)
        {
            bw.Position = bw.Length;
            Int64 offset = bw.Length / PsxSector.SECTOR;

            byte[] temp = new byte[numberOfSector * PsxSector.SECTOR];      // guarantee to fit data into a sector
            byte[] temp2 = new byte[numberOfSector * PsxSector.SECTOR];
            Buffer.BlockCopy(data, 0, temp2, 0, data.Length);
            byte[] header = new byte[0x10] {
                0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x02
            };
            for (long i = 0, total = bw.Position; i < numberOfSector; i++, total += PsxSector.SECTOR)
            {
                Buffer.BlockCopy(header, 0, temp, (int)(i * PsxSector.SECTOR), header.Length);
                long LBA = total / PsxSector.SECTOR;

                byte minutes = Convert.ToByte(((byte)(((LBA + 150) / 75) / 60)).ToString(), 16);
                temp[0xC + (i * PsxSector.SECTOR)] = minutes;

                byte seconds = Convert.ToByte(((byte)(((LBA + 150) / 75) % 60)).ToString(), 16);
                temp[0xD + (i * PsxSector.SECTOR)] = seconds;

                byte sectors = Convert.ToByte(((byte)(((LBA + 150) % 75) % 60)).ToString(), 16);
                temp[0xE + (i * PsxSector.SECTOR)] = sectors;

                Buffer.BlockCopy(temp2, (int)(i * PsxSector.DATA_SECTOR), temp, (int)(i * PsxSector.SECTOR) + 24, PsxSector.DATA_SECTOR);
            }
            bw.Write(temp, 0, temp.Length);
            return offset;
        }

        public static void WriteSector(ref FileStream bw, ref byte[] data, int numberOfSector)
        {
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
