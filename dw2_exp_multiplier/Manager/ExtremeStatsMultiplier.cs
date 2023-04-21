using dw2_exp_multiplier.Entity;
using System;
using System.Collections.Generic;


namespace dw2_exp_multiplier.Manager
{
    public class ExtremeStatsMultiplier : IEnemysetModifier
    {
        private decimal Multiplier;

        public ExtremeStatsMultiplier(decimal multiplier)
        {
            this.Multiplier = multiplier;
        }

        public override void Patch(ref List<Enemyset> enemysetList)
        {
            foreach (var enemyset in enemysetList)
            {
                if (enemyset.Move == 0x37 || enemyset.Move == 0x1A)
                {
                    foreach (var enemy in enemyset.Enemy)
                    {
                        var i = enemy.Hp * this.Multiplier;
                        enemy.Hp = (i > UInt16.MaxValue) ? UInt16.MaxValue : Convert.ToUInt16(i);

                        i = enemy.Mp * this.Multiplier;
                        enemy.Mp = (i > UInt16.MaxValue) ? UInt16.MaxValue : Convert.ToUInt16(i);

                        i = enemy.Attack * this.Multiplier;
                        enemy.Attack = (i > byte.MaxValue) ? byte.MaxValue : Convert.ToByte(i);

                        i = enemy.Defence * (this.Multiplier * 2);
                        enemy.Defence = (i > UInt16.MaxValue) ? UInt16.MaxValue : Convert.ToUInt16(i);

                        i = enemy.Speed * (this.Multiplier / 2);
                        enemy.Speed = (i > byte.MaxValue) ? byte.MaxValue : Convert.ToByte(i);
                    }
                }
            }
        }
    }

}
