using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleFix
{
    public class ChronoBreakerPatcher : IPatcher
    {
        public static readonly string TOOLTIP = ".\n";

        private byte[] data;

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));

            byte[] patchedPattern = { 0xC8, 0xD0, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8EBC, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0xCD, 0xD0, 0x01, 0x08,
                0x20, 0x80, 0x02, 0x3C
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x5530, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0x30, 0x01, 0x44, 0x24,
                0x00, 0x00, 0x82, 0x90,
                0xA5, 0x00, 0x03, 0x24,
                0x03, 0x00, 0x62, 0x14,
                0x00, 0x00, 0x00, 0x00,
                0xFF, 0xA2, 0x01, 0x08,
                0x00, 0x00, 0x80, 0xA0,
                0x9D, 0xB9, 0x01, 0x0C,
                0x00, 0x00, 0x04, 0x24,
                0x26, 0xA2, 0x01, 0x08,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10FD4, patchedPattern.Length);

            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
    }
    
}
