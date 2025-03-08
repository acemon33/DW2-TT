using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class DigimonGiftPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Make a wild Digimon stop moving when it accepts a gift.\n";

        private byte[] data;

        private int DataOffset1;
        private int DataOffset2;

        public DigimonGiftPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Digimon Gift Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            if (data == null)
            {
                data = this.DW2Image.ReadFile(FileIndex.STAG4000_PRO);
                if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                {
                    this.DataOffset1 = 0x7060;
                    this.DataOffset2 = 0x706C;
                }
                else
                {
                    this.DataOffset1 = 0x5ED0;
                    this.DataOffset2 = 0x5EDC;
                }
            }

            patchBtyes(ref fs);
        }

        public override bool ValidateBytes()
        {
            if (data == null)
            {
                data = this.DW2Image.ReadFile(FileIndex.STAG4000_PRO);
                if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                {
                    this.DataOffset1 = 0x7060;
                    this.DataOffset2 = 0x706C;
                }
                else
                {
                    this.DataOffset1 = 0x5ED0;
                    this.DataOffset2 = 0x5EDC;
                }
            }

            byte[] bytes = { 0x00, 0x00, 0x00, 0x00 };

            for (int i = 0, j = this.DataOffset1; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }

            bytes = new byte[] { 0x00, 0x00, 0x00, 0x00 };

            for (int i = 0, j = this.DataOffset2; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            return true;
        }

        private void patchBtyes(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x03, 0x00, 0x06, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, this.DataOffset1, patchedPattern.Length);
            
            patchedPattern = new byte[] { 0x05, 0x00, 0x06, 0xA2 };
            Buffer.BlockCopy(patchedPattern, 0, data, this.DataOffset2, patchedPattern.Length);
            
            this.DW2Image.WriteFile(ref data, FileIndex.STAG4000_PRO);
        }
    }
    
}
