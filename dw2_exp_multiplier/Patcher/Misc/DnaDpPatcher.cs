using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class DnaDpPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Device Dome's lab will sum-up the DP(s) instead of adding 1.\n";

        private byte[] data;

        public DnaDpPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "DNA DP Patcher";  }

        public override void Patch(ref FileStream fs)
        {
            if (data == null)
                data = this.DW2Image.ReadFile(FileIndex.STAG2000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                patchBtyesUS(ref fs);
            else
                patchBtyesJAP(ref fs);
        }

        public override bool ValidateBytes()
        {
            if (data == null)
                data = this.DW2Image.ReadFile(FileIndex.STAG2000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                return ValidateBytesUS();
            else
                return ValidateBytesJAP();
        }

        private void patchBtyesUS(ref FileStream fs)
        {
            byte[] patchedPattern =
            {
                0x06, 0x80, 0x02, 0x3C,
                0x90, 0xF7, 0x49, 0x8C,
                0x15, 0x03, 0x02, 0x24,
                0x0E, 0x00, 0xC4, 0x92,
                0xEC, 0xC2, 0x01, 0x08,
                0x0E, 0x00, 0xA3, 0x92,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x1508, patchedPattern.Length);    // ram: 80064874

            patchedPattern = new byte[]
            {
                0x07, 0x00, 0x22, 0x11,
                0x00, 0x00, 0x00, 0x00,
                0x2B, 0x10, 0x64, 0x00,
                0x02, 0x00, 0x40, 0x14,
                0x01, 0x00, 0x82, 0x24,
                0x01, 0x00, 0x62, 0x24,
                0x21, 0x92, 0x01, 0x08,
                0x00, 0x00, 0x00, 0x00,
                0x03, 0x00, 0x60, 0x14,
                0x00, 0x00, 0x00, 0x00,
                0x21, 0x92, 0x01, 0x08,
                0x01, 0x00, 0x82, 0x24,
                0x02, 0x00, 0x80, 0x14,
                0x00, 0x00, 0x00, 0x00,
                0x01, 0x00, 0x04, 0x24,
                0x21, 0x92, 0x01, 0x08,
                0x21, 0x10, 0x83, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xD850, patchedPattern.Length);    // ram: 80070bb0

            this.DW2Image.WriteFile(ref data, FileIndex.STAG2000_PRO);
        }

        private bool ValidateBytesUS()
        {
            byte[] bytes =
            {
                0x0E, 0x00, 0xC4, 0x92,
                0x0E, 0x00, 0xA3, 0x92,
                0x00, 0x00, 0x00, 0x00,
                0x2B, 0x10, 0x64, 0x00,
                0x02, 0x00, 0x40, 0x14,
                0x01, 0x00, 0x82, 0x24,
                0x01, 0x00, 0x62, 0x24
            };

            for (int i = 0, j = 0x1508; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            return true;
        }

        private void patchBtyesJAP(ref FileStream fs)
        {
            byte[] patchedPattern =
            {
                0x05, 0x80, 0x02, 0x3C,
                0xF0, 0x1B, 0x49, 0x8C,
                0x15, 0x03, 0x02, 0x24,
                0x0E, 0x00, 0xC4, 0x92,
                0xEC, 0xC2, 0x01, 0x08,
                0x0E, 0x00, 0xA3, 0x92,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x3BEC, patchedPattern.Length);

            patchedPattern = new byte[]
            {
    0x01, 0x00, 0x82, 0x24, 0x01, 0x00, 0x62, 0x24, 0x50, 0x9B, 0x01, 0x08, 0x00, 0x00, 0x00, 0x00,
    0x03, 0x00, 0x60, 0x14, 0x00, 0x00, 0x00, 0x00, 0x50, 0x9B, 0x01, 0x08, 0x01, 0x00, 0x82, 0x24,
    0x02, 0x00, 0x80, 0x14, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x04, 0x24, 0x50, 0x9B, 0x01, 0x08,
    0x21, 0x10, 0x83, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xD850, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG2000_PRO);
        }

        private bool ValidateBytesJAP()
        {
            byte[] bytes =
            {
                0x0E, 0x00, 0xC4, 0x92,
                0x0E, 0x00, 0xA3, 0x92,
                0x00, 0x00, 0x00, 0x00,
                0x2B, 0x10, 0x64, 0x00,
                0x02, 0x00, 0x40, 0x14,
                0x01, 0x00, 0x82, 0x24,
                0x01, 0x00, 0x62, 0x24
            };

            for (int i = 0, j = 0x3BEC; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            return true;
        }
    }
    
}
