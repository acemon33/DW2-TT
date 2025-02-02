using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class EvilTouchDemiDartPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Increasing Lowering MP Effect.\n";

        private byte[] data;

        public EvilTouchDemiDartPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Reduce MP Patcher"; }

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
            byte[] patchedPattern = { 0x08, 0x00, 0x42, 0x30, 0x14, 0x00, 0x42, 0x20 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x84D8, patchedPattern.Length);

            patchedPattern = new byte[] { 0x15, 0x00, 0x42, 0x30, 0x32, 0x00, 0x42, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8524, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private void ValidateBytesUS()
        {
            byte[] bytes = { 0x03, 0x00, 0x42, 0x30, 0x04, 0x00, 0x42, 0x24 };
            for (int i = 0, j = 0x84D8; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }

            bytes = new byte[] { 0x07, 0x00, 0x42, 0x30, 0x0D, 0x00, 0x42, 0x24 };
            for (int i = 0, j = 0x8524; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
