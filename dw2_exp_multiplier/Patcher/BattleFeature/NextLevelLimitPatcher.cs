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
            data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            ValidateBytes();

            patchBtyes(ref fs);
        }

        private void patchBtyes(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x6C, 0xC6, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xE688, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private void ValidateBytes()
        {
            byte[] bytes =
            {
                0x7E, 0xC6, 0x01, 0x08
            };

            for (int i = 0, j = 0xE688; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
