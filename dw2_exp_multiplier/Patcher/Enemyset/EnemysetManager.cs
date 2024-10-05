using System.Collections.Generic;
using System.IO;
using dw2_exp_multiplier.Entity;
using dw2_exp_multiplier.Mapper;


namespace dw2_exp_multiplier.Patcher
{
    public class EnemysetManager
    {
        private List<IEnemysetModifier> ModifierList = new List<IEnemysetModifier>();
        private List<Enemyset> EnemysetList = new List<Enemyset>();

        public EnemysetManager(string filepath)
        {
            EnemysetMapper.ReadFile(filepath, ref this.EnemysetList);
        }

        public EnemysetManager(ref FileStream br)
        {
            EnemysetMapper.ReadBin(ref br, ref this.EnemysetList);
        }

        public void AddModifier(IEnemysetModifier enemysetModifier)
        {
            this.ModifierList.Add(enemysetModifier);
        }

        public void ApplyModifiers()
        {
            foreach (var modifier in this.ModifierList)
                modifier.Patch(ref this.EnemysetList);
        }

        public void WrtieToFile(string filepath)
        {
            EnemysetMapper.WriteFile(filepath, ref this.EnemysetList);
        }

        public void WriteToBin(ref FileStream br)
        {
            EnemysetMapper.WriteBin(ref br, ref this.EnemysetList);
        }
    }
    
}
