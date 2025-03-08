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

        public override string GetName() { return "Poison Damage Patcher"; }

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
            byte[] patchedPattern = { 0x11, 0x00, 0x52, 0x26 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8218, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private void ValidateBytesUS()
        {
            byte[] bytes = { 0x0a, 0x00, 0x52, 0x26 };
            for (int i = 0, j = 0x8218; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
