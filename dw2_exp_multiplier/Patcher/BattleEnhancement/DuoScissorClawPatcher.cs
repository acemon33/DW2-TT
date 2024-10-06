using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class DuoScissorClawPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "The defense reduction is applied after the damage is dealt.\n";

        private byte[] data;

        public override string GetName() { return ""; }

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));

            byte[] patchedPattern = { 0xC8, 0xD0, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x79D4, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0x78, 0x42, 0x62, 0xA0,
                0x74, 0x42, 0x66, 0xAC,
                0x70, 0x42, 0x65, 0xAC,
                0x4F, 0xAB, 0x01, 0x08,
                0x6C, 0x42, 0x64, 0xAC
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10FC0, patchedPattern.Length);

            patchedPattern = new byte[] { 0xA4, 0xD0, 0x01, 0x08, 0x12, 0x48, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x81FC, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0x07, 0x80, 0x04, 0x3C,
                0x78, 0x42, 0x88, 0x90,
                0x5C, 0x00, 0xA2, 0xAF,
                0x60, 0x00, 0xA9, 0xAF,
                0x05, 0x00, 0x00, 0x11,
                0x78, 0x42, 0x80, 0xA0,
                0x74, 0x42, 0x86, 0x8C,
                0x70, 0x42, 0x85, 0x8C,
                0x5A, 0xAA, 0x01, 0x0C,
                0x6C, 0x42, 0x84, 0x8C,
                0x5C, 0x00, 0xA2, 0x8F,
                0x60, 0x00, 0xA9, 0x8F,
                0x59, 0xAD, 0x01, 0x08,
                0x40, 0x20, 0x12, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10F30, patchedPattern.Length);

            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
    }
    
}
