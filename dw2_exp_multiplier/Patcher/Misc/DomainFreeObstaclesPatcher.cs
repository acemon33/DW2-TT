using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class DomainFreeObstaclesPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Prevent Bugs, Mines, Spores & Rocks to be spot on the Map.\n";

        private byte[] data;

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));

            byte[] patchedPattern = { 0x00, 0x00, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xA858, patchedPattern.Length);
            
            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));
        }
    }
    
}
