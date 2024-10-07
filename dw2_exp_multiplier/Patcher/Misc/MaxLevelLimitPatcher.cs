using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class MaxLevelLimitPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Remove the max level limit from Digimon.\n";

        private byte[] slusData;
        private byte[] Stag2000Data;

        public MaxLevelLimitPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Max Level Limit Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            slusData = this.DW2Image.ReadMainFile();
            Stag2000Data = this.DW2Image.ReadFile(FileIndex.STAG2000_PRO);

            ValidateBytes();

            patchBtyes(ref fs);
        }

        private void patchBtyes(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x08, 0x00, 0xE0, 0x03, 0x63, 0x00, 0x02, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, slusData, 0xF358, patchedPattern.Length);
            
            patchedPattern = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, Stag2000Data, 0x1584, patchedPattern.Length);

            this.DW2Image.WriteMainFile(ref slusData);
            this.DW2Image.WriteFile(ref Stag2000Data, FileIndex.STAG2000_PRO);
        }

        private void ValidateBytes()
        {
            byte[] bytes =
            {
                0xE8, 0xFF, 0xBD, 0x27,
                0x10, 0x00, 0xB0, 0xAF
            };

            for (int i = 0, j = 0xF358; i < bytes.Length; i++)
            {
                if (bytes[i] != slusData[j + i])
                    throw new Exception(GetName());
            }
            
            bytes = new byte[]
            {
                0x02, 0x00, 0x40, 0x14
            };

            for (int i = 0, j = 0x1584; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag2000Data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
