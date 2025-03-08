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

        private void ValidateBytesUS()
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
                    throw new Exception(GetName());
            }
        }
    }

}
