using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

/*
 * @author acemon33
 */

namespace dw2_exp_multiplier
{
    public class DigiBeetlePatcher
    {
        public static bool patch(ref FileStream fs, UInt16 digibeetleId)
        {
            byte[] data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));
            
            byte[] patchedPattern = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF8, 0x01, 0x10, 0x24 };
            Buffer.BlockCopy(BitConverter.GetBytes(digibeetleId), 0, patchedPattern, 8, 2);
            byte[] defaultPattern = { 0x2B, 0x8F, 0x01, 0x08, 0x00, 0x00, 0x00, 0x00, 0xF8, 0x01, 0x10, 0x24 };
            byte[] currentPattern = (digibeetleId == 0) ? defaultPattern : patchedPattern;
            
            Buffer.BlockCopy(currentPattern, 0, data, 0x940, currentPattern.Length);
            byte[] temp = new byte[DW2Slus.GetSize(FileIndex.STAG4000_PRO) * PsxSector.SECTOR];
            Buffer.BlockCopy(data, 0, temp, 0, data.Length);
            PsxSector.WriteSector(ref fs, ref temp, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));
            
            return true;
        }

        public static Dictionary<string, string> getDigiBeetleIds(ref Dictionary<string, Bitmap> imageList, string filename)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            list.Add("-1", "");
            list.Add("00", "Default");
            using (ZipArchive archive = ZipFile.OpenRead(filename))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    int tokenPosition = entry.Name.IndexOf("-");
                    string id = entry.Name.Substring(0, tokenPosition);

                    if (entry.Name.IndexOf(".") < 1) list.Add(id, entry.Name.Substring(tokenPosition + 1));
                    else list.Add(id, entry.Name.Substring(tokenPosition + 1, entry.Name.IndexOf(".") - tokenPosition - 1));

                    imageList.Add(id, new Bitmap(entry.Open()));
                }
            }
            return list;
        }
    }
}
