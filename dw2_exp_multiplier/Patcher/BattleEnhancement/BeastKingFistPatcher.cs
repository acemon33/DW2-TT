using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class BeastKingFistPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "The damage is 1.8 on Counter-Attack.\n";

        private byte[] data;

        public BeastKingFistPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Beast King Fist Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            if (data == null)
                data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                patchBtyesUS(ref fs);
            else
                throw new NotImplementedException(GetName() + " Not Supported in JAP version");
        }

        public override bool ValidateBytes()
        {
            if (data == null)
                data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                return ValidateBytesUS();
            else
                throw new NotImplementedException(GetName() + " Not Supported in JAP version");
        }

        private void patchBtyesUS(ref FileStream fs)
        {
            byte[] patchedPattern =
            {
                0x12, 0x00, 0x04, 0x34,
                0x18, 0x00, 0x44, 0x00,
                0x0A, 0x00, 0x12, 0x34,
                0x12, 0x20, 0x00, 0x00,
                0x1A, 0x00, 0x92, 0x00,
                0x12, 0x90, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7CC0, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private bool ValidateBytesUS()
        {
            byte[] bytes =
            {
    0x00, 0x14, 0x02, 0x00, 0x03, 0x24, 0x02, 0x00, 0xC2, 0x17, 0x02, 0x00, 0x21, 0x10, 0x82, 0x00,
    0x43, 0x10, 0x02, 0x00, 0x21, 0x90, 0x44, 0x00,
};
            for (int i = 0, j = 0x7CC0; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            return true;
        }
    }
    
}
