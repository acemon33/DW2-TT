using System.Collections.Generic;
using System.IO;
using dw2_exp_multiplier.Base;
using dw2_exp_multiplier.Entity;
using dw2_exp_multiplier.Mapper;


namespace dw2_exp_multiplier.Patcher
{
    public class EnemysetManager
    {
        private List<IEnemysetModifier> ModifierList = new List<IEnemysetModifier>();
        private List<Enemyset> EnemysetList = new List<Enemyset>();
        private DW2Image dw2Image;

        public EnemysetManager(DW2Image dw2Image, string filepath)
        {
            EnemysetMapper.ReadFile(filepath, ref this.EnemysetList);
            this.dw2Image = dw2Image;
        }

        public EnemysetManager(ref FileStream br)
        {
            EnemysetMapper.ReadBin(this.dw2Image, ref this.EnemysetList);
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
            EnemysetMapper.WriteBin(this.dw2Image, ref this.EnemysetList);
        }
    }
    
}
