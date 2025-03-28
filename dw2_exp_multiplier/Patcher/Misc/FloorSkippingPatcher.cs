using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class FloorSkippingPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Allow the Player to jump to the specified floor in the domain\nUsage:\n- press x on the floor gate\n- when dialog appeares, don't close it, press as the following:\n  - R1 to increase floor\n  - L1 to decrease floor\n";

        private byte[] data;

        public FloorSkippingPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Floor Skipping Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            if (data == null)
                data = this.DW2Image.ReadFile(FileIndex.STAG4000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                patchBtyesUS(ref fs);
            else
                patchBtyesJAP(ref fs);
        }

        public override bool ValidateBytes()
        {
            if (data == null)
                data = this.DW2Image.ReadFile(FileIndex.STAG4000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                return ValidateBytesUS();
            else
                return ValidateBytesJAP();
        }

        private void patchBtyesUS(ref FileStream fs)
        {
            byte[] patchedPattern = { 0xB6, 0xCC, 0x01, 0x08, 0x18, 0x00, 0xBF, 0x8F };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x43DC, patchedPattern.Length);
            
            patchedPattern = new byte[]
            {
                0x10, 0xF7, 0x90, 0x8E,
                0xA3, 0xD5, 0x91, 0x82,
                0x04, 0x00, 0x00, 0x1A,
                0x18, 0xF7, 0x90, 0x8E,
                0x01, 0x00, 0x31, 0x26,
                0xC1, 0xCC, 0x01, 0x08,
                0xA3, 0xD5, 0x91, 0xA2,
                0x00, 0x00, 0x00, 0x00,
                0x0E, 0x00, 0x00, 0x1A,
                0xFF, 0xFF, 0x31, 0x26,
                0xA3, 0xD5, 0x91, 0xA2,
                0x01, 0x00, 0x31, 0x26,
                0x0A, 0x00, 0x10, 0x24,
                0x1B, 0x00, 0x30, 0x02,
                0x12, 0x80, 0x00, 0x00,
                0x10, 0x88, 0x00, 0x00,
                0x03, 0x00, 0x00, 0x12,
                0xED, 0xE3, 0x90, 0xA2,
                0xCD, 0xCC, 0x01, 0x08,
                0xEE, 0xE3, 0x91, 0xA2,
                0xED, 0xE3, 0x91, 0xA2,
                0xFF, 0x00, 0x10, 0x34,
                0xEE, 0xE3, 0x90, 0xA2,
                0xD1, 0x9D, 0x01, 0x08,
                0x14, 0x00, 0xB1, 0x8F,
                0xEE, 0xE3, 0x90, 0xA2,
                0xD1, 0x9D, 0x01, 0x08,
                0x14, 0x00, 0xB1, 0x8F
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xFF78, patchedPattern.Length);
            
            this.DW2Image.WriteFile(ref data, FileIndex.STAG4000_PRO);
        }

        private bool ValidateBytesUS()
        {
            byte[] bytes =
            {
                0x18, 0x00, 0xBF, 0x8F,
                0x14, 0x00, 0xB1, 0x8F
            };

            for (int i = 0, j = 0x43DC; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }

            for (int i = 0, j = 0xFF78; i < 0x70; i++)
            {
                if (data[j + i] != 0)
                    return false;
            }
            return true;
        }

        private void patchBtyesJAP(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x2C, 0xCC, 0x01, 0x08, 0x18, 0x00, 0xBF, 0x8F };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x324C, patchedPattern.Length);

            patchedPattern = new byte[]
            {
    0x70, 0x1B, 0x90, 0x8E, 0x2B, 0x1D, 0x91, 0x82, 0x04, 0x00, 0x00, 0x1A, 0x78, 0x1B, 0x90, 0x8E,
    0x01, 0x00, 0x31, 0x26, 0x37, 0xCC, 0x01, 0x08, 0x2B, 0x1D, 0x91, 0xA2, 0x00, 0x00, 0x00, 0x00,
    0x0E, 0x00, 0x00, 0x1A, 0xFF, 0xFF, 0x31, 0x26, 0x2B, 0x1D, 0x91, 0xA2, 0x01, 0x00, 0x31, 0x26,
    0x0A, 0x00, 0x10, 0x24, 0x1B, 0x00, 0x30, 0x02, 0x12, 0x80, 0x00, 0x00, 0x10, 0x88, 0x00, 0x00,
    0x03, 0x00, 0x00, 0x12, 0x73, 0x2B, 0x90, 0xA2, 0x43, 0xCC, 0x01, 0x08, 0x74, 0x2B, 0x91, 0xA2,
    0x73, 0x2B, 0x91, 0xA2, 0xFF, 0x00, 0x10, 0x34, 0x74, 0x2B, 0x90, 0xA2, 0xE3, 0x98, 0x01, 0x08,
    0x14, 0x00, 0xB1, 0x8F
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xFF78, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG4000_PRO);
        }

        private bool ValidateBytesJAP()
        {
            byte[] bytes =
            {
                0x18, 0x00, 0xBF, 0x8F,
                0x14, 0x00, 0xB1, 0x8F
            };

            for (int i = 0, j = 0x324C; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }

            for (int i = 0, j = 0xFF78; i < 0x70; i++)
            {
                if (data[j + i] != 0)
                    return false;
            }
            return true;
        }
    }

}
