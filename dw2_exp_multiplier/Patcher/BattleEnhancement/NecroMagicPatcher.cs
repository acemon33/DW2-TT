using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class NecroMagicPatcher : IPatcher
    {
        public static readonly string TOOLTIP = ".\n";

        private byte[] data;

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));

            byte[] patchedPattern = { 0xC8, 0x00, 0x05, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x83CC, patchedPattern.Length);

            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
    }
    
}
