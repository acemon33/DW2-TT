using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Manager
{
    public class DigimonGiftPatcher
    {
        private byte[] data;
        
        public DigimonGiftPatcher(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));
        }

        public void Patch(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x05, 0x00, 0x19, 0xa2 };    // sb $t9,0x5($s0)    # Change target gifted enemy movement on delay slot
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7060, patchedPattern.Length);    // ram: 8006a3c0
            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));
        }
        
        public void UnPatch(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x00, 0x00, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7060, patchedPattern.Length);    // ram: 8006a3c0
            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));
        }
    }
    
}
