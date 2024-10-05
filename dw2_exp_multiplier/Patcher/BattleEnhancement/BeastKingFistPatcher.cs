using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class BeastKingFistPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "The damage is 1.8 on Counter-Attack.\n";

        private byte[] data;

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));

            byte[] patchedPattern =
            {
                0x12, 0x00, 0x04, 0x34,
                0x18, 0x00, 0x44, 0x00,
                0x0A, 0x00, 0x12, 0x34,
                0x12, 0x20, 0x00, 0x00,
                0x1A, 0x00, 0x92, 0x00,
                0x12, 0x90, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7CC0, patchedPattern.Length);

            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
    }
    
}
