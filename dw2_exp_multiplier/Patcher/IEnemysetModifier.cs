using dw2_exp_multiplier.Entity;
using System.Collections.Generic;


namespace dw2_exp_multiplier.Patcher
{
    public abstract class IEnemysetModifier
    {
        public abstract void Patch(ref List<Enemyset> enemysetList);

        protected static bool IsBossSet(Enemyset set)
        {
            return set.Move == 0x37 || set.Move == 0x1A;
        }
    }

}
