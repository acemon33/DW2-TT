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

        public override string GetName() { return "Max Level Limit Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            slusData = PsxSector.ReadSector(ref fs, FileIndex.SLUS_011_93_INDEX, FileIndex.SLUS_011_93_SIZE);
            Stag2000Data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG2000_PRO), DW2Slus.GetSize(FileIndex.STAG2000_PRO));

            ValidateBytes();

            patchBtyes(ref fs);
        }

        private void patchBtyes(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x08, 0x00, 0xE0, 0x03, 0x63, 0x00, 0x02, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, slusData, 0xF358, patchedPattern.Length);
            
            patchedPattern = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, Stag2000Data, 0x1584, patchedPattern.Length);

            PsxSector.WriteSector(ref fs, ref slusData, FileIndex.SLUS_011_93_INDEX, FileIndex.SLUS_011_93_SIZE);
            PsxSector.WriteSector(ref fs, ref Stag2000Data, DW2Slus.GetLba(FileIndex.STAG2000_PRO), DW2Slus.GetSize(FileIndex.STAG2000_PRO));
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
