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

        public override string GetName() { return ""; }

        public override void Patch(ref FileStream fs)
        {
            data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            byte[] patchedPattern = { 0x21, 0x30, 0x03, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x9524, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }
    }
    
}
