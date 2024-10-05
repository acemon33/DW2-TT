using dw2_exp_multiplier.Entity;
using System;
using System.Collections.Generic;


namespace dw2_exp_multiplier.Patcher
{
    public class ExpBitsMultiplier : IEnemysetModifier
    {
        private decimal Multiplier;

        public ExpBitsMultiplier(decimal multiplier)
        {
            this.Multiplier = multiplier;
        }

        public override void Patch(ref List<Enemyset> enemysetList)
        {
            foreach (var enemyset in enemysetList)
            {
                foreach (var enemy in enemyset.Enemy)
                {
                    var i = enemy.Exp * this.Multiplier;
                    enemy.Exp = (ushort)((i > Int16.MaxValue) ? Int16.MaxValue : Convert.ToInt16(i));
                    i = enemy.Bits * this.Multiplier;
                    enemy.Bits = (ushort)((i > Int16.MaxValue) ? Int16.MaxValue : Convert.ToInt16(i));
                }
            }
        }
    }

}
