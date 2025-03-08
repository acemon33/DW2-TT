using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleFeature
{
    public class TurnSkippingPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Skip animations & voice during battle combat.\nUsage:\n- Press a button on Main Menu (Give Orders / Cannon / Run Away) as the following:\n  - [] square: to enable Skip Turns (Skip Animation)\n  - /\\ triangle: to disable Skip Turns (Default Animation)";

        private byte[] slusData;
        private byte[] Stag3000Data;

        public TurnSkippingPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Turn Skipping Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.JAP_VERSION)
                throw new NotImplementedException(GetName() + " Not Supported in JAP version");

            slusData = this.DW2Image.ReadMainFile();
            Stag3000Data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            //ValidateBytes();
            patchBtyes(ref fs);
        }

        public override bool ValidateBytes()
        {
            return false;
        }

        private void patchBtyes(ref FileStream fs)
        {
            byte[] patchedPattern =
            {
                0x07, 0x80, 0x10, 0x3C,
                0xC8, 0x3C, 0x11, 0x92,
                0x06, 0x00, 0x12, 0x24,
                0x09, 0x00, 0x32, 0x16,
                0x06, 0x80, 0x12, 0x3C,
                0x08, 0xF7, 0x51, 0x8E,
                0x0C, 0xF7, 0x53, 0x8E,
                0x02, 0x00, 0x20, 0x12,
                0x01, 0x00, 0x11, 0x24,
                0xB0, 0x40, 0x11, 0xA2,
                0x02, 0x00, 0x60, 0x12,
                0x00, 0x00, 0x00, 0x00,
                0xB0, 0x40, 0x00, 0xA2,
                0xE8, 0x93, 0x01, 0x08,
                0x60, 0x00, 0xBF, 0x8F,
                0x07, 0x80, 0x10, 0x3C,
                0xB0, 0x40, 0x12, 0x92,
                0xA0, 0x38, 0x14, 0x92,
                0x4D, 0x00, 0x40, 0x12,
                0x18, 0x00, 0x16, 0x24,
                0x01, 0x00, 0x08, 0x24,
                0x0B, 0x00, 0x09, 0x24,
                0x10, 0x00, 0x18, 0x24,
                0x0A, 0x00, 0x80, 0x16,
                0x96, 0x38, 0x0C, 0x92,
                0x00, 0x00, 0x00, 0x00,
                0x03, 0x00, 0x8F, 0x29,
                0x02, 0x00, 0xE0, 0x15,
                0x13, 0x00, 0x0F, 0x24,
                0xA0, 0x38, 0x0F, 0xA2,
                0x06, 0x00, 0x0E, 0x26,
                0xB0, 0x38, 0x13, 0x92,
                0x03, 0x27, 0x01, 0x08,
                0xA2, 0x38, 0x16, 0xA2,
                0xB1, 0x40, 0x17, 0x92,
                0x00, 0x00, 0x00, 0x00,
                0x09, 0x00, 0xF3, 0x2A,
                0x31, 0x00, 0x60, 0x16,
                0x16, 0x00, 0x12, 0x24,
                0xFF, 0xFF, 0x17, 0x20,
                0x98, 0x38, 0x15, 0x92,
                0x09, 0x00, 0x12, 0x24,
                0x05, 0x00, 0xB2, 0x16,
                0x16, 0x00, 0x12, 0x24,
                0xF8, 0xFF, 0x0E, 0x22,
                0xAC, 0x38, 0x13, 0x92,
                0x03, 0x27, 0x01, 0x08,
                0xB0, 0x38, 0x18, 0xA2,
                0x20, 0x00, 0x92, 0x16,
                0xB4, 0x00, 0x12, 0x24,
                0x00, 0x00, 0x35, 0x92,
                0xA0, 0x00, 0x0B, 0x24,
                0x02, 0x00, 0xAB, 0x16,
                0xB6, 0x00, 0x13, 0x24,
                0x00, 0x00, 0x33, 0xA2,
                0x00, 0x00, 0x0E, 0x26,
                0xAA, 0x38, 0x13, 0x92,
                0x00, 0x00, 0x0D, 0x24,
                0xC2, 0x38, 0xCC, 0x25,
                0x00, 0x00, 0x88, 0xA1,
                0x02, 0x00, 0x88, 0xA1,
                0x0C, 0x00, 0x89, 0xA1,
                0x02, 0x00, 0x68, 0x12,
                0x01, 0x00, 0xAD, 0x25,
                0x14, 0x00, 0x98, 0xA1,
                0x2A, 0xC8, 0xB3, 0x01,
                0xF8, 0xFF, 0x20, 0x17,
                0x1E, 0x00, 0x8C, 0x25,
                0x06, 0x00, 0x13, 0x11,
                0x1E, 0x00, 0x0C, 0x24,
                0x18, 0x00, 0x93, 0x01,
                0x12, 0x60, 0x00, 0x00,
                0xBA, 0x38, 0x8C, 0x25,
                0x21, 0x60, 0x8E, 0x01,
                0x00, 0x00, 0x96, 0xA1,
                0x05, 0x00, 0x13, 0x15,
                0x03, 0x00, 0x13, 0x24,
                0x02, 0x00, 0xB3, 0x16,
                0x00, 0x00, 0x00, 0x00,
                0xFC, 0xFF, 0xCE, 0x25,
                0xDA, 0x38, 0xD8, 0xA1,
                0x02, 0x00, 0x92, 0x16,
                0x78, 0x00, 0x12, 0x24,
                0xA0, 0x38, 0x18, 0xA2,
                0x02, 0x00, 0x92, 0x16,
                0x00, 0x00, 0x00, 0x00,
                0xA0, 0x38, 0x18, 0xA2,
                0x01, 0x00, 0xF7, 0x26,
                0xB1, 0x40, 0x17, 0xA2,
                0x13, 0x00, 0x0F, 0x24,
                0x05, 0x00, 0x8F, 0x16,
                0x00, 0x00, 0x35, 0x92,
                0xA2, 0x00, 0x13, 0x24,
                0x02, 0x00, 0xB3, 0x16,
                0xBC, 0x00, 0x13, 0x24,
                0x00, 0x00, 0x33, 0xA2,
                0x9C, 0x00, 0xBF, 0x8F,
                0xB2, 0xB4, 0x01, 0x08,
                0x98, 0x00, 0xB6, 0x8F,
                0xB0, 0x40, 0x63, 0x90,
                0x00, 0x00, 0x00, 0x00,
                0x03, 0x00, 0x60, 0x10,
                0x18, 0x00, 0x43, 0x8E,
                0x22, 0xBC, 0x01, 0x08,
                0x00, 0x00, 0x00, 0x00,
                0x97, 0xBC, 0x01, 0x08,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, slusData, 0x3A328, patchedPattern.Length);
            
            patchedPattern = new byte[] { 0xCA, 0x26, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, Stag3000Data, 0x1C38, patchedPattern.Length);

            patchedPattern = new byte[] { 0xD9, 0x26, 0x01, 0x08, 0x07, 0x80, 0x10, 0x3C };
            Buffer.BlockCopy(patchedPattern, 0, Stag3000Data, 0x9F64, patchedPattern.Length);

            patchedPattern = new byte[] { 0x2D, 0x27, 0x01, 0x08, 0x07, 0x80, 0x03, 0x3C };
            Buffer.BlockCopy(patchedPattern, 0, Stag3000Data, 0xBEF4, patchedPattern.Length);

            this.DW2Image.WriteMainFile(ref slusData);
            this.DW2Image.WriteFile(ref Stag3000Data, FileIndex.STAG3000_PRO);
        }

        private void ValidateBytesUS()
        {
            byte[] bytes =
            {
                0x60, 0x00, 0xBF, 0x8F
            };
            for (int i = 0, j = 0x1C38; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag3000Data[j + i])
                    throw new Exception(GetName());
            }

            bytes = new byte[]
            {
                0x9C, 0x00, 0xBF, 0x8F,
                0x98, 0x00, 0xB6, 0x8F
            };
            for (int i = 0, j = 0x9F64; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag3000Data[j + i])
                    throw new Exception(GetName());
            }

            bytes = new byte[]
            {
                0x18, 0x00, 0x43, 0x8E,
                0x00, 0x00, 0x00, 0x00
            };
            for (int i = 0, j = 0xBEF4; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag3000Data[j + i])
                    throw new Exception(GetName());
            }

            for (int i = 0, j = 0x3A328; i < 0x1B8; i++)
            {
                if (slusData[j + i] != 0)
                    throw new Exception(GetName());
            }
        }
    }
    
}
