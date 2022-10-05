using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

/*
 * @author acemon33
 */

namespace dw2_exp_multiplier
{
    public class EnemysetManager
    {
        public static void ReadFile(string filename, ref List<Enemyset> enemysetList)
        {
            try
            {
                byte[] buffer = File.ReadAllBytes(filename);
                EnemysetManager.Read(ref buffer, ref enemysetList);
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
        }
    
        public static void WriteFile(string filename, ref List<Enemyset> enemysetList)
        {
            byte[] buffer = EnemysetManager.Write(enemysetList.Count * Enemyset.LENGTH, ref enemysetList);

            try
            {
                File.WriteAllBytes(filename, buffer);
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
        }
        
        public static void MultiplyExpBits(UInt16 multiplier, ref List<Enemyset> enemysetList)
        {
            foreach (var enemyset in enemysetList)
            {
                foreach (var enemy in enemyset.Enemy)
                {
                    var i = enemy.Exp * multiplier;
                    enemy.Exp = ( i < enemy.Exp) ? UInt16.MaxValue : Convert.ToUInt16(i);   
                    i = enemy.Bits * multiplier;
                    enemy.Bits = ( i < enemy.Bits) ? UInt16.MaxValue : Convert.ToUInt16(i);   
                }
            }
        }
        
        public static void ReadBin(string filename, ref List<Enemyset> enemysetList)
        {
            BinaryReader br = null;
            try
            {
                br = new BinaryReader(File.OpenRead(filename));
                byte[] buffer = PsxSector.ReadSector(ref br, 146074, 10);
                EnemysetManager.Read(ref buffer, ref enemysetList);
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
        }
        
        public static void WriteBin(string filename, ref List<Enemyset> enemysetList)
        {
            byte[] buffer = EnemysetManager.Write(PsxSector.SECTOR * 10, ref enemysetList);

            BinaryWriter br = null;
            try
            {
                br = new BinaryWriter(File.OpenWrite(filename));   
                PsxSector.WriteSector(ref br, ref buffer, 146074, 10);
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
        }

        private static void Read(ref byte[] buffer, ref List<Enemyset> enemysetList)
        {
            var length = buffer.Length / Enemyset.LENGTH;
            byte[] temp = new byte[Enemyset.LENGTH];
            for (var i = 0; i < length; i++)
            {
                Buffer.BlockCopy(buffer, i * temp.Length, temp, 0, temp.Length);
                if (temp[0] == 0) break;
                enemysetList.Add(new Enemyset(temp));
            }
        }

        private static byte[] Write(int size, ref List<Enemyset> enemysetList)
        {
            byte[] buffer = new byte[size];
            for (var i = 0; i < enemysetList.Count; i++)
            {
                Buffer.BlockCopy(enemysetList[i].ToArray(), 0, buffer, i * Enemyset.LENGTH, Enemyset.LENGTH);
            }
            return buffer;
        }
        
    }
}
