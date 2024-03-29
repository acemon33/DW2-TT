using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Manager
{
    public class DigiLinePatcher
    {
        public static readonly string TOOLTIP = "Allow to 1 or 2 Digimons to be in Digi-line (instead of always to be 3).\n";

        private byte[] slusData;
        private byte[] Stag4000Data;
        
        public DigiLinePatcher(ref FileStream fs)
        {
            slusData = PsxSector.ReadSector(ref fs, FileIndex.SLUS_011_93_INDEX, FileIndex.SLUS_011_93_SIZE);
            Stag4000Data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));
        }

        public void Patch(ref FileStream fs)
        {
            byte[] patchedPattern =
            {
                0xbc, 0x26, 0x01, 0x08,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, slusData, 0x7DCC, patchedPattern.Length);    // ram: 800175cc
            
            patchedPattern = new byte[]
            {
                0x08, 0xF7, 0x63, 0x8C,
                0x00, 0x00, 0x00, 0x00,
                0x09, 0x00, 0x60, 0x18,
                0x00, 0x00, 0x00, 0x00,
                0x9E, 0x01, 0x02, 0x86,
                0x00, 0x00, 0x00, 0x00,
                0x01, 0x00, 0x43, 0x28,
                0x04, 0x00, 0x60, 0x14,
                0x00, 0x00, 0x00, 0x00,
                0x9C, 0x01, 0x02, 0xA6,
                0x7F, 0x5D, 0x00, 0x08,
                0x00, 0x00, 0x00, 0x00,
                0x06, 0x80, 0x03, 0x3C,
                0x04, 0xF7, 0x63, 0x8C,
                0x75, 0x5D, 0x00, 0x08
            };
            Buffer.BlockCopy(patchedPattern, 0, slusData, 0x3A2F0, patchedPattern.Length);    // ram: 80049Af0

            patchedPattern = new byte[] { 0x00, 0x00, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, Stag4000Data, 0x678, patchedPattern.Length);    // ram: 800639d8

            PsxSector.WriteSector(ref fs, ref slusData, FileIndex.SLUS_011_93_INDEX, FileIndex.SLUS_011_93_SIZE);
            PsxSector.WriteSector(ref fs, ref Stag4000Data, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));
        }
        
        public void UnPatch(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x04, 0xF7, 0x63, 0x8C, 0x00, 0x00, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, slusData, 0x7DCC, patchedPattern.Length);    // ram: 800175cc
            
            patchedPattern = new byte[]
            {
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, slusData, 0x3A2F0, patchedPattern.Length);    // ram: 80049Af0
            
            patchedPattern = new byte[] { 0xE4, 0x00, 0x62, 0xA0 };
            Buffer.BlockCopy(patchedPattern, 0, Stag4000Data, 0x678, patchedPattern.Length);    // ram: 800639d8

            PsxSector.WriteSector(ref fs, ref slusData, FileIndex.SLUS_011_93_INDEX, FileIndex.SLUS_011_93_SIZE);
            PsxSector.WriteSector(ref fs, ref Stag4000Data, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));
        }
    }
    
}
