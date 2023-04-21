using dw2_exp_multiplier.Entity;
using System.Collections.Generic;


namespace dw2_exp_multiplier.Manager
{
    public abstract class IEnemysetModifier
    {
        abstract public void Patch(ref List<Enemyset> enemysetList);
    }

}
