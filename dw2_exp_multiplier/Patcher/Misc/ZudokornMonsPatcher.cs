using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class ZudokornMonsPatchers : IPatcher
    {
        public static readonly string TOOLTIP = "Obtain Zodukorn's Digimons after the training mission and get 30,000 Bits!.\n";

        private byte[] slusData;
        private byte[] Stag2000Data;

        public override string GetName() { return "Zudokorn Mons Patcher Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            slusData = PsxSector.ReadSector(ref fs, FileIndex.SLUS_011_93_INDEX, FileIndex.SLUS_011_93_SIZE);
            Stag2000Data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG2000_PRO), DW2Slus.GetSize(FileIndex.STAG2000_PRO));

            ValidateBytes();

            patchBtyes(ref fs);
        }

        private void patchBtyes(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x00, 0x00, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, slusData, 0x129AC, patchedPattern.Length);
            
            patchedPattern = new byte[] { 0x30, 0x75, 0x03, 0x24 };     // Bits
            Buffer.BlockCopy(patchedPattern, 0, slusData, 0x12CB0, patchedPattern.Length);
            
            patchedPattern = new byte[]
            {
                0xE4, 0x00, 0x45, 0xA0,
                0x40, 0x01, 0x45, 0xA0,
                0x8E, 0x9A, 0x01, 0x08,
                0x9C, 0x01, 0x45, 0xA0
            };
            Buffer.BlockCopy(patchedPattern, 0, Stag2000Data, 0x3544, patchedPattern.Length);

            PsxSector.WriteSector(ref fs, ref slusData, FileIndex.SLUS_011_93_INDEX, FileIndex.SLUS_011_93_SIZE);
            PsxSector.WriteSector(ref fs, ref Stag2000Data, DW2Slus.GetLba(FileIndex.STAG2000_PRO), DW2Slus.GetSize(FileIndex.STAG2000_PRO));
        }

        private void ValidateBytes()
        {
            byte[] bytes =
            {
                0xF6, 0xFF, 0x40, 0x14
            };

            for (int i = 0, j = 0x129AC; i < bytes.Length; i++)
            {
                if (bytes[i] != slusData[j + i])
                    throw new Exception(GetName());
            }
            
            bytes = new byte[]
            {
                0xF4, 0x01, 0x03, 0x24
            };

            for (int i = 0, j = 0x12CB0; i < bytes.Length; i++)
            {
                if (bytes[i] != slusData[j + i])
                    throw new Exception(GetName());
            }
            
            bytes = new byte[]
            {
                0xE4, 0x00, 0x40, 0xA0,
                0x40, 0x01, 0x40, 0xA0,
                0x8E, 0x9A, 0x01, 0x08,
                0x9C, 0x01, 0x40, 0xA0
            };

            for (int i = 0, j = 0x3544; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag2000Data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
