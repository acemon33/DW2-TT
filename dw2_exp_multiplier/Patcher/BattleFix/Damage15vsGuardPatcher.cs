using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleFix
{
    public class Damage15vsGuardPatcher : IPatcher
    {
        public static readonly string TOOLTIP = ".\n";

        private byte[] data;

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));

            byte[] patchedPattern = { 0x05, 0x00, 0x02, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7F04, patchedPattern.Length);

            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
    }
    
}
