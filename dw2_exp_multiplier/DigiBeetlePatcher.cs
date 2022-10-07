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
        private const int INDEX = 410;

        public static bool patch(string filename, UInt16 digibeetleId)
        {
            BinaryWriter bw = null;
            BinaryReader br = null;
            try
            {
                br = new BinaryReader(File.OpenRead(filename));
                byte[] data = PsxSector.ReadSector(ref br, DW2Slus.GetLba(DigiBeetlePatcher.INDEX), DW2Slus.GetSize(DigiBeetlePatcher.INDEX));
                br.Close();
                
                byte[] patchedPattern = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF8, 0x01, 0x10, 0x24 };
                Buffer.BlockCopy(BitConverter.GetBytes(digibeetleId), 0, patchedPattern, 8, 2);
                byte[] defaultPattern = { 0x2B, 0x8F, 0x01, 0x08, 0x00, 0x00, 0x00, 0x00, 0xF8, 0x01, 0x10, 0x24 };
                byte[] currentPattern = (digibeetleId == 0) ? defaultPattern : patchedPattern;
                
                Buffer.BlockCopy(currentPattern, 0, data, 0x940, currentPattern.Length);
                bw = new BinaryWriter(File.OpenWrite(filename));
                byte[] temp = new byte[DW2Slus.GetSize(DigiBeetlePatcher.INDEX) * PsxSector.SECTOR];
                Buffer.BlockCopy(data, 0, temp, 0, data.Length);
                PsxSector.WriteSector(ref bw, ref temp, DW2Slus.GetLba(DigiBeetlePatcher.INDEX), DW2Slus.GetSize(DigiBeetlePatcher.INDEX));
                bw.Close();
                
                return true;
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("The File \"" + filename + "\" is not found", "File Error");
            }
            catch (IOException ex)
            {
                MessageBox.Show("The File \"" + filename + "\" is being used by anothe program", "File Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "File Error");
            }
            finally
            {
                if (br != null)
                {
                    br.Close();
                    br.Dispose();
                }
                if (bw != null)
                {
                    bw.Close();
                    bw.Dispose();
                }
            }
            return false;
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
