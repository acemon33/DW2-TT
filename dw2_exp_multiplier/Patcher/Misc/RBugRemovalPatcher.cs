using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class RBugRemovalPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Prevent R-Bug to be spot on the Map.\n";

        private byte[] data;

        public RBugRemovalPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "R-Bug Removal Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            data = this.DW2Image.ReadFile(FileIndex.STAG4000_PRO);

            ValidateBytes();

            patchBtyes(ref fs);
        }

        private void patchBtyes(ref FileStream fs)
        {
            byte[] patchedPattern = { 0xC8, 0xCA, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xA1F4, patchedPattern.Length);
            
            patchedPattern = new byte[]
            {
                0x0B, 0x00, 0x02, 0x24,
                0x02, 0x00, 0x62, 0x15,
                0x00, 0x00, 0x02, 0x24,
                0x00, 0x00, 0x02, 0xAD,
                0x57, 0xB5, 0x01, 0x08,
                0x08, 0x00, 0x0B, 0xA1
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xF7C0, patchedPattern.Length);
            
            this.DW2Image.WriteFile(ref data, FileIndex.STAG4000_PRO);
        }

        private void ValidateBytes()
        {
            byte[] bytes =
            {
                0x08, 0x00, 0x0B, 0xA1
            };

            for (int i = 0, j = 0xA1F4; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
