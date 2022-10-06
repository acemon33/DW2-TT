using System;
using System.IO;
using System.Windows.Forms;

/*
 * @author acemon33
 */

namespace dw2_exp_multiplier
{
    public class Stmapdat
    {
        private const int INDEX = 3369;
        
        public static byte[] ReadFile(string filename)
        {
            try
            {
                return File.ReadAllBytes(filename);
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
            return null;
        }

        public static bool WriteFile(string filename, ref byte[] buffer)
        {
            try
            {
                File.WriteAllBytes(filename, buffer);
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
            return false;
        }
        
        public static byte[] ReadBin(string filename)
        {
            BinaryReader br = null;
            try
            {
                br = new BinaryReader(File.OpenRead(filename));
                return PsxSector.ReadSector(ref br, DW2Slus.GetLba(Stmapdat.INDEX), DW2Slus.GetSize(Stmapdat.INDEX));
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
            }
            return null;
        }
        
        public static bool WriteBin(string filename, ref byte[] buffer)
        {
            BinaryWriter br = null;
            try
            {
                byte[] temp = new byte[DW2Slus.GetSize(Stmapdat.INDEX) * PsxSector.SECTOR];
                Buffer.BlockCopy(buffer, 0, temp, 0, buffer.Length);
                br = new BinaryWriter(File.OpenWrite(filename));   
                PsxSector.WriteSector(ref br, ref temp, DW2Slus.GetLba(Stmapdat.INDEX), DW2Slus.GetSize(Stmapdat.INDEX));
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
            }
            return false;
        }

        public static byte[] Read(int index)
        {
            string filename;
            switch (index)
            {
                case 1: filename = "STMAPDAT-VANILLA.BIN"; break;
                case 2: filename = "STMAPDAT-DEBUG.BIN"; break;
                case 3: filename = "STMAPDAT-EXIT.BIN"; break;
                default: filename = "STMAPDAT-VANILLA.BIN"; break;
            }
            return Stmapdat.ReadFile(filename);
        }
    }
}
