using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class NameExpansionPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Expand the Hero's Name upto ( 8 ) characters and Digi-Beetle upto ( 10 ) characters.\n";

        private byte[] slusData;
        private byte[] Stag1100Data;

        public override string GetName() { return "Name Extension Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            slusData = PsxSector.ReadSector(ref fs, FileIndex.SLUS_011_93_INDEX, FileIndex.SLUS_011_93_SIZE);
            Stag1100Data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG1100_PRO), DW2Slus.GetSize(FileIndex.STAG1100_PRO));

            ValidateBytes();

            patchBtyes(ref fs);
        }

        private void patchBtyes(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x08, 0x00, 0x02, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, slusData, 0x315C, patchedPattern.Length);    // ram: 8001295c
            
            patchedPattern = new byte[] { 0x0a, 0x00, 0x02, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, slusData, 0x3168, patchedPattern.Length);    // ram: 80012968

            patchedPattern = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, Stag1100Data, 0x2144, patchedPattern.Length);    // ram: 800654a4

            PsxSector.WriteSector(ref fs, ref slusData, FileIndex.SLUS_011_93_INDEX, FileIndex.SLUS_011_93_SIZE);
            PsxSector.WriteSector(ref fs, ref Stag1100Data, DW2Slus.GetLba(FileIndex.STAG1100_PRO), DW2Slus.GetSize(FileIndex.STAG1100_PRO));
        }

        private void ValidateBytes()
        {
            byte[] bytes =
            {
                0x05, 0x00, 0x02, 0x24
            };

            for (int i = 0, j = 0x315C; i < bytes.Length; i++)
            {
                if (bytes[i] != slusData[j + i])
                    throw new Exception(GetName());
            }
            
            bytes = new byte[]
            {
                0x07, 0x00, 0x02, 0x24
            };

            for (int i = 0, j = 0x3168; i < bytes.Length; i++)
            {
                if (bytes[i] != slusData[j + i])
                    throw new Exception(GetName());
            }
            
            bytes = new byte[]
            {
                0x19, 0x00, 0xEB, 0xA0,
                0xD8, 0x00, 0xEB, 0xA0
            };

            for (int i = 0, j = 0x2144; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag1100Data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
