using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class DnaLabsPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Remove restriction on 3 Guard team labs which will allow dna of any type.\n";

        private byte[] data;

        private int DataOffset;

        public DnaLabsPatcher(DW2Image dw2Image) : base(dw2Image) {}

        public override string GetName() { return "DNA Labs Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            if (data == null)
            {
                data = this.DW2Image.ReadFile(FileIndex.STAG2000_PRO);

                if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                    this.DataOffset = 0x60E0;
                else
                    this.DataOffset = 0x6A14;
            }

            patchBtyes(ref fs);
        }

        public override bool ValidateBytes()
        {
            if (data == null)
            {
                data = this.DW2Image.ReadFile(FileIndex.STAG2000_PRO);

                if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                    this.DataOffset = 0x60E0;
                else
                    this.DataOffset = 0x6A14;
            }

            byte[] bytes = { 0x21, 0x90, 0x00, 0x00 };

            for (int i = 0, j = this.DataOffset; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            return true;
        }

        private void patchBtyes(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x01, 0x00, 0x12, 0x20 };    // addi $s2, $r0, 0x1    # dna-flag = true
            Buffer.BlockCopy(patchedPattern, 0, data, this.DataOffset, patchedPattern.Length);    // ram: 80069440

            this.DW2Image.WriteFile(ref data, FileIndex.STAG2000_PRO);
        }
    }
    
}
