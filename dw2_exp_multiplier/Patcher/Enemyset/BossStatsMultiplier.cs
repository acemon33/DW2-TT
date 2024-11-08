﻿using dw2_exp_multiplier.Entity;
using System;
using System.Collections.Generic;


namespace dw2_exp_multiplier.Patcher
{
    public class BossStatsMultiplier : IEnemysetModifier
    {
        private decimal Multiplier;

        public BossStatsMultiplier(decimal multiplier)
        {
            this.Multiplier = multiplier;
        }

        public override void Patch(ref List<Enemyset> enemysetList)
        {
            foreach (var enemyset in enemysetList)
            {
                if (IEnemysetModifier.IsBossSet(enemyset))
                {
                    foreach (var enemy in enemyset.Enemy)
                    {
                        var i = enemy.Hp * this.Multiplier;
                        enemy.Hp = (i > UInt16.MaxValue) ? UInt16.MaxValue : Convert.ToUInt16(i);

                        i = enemy.Mp * this.Multiplier;
                        enemy.Mp = (i > UInt16.MaxValue) ? UInt16.MaxValue : Convert.ToUInt16(i);

                        i = enemy.Attack * this.Multiplier;
                        enemy.Attack = (i > byte.MaxValue) ? byte.MaxValue : Convert.ToByte(i);

                        i = enemy.Defence * this.Multiplier;
                        enemy.Defence = (i > UInt16.MaxValue) ? UInt16.MaxValue : Convert.ToUInt16(i);

                        i = enemy.Speed * this.Multiplier * Convert.ToDecimal(0.75);
                        if (i < enemy.Speed) i = enemy.Speed;
                        enemy.Speed = Convert.ToByte(i);
                    }
                }
            }
        }
    }

}
