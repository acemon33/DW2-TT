using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleFix
{
    public class VenomInfusionPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Guarantee to disable target's turn.\n";

        private byte[] data;

        public VenomInfusionPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return ""; }

        public override void Patch(ref FileStream fs)
        {
            data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            byte[] patchedPattern =
            {
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8D38, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }
    }
    
}
