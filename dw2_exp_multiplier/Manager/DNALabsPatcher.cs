using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Manager
{
    public class DnaLabsPatcher
    {
        public static readonly string TOOLTIP =
            "Remove restriction on 3 Guard team labs which will allow dna of any type\n" +
            "and Device Dome's lab will sum-up the DP(s) instead of adding 1.\n";

        private byte[] data;
        
        public DnaLabsPatcher(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG2000_PRO), DW2Slus.GetSize(FileIndex.STAG2000_PRO));
        }

        private void UnRestrictedDnaCityLabs()
        {
            byte[] patchedPattern = { 0x01, 0x00, 0x12, 0x20 };    // addi $s2, $r0, 0x1    # dna-flag = true
            Buffer.BlockCopy(patchedPattern, 0, data, 0x60E0, patchedPattern.Length);    // ram: 80069440
        }

        private void SumUpDpOnDNA()
        {
            byte[] patchedPattern =
            {
                0xEC, 0xC2, 0x01, 0x08,    // j 0x70bb0
                0x00, 0x00, 0x00, 0x00,    // delay slot
                0x00, 0x00, 0x00, 0x00,    // nop
                0x00, 0x00, 0x00, 0x00     // nop
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x1514, patchedPattern.Length);    // ram: 80064874
            
            patchedPattern = new byte[]
            {
                0x06, 0x80, 0x02, 0x3C,    // lui   $v0,0x8006      # Get DNA Lab ID
                0x90, 0xF7, 0x43, 0x8C,    // lw    $v1,0xf790($v0) # //
                0x15, 0x03, 0x02, 0x24,    // move  $v0,0x315       # Device Dome ID
                0x06, 0x00, 0x62, 0x10,    // beq   $v1,$v0,0x24    # Check if Lab is Device-Dome-Lab
                0x0E, 0x00, 0xA3, 0x92,    // lbu   $v1,0xe($s5)    # Get 2nd Digimon's dp on delay slot, 1st Digimon's dp passed as argument ($a0)
                0x2B, 0x10, 0x64, 0x00,    // sltu  $v0,$v1,$a0     # Check for max dp
                0x02, 0x00, 0x40, 0x14,    // bne   $v0,$r0,0x8     # //
                0x01, 0x00, 0x82, 0x24,    // addiu $v0,$a0,0x1     # Add dp by 1
                0x01, 0x00, 0x62, 0x24,    // addiu $v0,$v1,0x1     # //
                0x21, 0x92, 0x01, 0x08,    // j  0x64884            # Return back to the main flow
                0x00, 0x00, 0x00, 0x00,    // delay slot
                0x20, 0x10, 0x83, 0x00,    // add   $v0, $a0, $v1   # Sum-up dp(s)
                0x02, 0x00, 0x43, 0x28,    // slti  $v1,$v0,2       # Check minimum
                0x02, 0x00, 0x60, 0x10,    // beq   $v1,$r0,0x8     # //
                0x00, 0x00, 0x00, 0x00,    // delay slot
                0x01, 0x00, 0x42, 0x20,    // addi  $v0, $v0, 1     # Add 1 in case calculation doesn't make sense
                0x21, 0x92, 0x01, 0x08     // j  0x64884            # Return back to the main flow
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xD850, patchedPattern.Length);    // ram: 80070bb0
        }

        public void Patch(ref FileStream fs)
        {
            UnRestrictedDnaCityLabs();
            SumUpDpOnDNA();
            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG2000_PRO), DW2Slus.GetSize(FileIndex.STAG2000_PRO));
        }
        
        public void UnPatch(ref FileStream fs)
        {
            UndoUnRestrictedDnaCityLabs();
            UndoSumUpDpOnDNA();
            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG2000_PRO), DW2Slus.GetSize(FileIndex.STAG2000_PRO));
        }
        
        private void UndoUnRestrictedDnaCityLabs()
        {
            byte[] patchedPattern = { 0x21, 0x90, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x60E0, patchedPattern.Length);
        }
        
        private void UndoSumUpDpOnDNA()
        {
            byte[] patchedPattern =
            {
                0x2B, 0x10, 0x64, 0x00,
                0x02, 0x00, 0x40, 0x14,
                0x01, 0x00, 0x82, 0x24,
                0x01, 0x00, 0x62, 0x24
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x1514, patchedPattern.Length);
            
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
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xD850, patchedPattern.Length);
        }
    }
    
}
