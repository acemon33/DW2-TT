using System;
using System.Collections.Generic;
using System.IO;
using dw2_exp_multiplier.Base;
using dw2_exp_multiplier.Entity;


namespace dw2_exp_multiplier.Mapper
{
    public class EnemysetMapper
    {
        public static bool ReadFile(string filename, ref List<Enemyset> enemysetList)
        {
            byte[] buffer = File.ReadAllBytes(filename);
            EnemysetMapper.Read(ref buffer, ref enemysetList);
            return true;
        }
    
        public static bool WriteFile(string filename, ref List<Enemyset> enemysetList)
        {
            byte[] buffer = EnemysetMapper.Write(enemysetList.Count * Enemyset.LENGTH, ref enemysetList);
            File.WriteAllBytes(filename, buffer);
            return true;
        }
        
        public static bool ReadBin(ref FileStream br, ref List<Enemyset> enemysetList)
        {
            byte[] buffer = PsxSector.ReadSector(ref br, DW2Slus.GetLba(FileIndex.ENEMYSET_BIN), DW2Slus.GetSize(FileIndex.ENEMYSET_BIN));
            
            EnemysetMapper.Read(ref buffer, ref enemysetList);
            
            return true;
        }
        
        public static bool WriteBin(ref FileStream br, ref List<Enemyset> enemysetList)
        {
            byte[] buffer = EnemysetMapper.Write(PsxSector.SECTOR * DW2Slus.GetSize(FileIndex.ENEMYSET_BIN), ref enemysetList);

            PsxSector.WriteSector(ref br, ref buffer, DW2Slus.GetLba(FileIndex.ENEMYSET_BIN), DW2Slus.GetSize(FileIndex.ENEMYSET_BIN));
            
            return true;
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
