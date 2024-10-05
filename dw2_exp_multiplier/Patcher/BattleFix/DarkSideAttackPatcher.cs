using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleFix
{
    public class DarkSideAttackPatcher : IPatcher
    {
        public static readonly string TOOLTIP = ".\n";

        private byte[] data;

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));

            byte[] patchedPattern =
            {
                0x07, 0x80, 0x02, 0x3C,
                0xC0, 0x3C, 0x45, 0x24,
                0xBC, 0xD0, 0x01, 0x08,
                0x21, 0x10, 0xC5, 0x02
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x843C, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0x4F, 0x03, 0x43, 0x90,
                0x00, 0x00, 0x00, 0x00,
                0x02, 0x00, 0x62, 0x30,
                0x04, 0x00, 0x40, 0x14,
                0x14, 0x00, 0x83, 0x86,
                0x14, 0x00, 0x82, 0x96,
                0x25, 0xAC, 0x01, 0x08,
                0x23, 0x90, 0x64, 0x00,
                0x16, 0x00, 0x82, 0x96,
                0x16, 0x00, 0x83, 0x86,
                0x25, 0xAC, 0x01, 0x08,
                0x23, 0x90, 0x64, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10F90, patchedPattern.Length);

            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
    }
    
}
