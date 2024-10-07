using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class PoisonEffectPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Poison damage increased (17).\n";

        private byte[] data;

        public PoisonEffectPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return ""; }

        public override void Patch(ref FileStream fs)
        {
            data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            byte[] patchedPattern = { 0x11, 0x00, 0x52, 0x26 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8218, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }
    }
    
}
