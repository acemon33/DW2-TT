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

        public override string GetName() { return ""; }

        public override void Patch(ref FileStream fs)
        {
            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.JAP_VERSION)
                throw new NotImplementedException(GetName() + " Not Supported in JAP version");

            data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

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
    }
    
}
