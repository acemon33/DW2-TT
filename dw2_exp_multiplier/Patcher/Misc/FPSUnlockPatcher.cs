using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class FPSUnlockPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Experimental: Unlock the game 30 FPS.\nUsage:\n- Enable:  Press L1 + R1 buttons at any time\n- Disable: Press L2 + R2 buttons at any time";

        private byte[] data;

        public FPSUnlockPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "FPS Unlock Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            if (data == null)
                data = this.DW2Image.ReadMainFile();

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                patchBtyesUS(ref fs);
            else
                patchBtyesJAP(ref fs);
        }

        public override bool ValidateBytes()
        {
            if (data == null)
                data = this.DW2Image.ReadMainFile();

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                return ValidateBytesUS();
            else
                return ValidateBytesJAP();
        }

        private void patchBtyesUS(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x38, 0x27, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x13C7C, patchedPattern.Length);

            patchedPattern = new byte[] {
                0x05, 0x80, 0x02, 0x3C, 0xAB, 0xF6, 0x42, 0x34, 0x00, 0x00, 0x42, 0x90, 0xF3, 0x00, 0x03, 0x34,
                0x03, 0x00, 0x43, 0x14, 0xFC, 0x00, 0x03, 0x34, 0x02, 0x80, 0x02, 0x3C, 0xB8, 0x34, 0x40, 0xAC,
                0x04, 0x00, 0x43, 0x14, 0x02, 0x80, 0x03, 0x3C, 0x40, 0x14, 0x02, 0x3C, 0x1B, 0x00, 0x42, 0x24,
                0xB8, 0x34, 0x62, 0xAC, 0x08, 0x00, 0xE0, 0x03, 0x00, 0x00, 0x00, 0x00,
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x3A4E0, patchedPattern.Length);

            this.DW2Image.WriteMainFile(ref data);
        }

        private bool ValidateBytesUS()
        {
            byte[] bytes = { 0x08, 0x00, 0xE0, 0x03 };
            for (int i = 0, j = 0x13C7C; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }

            for (int i = 0, j = 0x3A4E0; i < 0x3C; i++)
            {
                if (data[j + i] != 0)
                    return false;
            }
            return true;
        }

        private void patchBtyesJAP(ref FileStream fs)
        {
            byte[] patchedPattern = { 0xAD, 0x26, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x3120, patchedPattern.Length);

            patchedPattern = new byte[] {
                0x05, 0x80, 0x02, 0x3C, 0x0B, 0x1B, 0x42, 0x34, 0x00, 0x00, 0x42, 0x90, 0xF3, 0x00, 0x03, 0x34,
                0x03, 0x00, 0x43, 0x14, 0xFC, 0x00, 0x03, 0x34, 0x01, 0x80, 0x02, 0x3C, 0x5C, 0x29, 0x40, 0xAC,
                0x04, 0x00, 0x43, 0x14, 0x01, 0x80, 0x03, 0x3C, 0x40, 0x14, 0x02, 0x3C, 0x1B, 0x00, 0x42, 0x24,
                0x5C, 0x29, 0x62, 0xAC, 0x08, 0x00, 0xE0, 0x03, 0x00, 0x00, 0x00, 0x00,
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x3A2B4, patchedPattern.Length);

            this.DW2Image.WriteMainFile(ref data);
        }

        private bool ValidateBytesJAP()
        {
            byte[] bytes = { 0x08, 0x00, 0xE0, 0x03 };
            for (int i = 0, j = 0x3120; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }

            for (int i = 0, j = 0x3A2B4; i < 0x3C; i++)
            {
                if (data[j + i] != 0)
                    return false;
            }
            return true;
        }
    }
    
}
