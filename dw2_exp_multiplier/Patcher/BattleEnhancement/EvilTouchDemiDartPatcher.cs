using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class EvilTouchDemiDartPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Increasing Lowering MP Effect.\n";

        private byte[] data;

        public override string GetName() { return ""; }

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));

            byte[] patchedPattern = { 0x08, 0x00, 0x42, 0x30, 0x14, 0x00, 0x42, 0x20 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x84D8, patchedPattern.Length);

            patchedPattern = new byte[] { 0x15, 0x00, 0x42, 0x30, 0x32, 0x00, 0x42, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8524, patchedPattern.Length);

            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
    }
    
}
