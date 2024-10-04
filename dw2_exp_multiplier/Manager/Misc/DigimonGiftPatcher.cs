using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Manager
{
    public class DigimonGiftPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Make a wild Digimon stop moving when it accepts a gift.\n";

        private byte[] data;

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));

            byte[] patchedPattern = { 0x03, 0x00, 0x06, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7060, patchedPattern.Length);
            
            patchedPattern = new byte[] { 0x05, 0x00, 0x06, 0xA2 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x706C, patchedPattern.Length);
            
            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));
        }
    }
    
}
