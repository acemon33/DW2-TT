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

        public override string GetName() { return "Venom Infusion Patcher"; }

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

        private bool ValidateBytesUS()
        {
            byte[] bytes =
            {
                0x44, 0x8E, 0x00, 0x0C,
                0x00, 0x00, 0x00, 0x00,
                0x07, 0x00, 0x42, 0x30,
                0x04, 0x00, 0x40, 0x10,
            };
            for (int i = 0, j = 0x8D38; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            return true;
        }

        private void patchBtyesJAP(ref FileStream fs)
        {
            byte[] patchedPattern =
            {
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xBFFC, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private bool ValidateBytesJAP()
        {
            byte[] bytes =
            {
                0x6D, 0x4B, 0x00, 0x0C,
                0x00, 0x00, 0x00, 0x00,
                0x07, 0x00, 0x42, 0x30,
                0x04, 0x00, 0x40, 0x10
            };
            for (int i = 0, j = 0xBFFC; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            return true;
        }
    }

}
