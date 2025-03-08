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
            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.JAP_VERSION)
                throw new NotImplementedException(GetName() + " Not Supported in JAP version");

            data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            ValidateBytesUS();
            patchBtyesUS(ref fs);
        }

        public override bool ValidateBytes()
        {
            return false;
        }

        private void patchBtyesUS(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x21, 0x30, 0x03, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x9524, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private void ValidateBytesUS()
        {
            byte[] bytes = { 0x21, 0x30, 0x60, 0x02 };
            for (int i = 0, j = 0x9524; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }
        }
    }

}
