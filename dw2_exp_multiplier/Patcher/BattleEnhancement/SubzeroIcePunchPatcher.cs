using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class SubzeroIcePunchPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Every hit increases 7 AP.\n";

        private byte[] data;

        public SubzeroIcePunchPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return ""; }

        public override void Patch(ref FileStream fs)
        {
            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.JAP_VERSION)
                throw new NotImplementedException(GetName() + " Not Supported in JAP version");

            data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            byte[] patchedPattern = { 0x07, 0x00, 0x62, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7E48, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }
    }
    
}
