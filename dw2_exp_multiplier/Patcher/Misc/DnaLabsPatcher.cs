using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class DnaLabsPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Remove restriction on 3 Guard team labs which will allow dna of any type.\n";

        private byte[] data;

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG2000_PRO), DW2Slus.GetSize(FileIndex.STAG2000_PRO));

            byte[] patchedPattern = { 0x01, 0x00, 0x12, 0x20 };    // addi $s2, $r0, 0x1    # dna-flag = true
            Buffer.BlockCopy(patchedPattern, 0, data, 0x60E0, patchedPattern.Length);    // ram: 80069440

            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG2000_PRO), DW2Slus.GetSize(FileIndex.STAG2000_PRO));
        }
    }
    
}