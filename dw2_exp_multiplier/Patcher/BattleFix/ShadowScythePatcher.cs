using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleFix
{
    public class ShadowScythePatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Correct the behavior when being Interrupted.\n";

        private byte[] data;

        public ShadowScythePatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Shadow Scythe Patcher"; }

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
            byte[] patchedPattern = { 0x21, 0x30, 0x03, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x9524, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private bool ValidateBytesUS()
        {
            byte[] bytes = { 0x21, 0x30, 0x60, 0x02 };
            for (int i = 0, j = 0x9524; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            return true;
        }

        private void patchBtyesJAP(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x21, 0x30, 0x03, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xC7E8, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private bool ValidateBytesJAP()
        {
            byte[] bytes = { 0x21, 0x30, 0x60, 0x02 };
            for (int i = 0, j = 0xC7E8; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            return true;
        }
    }

}
