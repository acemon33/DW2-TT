using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class DomainFreeObstaclesPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Prevent Bugs, Mines, Spores & Rocks to be spot on the Map.\n";

        private byte[] data;

        public DomainFreeObstaclesPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Domain Free Obstacles Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            data = this.DW2Image.ReadFile(FileIndex.STAG4000_PRO);

            ValidateBytes();

            patchBtyes(ref fs);
        }

        private void patchBtyes(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x00, 0x00, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xA858, patchedPattern.Length);
            
            this.DW2Image.WriteFile(ref data, FileIndex.STAG4000_PRO);
        }

        private void ValidateBytes()
        {
            byte[] bytes =
            {
                0x08, 0x00, 0x40, 0x00
            };

            for (int i = 0, j = 0xA858; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
