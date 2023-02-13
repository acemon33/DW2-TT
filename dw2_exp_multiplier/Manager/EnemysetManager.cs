using System;
using System.Collections.Generic;
using dw2_exp_multiplier.Entity;

/*
 * @author acemon33
 */

namespace dw2_exp_multiplier.Manager
{
    public class EnemysetManager
    {
        public static void MultiplyExpBits(Decimal multiplier, ref List<Enemyset> enemysetList)
        {
            foreach (var enemyset in enemysetList)
            {
                foreach (var enemy in enemyset.Enemy)
                {
                    var i = enemy.Exp * multiplier;
                    enemy.Exp = ( i > UInt16.MaxValue) ? UInt16.MaxValue : Convert.ToUInt16(i);   
                    i = enemy.Bits * multiplier;
                    enemy.Bits = ( i > UInt16.MaxValue) ? UInt16.MaxValue : Convert.ToUInt16(i);   
                }
            }
        }
        
        public static void MultiplyBossStats(Decimal multiplier, ref List<Enemyset> enemysetList)
        {
            foreach (var enemyset in enemysetList)
            {
                if (enemyset.Move == 0x37 || enemyset.Move == 0x1A)
                {
                    foreach (var enemy in enemyset.Enemy)
                    {
                        var i = enemy.Hp * multiplier;
                        enemy.Hp = (i > UInt16.MaxValue) ? UInt16.MaxValue : Convert.ToUInt16(i);

                        i = enemy.Mp * multiplier;
                        enemy.Mp = (i > UInt16.MaxValue) ? UInt16.MaxValue : Convert.ToUInt16(i);

                        i = enemy.Attack * multiplier;
                        enemy.Attack = (i > byte.MaxValue) ? byte.MaxValue : Convert.ToByte(i);

                        i = enemy.Defence * multiplier;
                        enemy.Defence = (i > UInt16.MaxValue) ? UInt16.MaxValue : Convert.ToUInt16(i);

                        i = enemy.Speed * (multiplier / 2);
                        enemy.Speed = (i > byte.MaxValue) ? byte.MaxValue : Convert.ToByte(i);
                    }
                }
            }
        }
        
        public static void MultiplyExtremeStats(Decimal multiplier, ref List<Enemyset> enemysetList)
        {
            foreach (var enemyset in enemysetList)
            {
                if (enemyset.Move == 0x37 || enemyset.Move == 0x1A)
                {
                    foreach (var enemy in enemyset.Enemy)
                    {
                        var i = enemy.Hp * multiplier;
                        enemy.Hp = (i > UInt16.MaxValue) ? UInt16.MaxValue : Convert.ToUInt16(i);

                        i = enemy.Mp * multiplier;
                        enemy.Mp = (i > UInt16.MaxValue) ? UInt16.MaxValue : Convert.ToUInt16(i);

                        i = enemy.Attack * multiplier;
                        enemy.Attack = (i > byte.MaxValue) ? byte.MaxValue : Convert.ToByte(i);

                        i = enemy.Defence * (multiplier * 2);
                        enemy.Defence = (i > UInt16.MaxValue) ? UInt16.MaxValue : Convert.ToUInt16(i);

                        i = enemy.Speed * (multiplier / 2);
                        enemy.Speed = (i > byte.MaxValue) ? byte.MaxValue : Convert.ToByte(i);
                    }
                }
            }
        }
    }
    
}
