using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class EnemyBossConfusionPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Enemy Boss can be affected by Confusion Effect.\n";

        private byte[] data;

        public override string GetName() { return ""; }

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));

            byte[] patchedPattern = { 0xA8, 0xAD, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8328, patchedPattern.Length);

            patchedPattern = new byte[] { 0x15, 0xA9, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x70DC, patchedPattern.Length);

            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
    }
    
}
