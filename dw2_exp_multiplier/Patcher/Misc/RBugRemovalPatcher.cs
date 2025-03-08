using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class RBugRemovalPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Prevent R-Bug to be spot on the Map.\n";

        private byte[] data;

        public RBugRemovalPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "R-Bug Removal Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            if (data == null)
                data = this.DW2Image.ReadFile(FileIndex.STAG4000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
            {
                ValidateBytesUS();
                patchBtyesUS(ref fs);
            }
            else
            {
                ValidateBytesJAP();
                patchBtyesJAP(ref fs);
            }
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
            byte[] patchedPattern = { 0xD2, 0xCC, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xA1F4, patchedPattern.Length);
            
            patchedPattern = new byte[]
            {
                0x0B, 0x00, 0x02, 0x24,
                0x02, 0x00, 0x62, 0x15,
                0x00, 0x00, 0x02, 0x24,
                0x00, 0x00, 0x02, 0xAD,
                0x57, 0xB5, 0x01, 0x08,
                0x08, 0x00, 0x0B, 0xA1
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xFFE8, patchedPattern.Length);
            
            this.DW2Image.WriteFile(ref data, FileIndex.STAG4000_PRO);
        }

        private bool ValidateBytesUS()
        {
            byte[] bytes =
            {
                0x08, 0x00, 0x0B, 0xA1
            };

            for (int i = 0, j = 0xA1F4; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }

            for (int i = 0, j = 0xFFE8; i < 0x18; i++)
            {
                if (data[j + i] != 0)
                    return false;
            }
            return true;
        }

        private void patchBtyesJAP(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x48, 0xCC, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xDB44, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0x0B, 0x00, 0x02, 0x24,
                0x02, 0x00, 0x62, 0x15,
                0x00, 0x00, 0x02, 0x24,
                0x00, 0x00, 0x02, 0xAD,
                0x21, 0xC3, 0x01, 0x08,
                0x08, 0x00, 0x0B, 0xA1
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xFFE8, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG4000_PRO);
        }

        private bool ValidateBytesJAP()
        {
            byte[] bytes =
            {
                0x08, 0x00, 0x0B, 0xA1
            };

            for (int i = 0, j = 0xDB44; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }

            for (int i = 0, j = 0xFFE8; i < 0x18; i++)
            {
                if (data[j + i] != 0)
                    return false;
            }
            return true;
        }
    }
    
}
