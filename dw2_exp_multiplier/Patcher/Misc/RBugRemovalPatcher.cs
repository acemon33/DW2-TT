using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class RBugRemovalPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Prevent R-Bug to be spot on the Map.\n";

        private byte[] data;

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));

            byte[] patchedPattern = { 0xC8, 0xCA, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xA1F4, patchedPattern.Length);
            
            patchedPattern = new byte[]
            {
                0x0B, 0x00, 0x02, 0x24,
                0x02, 0x00, 0x62, 0x15,
                0x00, 0x00, 0x02, 0x24,
                0x00, 0x00, 0x02, 0xAD,
                0x57, 0xB5, 0x01, 0x08,
                0x08, 0x00, 0x0B, 0xA1
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xF7C0, patchedPattern.Length);
            
            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG4000_PRO), DW2Slus.GetSize(FileIndex.STAG4000_PRO));
        }
    }
    
}
