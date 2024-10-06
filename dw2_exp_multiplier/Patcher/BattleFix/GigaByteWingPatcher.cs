using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleFix
{
    public class GigaByteWingPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Target can't Recover Status even the random chance to cure the status.\n";

        private byte[] data;

        public override string GetName() { return ""; }

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));

            byte[] patchedPattern =
            {
                0x07, 0x80, 0x02, 0x3C,
                0xB2, 0xD0, 0x01, 0x08,
                0xC0, 0x3C, 0x50, 0x24
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x6730, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0x21, 0x10, 0x90, 0x02,
                0x4F, 0x03, 0x42, 0x90,
                0xB8, 0x31, 0x90, 0x24,
                0x04, 0x00, 0x42, 0x30,
                0x03, 0x00, 0x40, 0x14,
                0x07, 0x80, 0x02, 0x3C,
                0xA7, 0xA6, 0x01, 0x08,
                0xC8, 0x31, 0x51, 0x24,
                0xEB, 0xA6, 0x01, 0x08,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10F68, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0x9C, 0xD0, 0x01, 0x08,
                0x2E, 0x00, 0x43, 0x94
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7550, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0x00, 0x00, 0x00, 0x00,
                0x02, 0x00, 0x76, 0x28,
                0x03, 0x00, 0xC0, 0x12,
                0x2C, 0x00, 0x43, 0x94,
                0x2E, 0xAA, 0x01, 0x08,
                0x01, 0x00, 0x16, 0x34,
                0x33, 0xAA, 0x01, 0x08,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10F10, patchedPattern.Length);

            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
    }
    
}
