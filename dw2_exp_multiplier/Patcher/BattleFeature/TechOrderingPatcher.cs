using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleFeature
{
    public class TechOrderingPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Allow the Player to arrange Digimon's Techs by accessing the Learn Tech Menu after the battle.\n";

        private byte[] data;

        public TechOrderingPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Tech Ordering Patcher"; }

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
            byte[] patchedPattern = { 0x00, 0x00, 0x59, 0x34 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x4680, patchedPattern.Length);

            patchedPattern = new byte[] { 0x66, 0xD0, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x484C, patchedPattern.Length);

            patchedPattern = new byte[] { 0x6E, 0xD0, 0x01, 0x08, 0x03, 0x00, 0x44, 0x30 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xE94C, patchedPattern.Length);

            patchedPattern = new byte[] { 0x74, 0xD0, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x4A24, patchedPattern.Length);
            
            patchedPattern = new byte[]
            {
                0x05, 0x00, 0xC0, 0x17,
                0x02, 0x00, 0x27, 0x2F,
                0x03, 0x00, 0xE0, 0x10,
                0x00, 0x00, 0x00, 0x00,
                0x5F, 0x9F, 0x01, 0x08,
                0x00, 0x00, 0x00, 0x00,
                0xED, 0x9E, 0x01, 0x08,
                0x00, 0x00, 0x00, 0x00,
                0x03, 0x00, 0x80, 0x10,
                0x00, 0x00, 0x00, 0x00,
                0x2D, 0xC7, 0x01, 0x08,
                0x00, 0x00, 0x00, 0x00,
                0x30, 0xC7, 0x01, 0x08,
                0x00, 0x00, 0x04, 0x36,
                0x06, 0x80, 0x14, 0x3C,
                0x10, 0xF7, 0x91, 0x8E,
                0x07, 0x80, 0x14, 0x3C,
                0x0D, 0x00, 0x20, 0x12,
                0x00, 0x00, 0x12, 0x24,
                0x0C, 0x40, 0x93, 0x92,
                0x00, 0x00, 0x00, 0x00,
                0x02, 0x00, 0x76, 0x2E,
                0x09, 0x00, 0xC0, 0x12,
                0x0C, 0x40, 0x93, 0x92,
                0x01, 0x00, 0x52, 0x26,
                0x02, 0x00, 0x60, 0x16,
                0x02, 0x00, 0x13, 0x24,
                0x04, 0x00, 0x13, 0x24,
                0x0C, 0x40, 0x93, 0xA2,
                0x03, 0x00, 0x55, 0x2E,
                0xF8, 0xFF, 0xA0, 0x16,
                0x01, 0x00, 0x94, 0x26,
                0x63, 0x9F, 0x01, 0x08,
                0x9C, 0x00, 0xBF, 0x8F
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10E38, patchedPattern.Length);
            
            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private bool ValidateBytesUS()
        {
            byte[] bytes =
            {
                0x00, 0x00, 0x00, 0x00
            };
            for (int i = 0, j = 0x4680; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            
            bytes = new byte[]
            {
                0x73, 0x00, 0xC0, 0x13
            };
            for (int i = 0, j = 0x484C; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }

            bytes = new byte[]
            {
                0x00, 0x00, 0x00, 0x00,
                0x03, 0x00, 0x40, 0x10
            };
            for (int i = 0, j = 0xE94C; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }

            bytes = new byte[]
            {
                0x9C, 0x00, 0xBF, 0x8F
            };
            for (int i = 0, j = 0x4A24; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }

            for (int i = 0, j = 0x10EC0; i < 0x88; i++)
            {
                if (data[j + i] != 0)
                    return false;
            }
            return true;
        }
    }
    
}
