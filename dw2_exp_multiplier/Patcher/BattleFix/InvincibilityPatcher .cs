using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleFix
{
    public class InvincibilityPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Correct the behavior when being Attacked by Beast King Fist Tech.\n";

        private byte[] data;

        public override string GetName() { return ""; }

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));

            byte[] patchedPattern = { 0x91, 0xD0, 0x01, 0x08, 0x07, 0x80, 0x02, 0x3C };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7CB8, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0xC0, 0x3C, 0x43, 0x24,
                0x21, 0x10, 0xC3, 0x02,
                0x4F, 0x03, 0x43, 0x90,
                0x00, 0x00, 0x00, 0x00,
                0x08, 0x00, 0x62, 0x30,
                0x03, 0x00, 0x40, 0x14,
                0xB8, 0x02, 0x82, 0x94,
                0x08, 0xAC, 0x01, 0x08,
                0x16, 0x00, 0x83, 0x86,
                0x14, 0xAC, 0x01, 0x08,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10EE4, patchedPattern.Length);

            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
    }
    
}
