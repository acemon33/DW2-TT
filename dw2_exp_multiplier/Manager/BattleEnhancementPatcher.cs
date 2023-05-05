using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Manager
{
    public class BattleEnhancementPatcher
    {
        public static readonly string VERSION = "0.2";
        
        private byte[] data;
        
        public BattleEnhancementPatcher(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }

        public void Patch(ref FileStream fs)
        {
            this.ApplyMpOnGuard();
            this.ApplyBeastKingFist();
            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
        
        public void UnPatch(ref FileStream fs)
        {
            this.UndoMpOnGuard();
            this.UndoBeastKingFist();
            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }

        private void ApplyMpOnGuard()
        {
            byte[] patchedPattern =
            {
                0x04, 0x00, 0x05, 0x24,    // addiu $a1, zero,0x4    # set the Divider (25%)
                0x1B, 0x00, 0x85, 0x00,    // divu $a0, $a1          # Divide with max_mp
                0x00, 0x00, 0x00, 0x00,
                0x12, 0x18, 0x00, 0x00,    // mflo $v1               # Retrieve the result
                0x00, 0x00, 0x00, 0x00,
                0x32, 0x00, 0xC2, 0x94,    // lhu $v0, 0x0032($a2)   # Get current_mp
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x9774, patchedPattern.Length);    // ram: 8006cad4
        }

        private void UndoMpOnGuard()
        {
            byte[] patchedPattern =
            {
                0x67, 0x66, 0xA5, 0x34,
                0x00, 0x24, 0x04, 0x00,
                0x03, 0x14, 0x04, 0x00,
                0x18, 0x00, 0x45, 0x00,
                0xC3, 0x27, 0x04, 0x00,
                0x32, 0x00, 0xC2, 0x94,
                0x10, 0x38, 0x00, 0x00,
                0x83, 0x18, 0x07, 0x00,
                0x23, 0x18, 0x64, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x9774, patchedPattern.Length);    // ram: 8006cad4
        }
        
        private void ApplyBeastKingFist()
        {
            byte[] patchedPattern =
            {
                0x0A, 0x00, 0x04, 0x24,    // addiu  $a0, $r0, 0x10
                0x1B, 0x00, 0x44, 0x00,    // divu   $v0, $a0
                0x40, 0x10, 0x02, 0x00,    // sll    $v0, $v0, 0x1
                0x12, 0x20, 0x00, 0x00,    // mflo   $a0
                0x00, 0x00, 0x00, 0x00     // nop
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7CC0, patchedPattern.Length);    // ram: 8006b020
        }
        
        private void UndoBeastKingFist()
        {
            byte[] patchedPattern =
            {
                0x00, 0x14, 0x02, 0x00,
                0x03, 0x24, 0x02, 0x00,
                0xC2, 0x17, 0x02, 0x00,
                0x21, 0x10, 0x82, 0x00,
                0x43, 0x10, 0x02, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7CC0, patchedPattern.Length);    // ram: 8006b020
        }
    }
    
}
