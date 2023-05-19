using dw2_exp_multiplier.Entity;
using System.Collections.Generic;


namespace dw2_exp_multiplier.Manager
{
    public class UnmoveableEnemy : IEnemysetModifier
    {
        public static readonly string TOOLTIP = "The wild Digimons will not move from their places.\n";

        public override void Patch(ref List<Enemyset> enemysetList)
        {
            foreach (var enemyset in enemysetList)
            {
                if (!IEnemysetModifier.IsBossSet(enemyset))
                    enemyset.Move = 0x07;
            }
        }
    }

}
