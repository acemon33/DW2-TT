using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class DigiLinePatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Allow to 1 or 2 Digimons to be in Digi-line (instead of always to be 3).\n";

        private byte[] slusData;
        private byte[] Stag4000Data;

        public DigiLinePatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Digi-Line Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            slusData = this.DW2Image.ReadMainFile();
            Stag4000Data = this.DW2Image.ReadFile(FileIndex.STAG4000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
            {
                ValidateBytesUS();
                patchBtyesUS(ref fs);
            }
            else
            {
                ValidateBytesJAP();
                patchBtyesJAP(ref fs);
            }
        }

        private void patchBtyesUS(ref FileStream fs)
        {
            byte[] patchedPattern =
            {
                0xB8, 0x26, 0x01, 0x08,
                0x08, 0xF7, 0x63, 0x8C
            };
            Buffer.BlockCopy(patchedPattern, 0, slusData, 0x7DCC, patchedPattern.Length);    // ram: 800175cc
            
            patchedPattern = new byte[]
            {
                0x00, 0x00, 0x00, 0x00,
                0x09, 0x00, 0x60, 0x18,
                0x00, 0x00, 0x00, 0x00,
                0x9E, 0x01, 0x02, 0x86,
                0x00, 0x00, 0x00, 0x00,
                0x01, 0x00, 0x43, 0x28,
                0x04, 0x00, 0x60, 0x14,
                0x00, 0x00, 0x00, 0x00,
                0x9C, 0x01, 0x02, 0xA6,
                0x7F, 0x5D, 0x00, 0x08,
                0x00, 0x00, 0x00, 0x00,
                0x06, 0x80, 0x03, 0x3C,
                0x04, 0xF7, 0x63, 0x8C,
                0x75, 0x5D, 0x00, 0x08,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, slusData, 0x3A2E0, patchedPattern.Length);    // ram: 80049Af0

            patchedPattern = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, Stag4000Data, 0x678, patchedPattern.Length);    // ram: 800639d8

            this.DW2Image.WriteMainFile(ref slusData);
            this.DW2Image.WriteFile(ref Stag4000Data, FileIndex.STAG4000_PRO);
        }

        private void ValidateBytesUS()
        {
            byte[] bytes =
            {
                0x04, 0xF7, 0x63, 0x8C,
                0x00, 0x00, 0x00, 0x00,
            };

            for (int i = 0, j = 0x7DCC; i < bytes.Length; i++)
            {
                if (bytes[i] != slusData[j + i])
                    throw new Exception(GetName());
            }

            for (int i = 0, j = 0x3A2E0; i < 0x3C; i++)
            {
                if (slusData[j + i] != 0)
                    throw new Exception(GetName());
            }

            bytes = new byte[]
            {
                0xE4, 0x00, 0x62, 0xA0,
            };

            for (int i = 0, j = 0x678; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag4000Data[j + i])
                    throw new Exception(GetName());
            }
        }

        private void patchBtyesJAP(ref FileStream fs)
        {
            byte[] patchedPattern =
            {
                0x2D, 0x26, 0x01, 0x08,
                0x68, 0x1B, 0x63, 0x8C
            };
            Buffer.BlockCopy(patchedPattern, 0, slusData, 0x10760, patchedPattern.Length);

            patchedPattern = new byte[]
            {
    0x00, 0x00, 0x00, 0x00, 0x09, 0x00, 0x60, 0x18, 0x00, 0x00, 0x00, 0x00, 0x9E, 0x01, 0x02, 0x86,
    0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x43, 0x28, 0x04, 0x00, 0x60, 0x14, 0x00, 0x00, 0x00, 0x00,
    0x9C, 0x01, 0x02, 0xA6, 0xE4, 0x7F, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x05, 0x80, 0x03, 0x3C,
    0x60, 0x1B, 0x63, 0x8C, 0xDA, 0x7F, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00,
            };
            Buffer.BlockCopy(patchedPattern, 0, slusData, 0x3A0B4, patchedPattern.Length);

            patchedPattern = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, Stag4000Data, 0x678, patchedPattern.Length);

            this.DW2Image.WriteMainFile(ref slusData);
            this.DW2Image.WriteFile(ref Stag4000Data, FileIndex.STAG4000_PRO);
        }

        private void ValidateBytesJAP()
        {
            byte[] bytes =
            {
                0x60, 0x1B, 0x63, 0x8C,
                0x00, 0x00, 0x00, 0x00,
            };

            for (int i = 0, j = 0x10760; i < bytes.Length; i++)
            {
                if (bytes[i] != slusData[j + i])
                    throw new Exception(GetName());
            }

            for (int i = 0, j = 0x3A0B4; i < 0x3C; i++)
            {
                if (slusData[j + i] != 0)
                    throw new Exception(GetName());
            }

            bytes = new byte[] { 0xE4, 0x00, 0x62, 0xA0, };

            for (int i = 0, j = 0x678; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag4000Data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
