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
            data = this.DW2Image.ReadFile(FileIndex.STAG2000_PRO);

            ValidateBytes();

            patchBtyes(ref fs);
        }

        private void patchBtyes(ref FileStream fs)
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

        private void ValidateBytes()
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
                    throw new Exception(GetName());
            }
        }
    }
    
}
