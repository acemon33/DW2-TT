using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleFix
{
    public class InvincibilityPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Correct the behavior when being Attacked by Beast King Fist Tech.\n";

        private byte[] data;

        public InvincibilityPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Invincibility Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            if (data == null)
                data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                patchBtyesUS(ref fs);
            else
                patchBtyesJAP(ref fs);
        }

        public override bool ValidateBytes()
        {
            if (data == null)
                data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                return ValidateBytesUS();
            else
                return ValidateBytesJAP();
        }

        private void patchBtyesUS(ref FileStream fs)
        {
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

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private bool ValidateBytesUS()
        {
            byte[] bytes = { 0xB8, 0x02, 0x82, 0x94, 0x16, 0x00, 0x83, 0x86 };
            for (int i = 0, j = 0x7CB8; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }

            for (int i = 0, j = 0x10EE4; i < 0x2c; i++)
            {
                if (data[j + i] != 0)
                    return false;
            }
            return true;
        }

        private void patchBtyesJAP(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x1C, 0xD0, 0x01, 0x08, 0x07, 0x80, 0x02, 0x3C };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xAF7C, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0xE8, 0x35, 0x43, 0x24, 0x21, 0x10, 0xC3, 0x02, 0x4F, 0x03, 0x43, 0x90,
                0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x62, 0x30, 0x03, 0x00, 0x40, 0x14,
                0xB8, 0x02, 0x82, 0x94, 0x2F, 0xB8, 0x01, 0x08, 0x16, 0x00, 0x83, 0x86,
                0x3B, 0xB8, 0x01, 0x08, 0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10F38, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private bool ValidateBytesJAP()
        {
            byte[] bytes = { 0xB8, 0x02, 0x82, 0x94, 0x16, 0x00, 0x83, 0x86 };
            for (int i = 0, j = 0xAF7C; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }

            for (int i = 0, j = 0x10F38; i < 0x2c; i++)
            {
                if (data[j + i] != 0)
                    return false;
            }
            return true;
        }
    }

}
