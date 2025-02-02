using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class DuoScissorClawPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "The defense reduction is applied after the damage is dealt.\n";

        private byte[] data;

        public DuoScissorClawPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Debuff Defence Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.JAP_VERSION)
                throw new NotImplementedException(GetName() + " Not Supported in JAP version");

            data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            ValidateBytesUS();
            patchBtyesUS(ref fs);
        }

        private void patchBtyesUS(ref FileStream fs)
        {
            byte[] patchedPattern = { 0xC8, 0xD0, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x79D4, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0xB3, 0x40, 0x73, 0xA0,
                0x40, 0x42, 0x66, 0xAC,
                0x3C, 0x42, 0x65, 0xAC,
                0x4F, 0xAB, 0x01, 0x08,
                0x38, 0x42, 0x64, 0xAC
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10FC0, patchedPattern.Length);

            patchedPattern = new byte[] { 0xA4, 0xD0, 0x01, 0x08, 0x12, 0x48, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x81FC, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0x07, 0x80, 0x04, 0x3C,
                0xB3, 0x40, 0x88, 0x90,
                0x5C, 0x00, 0xA2, 0xAF,
                0x60, 0x00, 0xA9, 0xAF,
                0x05, 0x00, 0x00, 0x11,
                0xB3, 0x40, 0x80, 0xA0,
                0x40, 0x42, 0x86, 0x8C,
                0x3C, 0x42, 0x85, 0x8C,
                0x5A, 0xAA, 0x01, 0x0C,
                0x38, 0x42, 0x84, 0x8C,
                0x5C, 0x00, 0xA2, 0x8F,
                0x60, 0x00, 0xA9, 0x8F,
                0x59, 0xAD, 0x01, 0x08,
                0x40, 0x20, 0x12, 0x00
            };

            Buffer.BlockCopy(patchedPattern, 0, data, 0x10F30, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private void ValidateBytesUS()
        {
            byte[] bytes = { 0x5A, 0xAA, 0x01, 0x0C };
            for (int i = 0, j = 0x79D4; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }

            for (int i = 0, j = 0x10FC0; i < 0x14; i++)
            {
                if (data[j + i] != 0)
                    throw new Exception(GetName());
            }

            bytes = new byte[] { 0x12, 0x48, 0x00, 0x00, 0x40, 0x20, 0x12, 0x00 };
            for (int i = 0, j = 0x81FC; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }

            for (int i = 0, j = 0x10F30; i < 0x38; i++)
            {
                if (data[j + i] != 0)
                    throw new Exception(GetName());
            }
        }
    }
    
}
