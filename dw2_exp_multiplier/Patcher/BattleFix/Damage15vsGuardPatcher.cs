using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleFix
{
    public class Damage15vsGuardPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Correct the behavior 1.5 damage vs guarding.\n";

        private byte[] data;

        public Damage15vsGuardPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return ""; }

        public override void Patch(ref FileStream fs)
        {
            data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            byte[] patchedPattern = { 0x05, 0x00, 0x02, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7F04, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }
    }
    
}
