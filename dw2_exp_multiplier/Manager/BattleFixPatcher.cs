using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Manager
{
    public class BattleFixPatcher
    {
        public static readonly string VERSION = "0.1";
        
        private byte[] data;
        
        public BattleFixPatcher(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }

        public void Patch(ref FileStream fs)
        {
            this.FixVenomInfusion();
            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
        
        public void UnPatch(ref FileStream fs)
        {
            this.UndoFixVenomInfusion();
            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }

        private void FixVenomInfusion()
        {
            byte[] patchedPattern = { 0x00, 0x00, 0x00, 0x00} ;
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8D38, patchedPattern.Length);    // ram: 8006c098
        }

        private void UndoFixVenomInfusion()
        {
            byte[] patchedPattern =  { 0x44, 0x8E, 0x00, 0x0C };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8D38, patchedPattern.Length);    // ram: 8006c098
        }
    }
    
}
