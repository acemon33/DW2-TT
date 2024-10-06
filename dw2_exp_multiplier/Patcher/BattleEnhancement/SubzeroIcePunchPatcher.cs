using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class SubzeroIcePunchPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Every hit increases 7 AP.\n";

        private byte[] data;

        public override string GetName() { return ""; }

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));

            byte[] patchedPattern = { 0x07, 0x00, 0x62, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7E48, patchedPattern.Length);

            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
    }
    
}
