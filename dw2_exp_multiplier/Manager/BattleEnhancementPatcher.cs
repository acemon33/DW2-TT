using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Manager
{
    public class BattleEnhancementPatcher : IPatcher
    {
        public static readonly string VERSION = "0.6";
        public static readonly string TOOLTIP =
            "1. MP-on-Guard: ratio of increasing MP during Guard is 25%\n" +
            "2. Beast King Fist: the damage is 2.1 on Counter-Attack\n" +
            "3. Subzero Ice Punch: every hit increases 7 AP\n" +
            "4. Demi Dart and Evil Touch Techs: lowering MP increased\n" +
            "5. Poison Effect Damage: poison damage increased\n" +
            "6. Debuff Defence Effect: reduce the defence state after the damage\n";

        private byte[] data;

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));

            this.ApplyMpOnGuard();
            this.ApplyBeastKingFist();
            this.ApplySubzeroIcePunch();
            this.ApplyMpReduction();
            this.ApplyPoisonDamage();
            this.ApplyDebuffDefence();
            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
        
        public void UnPatch(ref FileStream fs)
        {
            this.UndoMpOnGuard();
            this.UndoBeastKingFist();
            this.UndoSubzeroIcePunch();
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
        
        private void ApplySubzeroIcePunch()
        {
            byte[] patchedPattern = { 0x07, 0x00, 0x62, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7E48, patchedPattern.Length);    // ram: 8006b1a8
        }
        
        private void UndoSubzeroIcePunch()
        {
            byte[] patchedPattern = { 0x05, 0x00, 0x62, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7E48, patchedPattern.Length);    // ram: 8006b1a8
        }

        private void ApplyMpReduction()
        {
            byte[] patchedPattern = { 0x08, 0x00, 0x42, 0x30, 0x14, 0x00, 0x42, 0x20 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x84D8, patchedPattern.Length);    // ram: 8006b838

            patchedPattern = new byte[] { 0x15, 0x00, 0x42, 0x30, 0x32, 0x00, 0x42, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8524, patchedPattern.Length);    // ram: 8006b884
        }

        private void ApplyPoisonDamage()
        {
            byte[] patchedPattern = { 0x11, 0x00, 0x52, 0x26 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8218, patchedPattern.Length);    // ram: 8006b578
        }

        private void ApplyDebuffDefence()
        {
            byte[] patchedPattern = 
            {
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x79C8, patchedPattern.Length);    // ram: 8006ad28

            patchedPattern = new byte[] { 0x4C, 0xD0, 0x01, 0x08, 0x00, 0x00, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8208, patchedPattern.Length);    // ram: 8006b568

            patchedPattern = new byte[]
            {
                0x1A, 0x00, 0x24, 0x01,
                0x20, 0x48, 0x02, 0x00,
                0x60, 0x00, 0xA4, 0x8F,
                0x4C, 0x7C, 0x00, 0x0C,
                0x00, 0x00, 0x00, 0x00,
                0x04, 0x00, 0x42, 0x30,
                0x09, 0x00, 0x40, 0x10,
                0x00, 0x00, 0x00, 0x00,
                0x40, 0x10, 0x16, 0x00,
                0x07, 0x80, 0x12, 0x3C,
                0x46, 0x40, 0x52, 0x26,
                0x21, 0x20, 0x52, 0x00,
                0x1E, 0x00, 0x85, 0x26,
                0xDC, 0xFF, 0x46, 0x26,
                0x5A, 0xAA, 0x01, 0x0C,
                0x21, 0x30, 0x46, 0x00,
                0x12, 0x90, 0x00, 0x00,
                0x5C, 0xAD, 0x01, 0x08,
                0x20, 0x10, 0x09, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10DD0, patchedPattern.Length);    // ram: 80074130
        }
    }
    
}
