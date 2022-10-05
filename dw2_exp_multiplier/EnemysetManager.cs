using System;
using System.Collections.Generic;
using System.IO;

/*
 * @author acemon33
 */

namespace dw2_exp_multiplier
{
    public class EnemysetManager
    {
        public static void ReadFile(string filename, ref List<Enemyset> enemysetList)
        {
            byte[] buffer = File.ReadAllBytes(filename);
            var length = buffer.Length / Enemyset.LENGTH;
    
            byte[] temp = new byte[Enemyset.LENGTH];
            for (var i = 0; i < length; i++)
            {
                Buffer.BlockCopy(buffer, i * temp.Length, temp, 0, temp.Length);
                enemysetList.Add(new Enemyset(temp));
            }
        }
    
        public static void WriteFile(string filename, ref List<Enemyset> enemysetList)
        {
            byte[] buffer = new byte[enemysetList.Count * Enemyset.LENGTH];
            for (var i = 0; i < enemysetList.Count; i++)
            {
                Buffer.BlockCopy(enemysetList[i].ToArray(), 0, buffer, i * Enemyset.LENGTH, Enemyset.LENGTH);
            }
            File.WriteAllBytes(filename, buffer);
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
            BinaryReader br = new BinaryReader(File.OpenRead(filename));
            byte[] buffer = PsxSector.ReadSector(ref br,146074, 10);
            br.Close();
            br.Dispose();
            
            var length = buffer.Length / Enemyset.LENGTH;
    
            byte[] temp = new byte[Enemyset.LENGTH];
            for (var i = 0; i < length; i++)
            {
                Buffer.BlockCopy(buffer, i * temp.Length, temp, 0, temp.Length);
                if (temp[0] == 0) break;
                enemysetList.Add(new Enemyset(temp));
            }
        }
        
    }
}
