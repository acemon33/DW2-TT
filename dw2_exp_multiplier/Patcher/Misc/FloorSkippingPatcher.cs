using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class FloorSkippingPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Allow the Player to jump to the specified floor in the domain\nUsage:\n- press x on the floor gate\n- when dialog appeares, don't close it, press as the following:\n  - R1 to increase floor\n  - L1 to decrease floor\n";

        private byte[] data;

        public FloorSkippingPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Floor Skipping Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            data = this.DW2Image.ReadFile(FileIndex.STAG4000_PRO);

            ValidateBytes();

            patchBtyes(ref fs);
        }

        private void patchBtyes(ref FileStream fs)
        {
            byte[] patchedPattern = { 0xB6, 0xCC, 0x01, 0x08, 0x18, 0x00, 0xBF, 0x8F };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x43DC, patchedPattern.Length);
            
            patchedPattern = new byte[]
            {
                0x10, 0xF7, 0x90, 0x8E,
                0xA3, 0xD5, 0x91, 0x82,
                0x04, 0x00, 0x00, 0x1A,
                0x18, 0xF7, 0x90, 0x8E,
                0x01, 0x00, 0x31, 0x26,
                0xC1, 0xCC, 0x01, 0x08,
                0xA3, 0xD5, 0x91, 0xA2,
                0x00, 0x00, 0x00, 0x00,
                0x0E, 0x00, 0x00, 0x1A,
                0xFF, 0xFF, 0x31, 0x26,
                0xA3, 0xD5, 0x91, 0xA2,
                0x01, 0x00, 0x31, 0x26,
                0x0A, 0x00, 0x10, 0x24,
                0x1B, 0x00, 0x30, 0x02,
                0x12, 0x80, 0x00, 0x00,
                0x10, 0x88, 0x00, 0x00,
                0x03, 0x00, 0x00, 0x12,
                0xED, 0xE3, 0x90, 0xA2,
                0xCD, 0xCC, 0x01, 0x08,
                0xEE, 0xE3, 0x91, 0xA2,
                0xED, 0xE3, 0x91, 0xA2,
                0xFF, 0x00, 0x10, 0x34,
                0xEE, 0xE3, 0x90, 0xA2,
                0xD1, 0x9D, 0x01, 0x08,
                0x14, 0x00, 0xB1, 0x8F,
                0xEE, 0xE3, 0x90, 0xA2,
                0xD1, 0x9D, 0x01, 0x08,
                0x14, 0x00, 0xB1, 0x8F
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xFF78, patchedPattern.Length);
            
            this.DW2Image.WriteFile(ref data, FileIndex.STAG4000_PRO);
        }

        private void ValidateBytes()
        {
            byte[] bytes =
            {
                0x18, 0x00, 0xBF, 0x8F,
                0x14, 0x00, 0xB1, 0x8F
            };

            for (int i = 0, j = 0x43DC; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
