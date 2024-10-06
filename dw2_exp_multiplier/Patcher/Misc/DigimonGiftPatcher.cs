using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class DigimonGiftPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Make a wild Digimon stop moving when it accepts a gift.\n";

        private byte[] data;

        public override string GetName() { return "Digimon Gift Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));

            ValidateBytes();

            patchBtyes(ref fs);
        }

        private void patchBtyes(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x03, 0x00, 0x06, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7060, patchedPattern.Length);
            
            patchedPattern = new byte[] { 0x05, 0x00, 0x06, 0xA2 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x706C, patchedPattern.Length);
            
            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));
        }

        private void ValidateBytes()
        {
            byte[] bytes =
            {
                0x00, 0x00, 0x00, 0x00
            };

            for (int i = 0, j = 0x7060; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }
            
            bytes = new byte[]
            {
                0x00, 0x00, 0x00, 0x00
            };

            for (int i = 0, j = 0x706C; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
