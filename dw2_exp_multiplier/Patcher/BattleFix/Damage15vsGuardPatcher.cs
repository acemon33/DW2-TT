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

        public override string GetName() { return "1.5 Damage vs Guarding Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            if (data == null)
                data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                patchBtyesUS(ref fs);
            else
                patchBtyesJAP(ref fs);
        }

        public override bool ValidateBytes()
        {
            if (data == null)
                data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                return ValidateBytesUS();
            else
                return ValidateBytesJAP();
        }

        private void patchBtyesUS(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x05, 0x00, 0x02, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7F04, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private bool ValidateBytesUS()
        {
            byte[] bytes = { 0x03, 0x00, 0x02, 0x24 };
            for (int i = 0, j = 0x7F04; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            return true;
        }

        private void patchBtyesJAP(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x05, 0x00, 0x02, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xB1C8, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private bool ValidateBytesJAP()
        {
            byte[] bytes = { 0x03, 0x00, 0x02, 0x24 };
            for (int i = 0, j = 0xB1C8; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            return true;
        }
    }
}
