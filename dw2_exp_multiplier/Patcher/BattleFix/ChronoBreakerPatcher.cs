using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleFix
{
    public class ChronoBreakerPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Correct the behavior when some (Attack / Assist / Counter-Attack) Techs following Chrono Breaker.\n";

        private byte[] data;

        public ChronoBreakerPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Chrono Breaker Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            if (data == null)
                data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                patchBtyesUS(ref fs);
            else
                throw new NotImplementedException(GetName() + " Not Supported in JAP version");
        }

        public override bool ValidateBytes()
        {
            if (data == null)
                data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                return ValidateBytesUS();
            else
                throw new NotImplementedException(GetName() + " Not Supported in JAP version");
        }

        private void patchBtyesUS(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x88, 0xD0, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8EBC, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0x07, 0x80, 0x03, 0x3C,
                0x5C, 0x43, 0x63, 0x24,
                0x00, 0x00, 0x76, 0xA0,
                0x88, 0xB0, 0x01, 0x08,
                0x00, 0x14, 0x16, 0x00,
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10EC0, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0xCD, 0xD0, 0x01, 0x08,
                0x07, 0x80, 0x02, 0x3C
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x5530, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0x5C, 0x43, 0x44, 0x24, 0x00, 0x00, 0x82, 0x90, 0xA5, 0x00, 0x03, 0x24, 0x03, 0x00, 0x62, 0x14,
                0x00, 0x00, 0x00, 0x00, 0xFF, 0xA2, 0x01, 0x08, 0x00, 0x00, 0x80, 0xA0, 0x9D, 0xB9, 0x01, 0x0C,
                0x00, 0x00, 0x04, 0x24, 0x26, 0xA2, 0x01, 0x08, 0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10FD4, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private bool ValidateBytesUS()
        {
            byte[] bytes = { 0x00, 0x14, 0x16, 0x00 };
            for (int i = 0, j = 0x8EBC; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }

            for (int i = 0, j = 0x10EC0; i < 0x14; i++)
            {
                if (data[j + i] != 0)
                    return false;
            }

            bytes = new byte[] { 0x9D, 0xB9, 0x01, 0x0C, 0x21, 0x20, 0x00, 0x00 };
            for (int i = 0, j = 0x5530; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }

            for (int i = 0, j = 0x10FD4; i < 0x28; i++)
            {
                if (data[j + i] != 0)
                    return false;
            }
            return true;
        }
    }
    
}
