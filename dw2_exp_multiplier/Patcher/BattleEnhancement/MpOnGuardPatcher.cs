using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class MpOnGaurdPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Ratio of increasing MP during Guard is 25%.\n";

        private byte[] data;

        public MpOnGaurdPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "MP on Guard Patcher"; }

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
            byte[] patchedPattern =
            {
                0x04, 0x00, 0x05, 0x24,
                0x1B, 0x00, 0x85, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x12, 0x18, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x32, 0x00, 0xC2, 0x94,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x9774, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private void ValidateBytesUS()
        {
            byte[] bytes =
            {
    0x67, 0x66, 0xA5, 0x34, 0x00, 0x24, 0x04, 0x00, 0x03, 0x14, 0x04, 0x00, 0x18, 0x00, 0x45, 0x00,
    0xC3, 0x27, 0x04, 0x00, 0x32, 0x00, 0xC2, 0x94, 0x10, 0x38, 0x00, 0x00, 0x83, 0x18, 0x07, 0x00,
    0x23, 0x18, 0x64, 0x00,
};
            for (int i = 0, j = 0x9774; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
