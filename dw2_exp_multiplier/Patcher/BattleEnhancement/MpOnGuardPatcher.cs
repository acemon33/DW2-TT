using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class MpOnGaurdPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Ratio of increasing MP during Guard is 25%.\n";

        private byte[] data;

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));

            byte[] patchedPattern =
            {
                0x04, 0x00, 0x05, 0x24,
                0x1B, 0x00, 0x85, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x12, 0x18, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x32, 0x00, 0xC2, 0x94,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x9774, patchedPattern.Length);

            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
    }
    
}
