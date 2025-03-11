using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleFeature
{
    public class NextLevelLimitPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Allow a Digimon to Level up more than once according to the experience during battle.\n";

        private byte[] data;

        public NextLevelLimitPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Next Level Limit Patcher"; }

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

        private void patchBtyes(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x6C, 0xC6, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xE688, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private bool ValidateBytesUS()
        {
            byte[] bytes =
            {
                0x7E, 0xC6, 0x01, 0x08
            };

            for (int i = 0, j = 0xE688; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            return true;
        }
    }
    
}
