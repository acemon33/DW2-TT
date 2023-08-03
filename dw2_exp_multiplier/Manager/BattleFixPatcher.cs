using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Manager
{
    public class BattleFixPatcher
    {
        public static readonly string VERSION = "0.4";
        public static readonly string TOOLTIP =
            "1. Venom Infusion Tech: guarantee to disable target's turn\n" +
            "2. Zen Recovery Tech: reset all state changes permanently\n" +
            "3. Shadow Scythe Tech: correct the behavior when being Interrupted\n" +
            "4. Invincibility Tech: correct the behavior when being Attacked\n";

        private byte[] data;
        
        public BattleFixPatcher(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }

        public void Patch(ref FileStream fs)
        {
            this.FixVenomInfusion();
            this.FixZenRecovery();
            this.FixShadowScythe();
            this.FixInvincibility();
            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }
        
        public void UnPatch(ref FileStream fs)
        {
            this.UndoFixVenomInfusion();
            this.UndoFixZenRecovery();
            PsxSector.WriteSector(ref fs, ref data, DW2Slus.GetLba(FileIndex.STAG3000_PRO), DW2Slus.GetSize(FileIndex.STAG3000_PRO));
        }

        private void FixVenomInfusion()
        {
            byte[] patchedPattern = { 0x00, 0x00, 0x00, 0x00 } ;
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8D38, patchedPattern.Length);    // ram: 8006c098
        }

        private void UndoFixVenomInfusion()
        {
            byte[] patchedPattern =  { 0x44, 0x8E, 0x00, 0x0C };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8D38, patchedPattern.Length);    // ram: 8006c098
        }
        
        private void FixZenRecovery()
        {
            byte[] patchedPattern =
            {
				0x7A, 0x03, 0x43, 0x94,
				0x00, 0x00, 0x00, 0x00,
				0x1C, 0x00, 0x83, 0xA6,
				0x56, 0x03, 0x43, 0xA4,
				0x86, 0x03, 0x43, 0x94,
				0x00, 0x00, 0x00, 0x00,
				0x1E, 0x00, 0x83, 0xA6,
				0x38, 0xD0, 0x01, 0x08,
				0x62, 0x03, 0x43, 0xA4
            } ;
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7ADC, patchedPattern.Length);    // ram: 8006ae3c

            patchedPattern = new byte[]
            {
				0x92, 0x03, 0x43, 0x94,
				0x00, 0x00, 0x00, 0x00,
				0x20, 0x00, 0x83, 0xA6,
				0x6E, 0x03, 0x43, 0xA4,
				0x98, 0xAB, 0x01, 0x08
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10D80, patchedPattern.Length);    // ram: 800740E0
        }

        private void UndoFixZenRecovery()
        {
            byte[] patchedPattern =
            {
                0x7A, 0x03, 0x43, 0x94,
                0x00, 0x00, 0x00, 0x00,
                0x1C, 0x00, 0x83, 0xA6,
                0x86, 0x03, 0x43, 0x94,
                0x00, 0x00, 0x00, 0x00,
                0x1E, 0x00, 0x83, 0xA6,
                0x92, 0x03, 0x42, 0x94,
                0x00, 0x00, 0x00, 0x00,
                0x20, 0x00, 0x82, 0xA6
            } ;
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7ADC, patchedPattern.Length);    // ram: 8006ae3c
            
            patchedPattern = new byte[]
            {
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10D80, patchedPattern.Length);    // ram: 800740E0
        }

        private void FixShadowScythe()
        {
            byte[] patchedPattern = { 0x21, 0x30, 0x03, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x9524, patchedPattern.Length);    // ram: 8006c884
        }

        private void FixInvincibility()
        {
            byte[] patchedPattern = { 0x3E, 0xD0, 0x01, 0x08, 0x00, 0x00, 0x00, 0x00 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x7CB8, patchedPattern.Length);    // ram: 8006b018

            patchedPattern = new byte[]
            {
                0x07, 0x80, 0x02, 0x3C,
                0xC0, 0x3C, 0x43, 0x24,
                0x21, 0x10, 0xC3, 0x02,
                0x4F, 0x03, 0x43, 0x90,
                0x00, 0x00, 0x00, 0x00,
                0x08, 0x00, 0x62, 0x30,
                0x05, 0x00, 0x40, 0x14,
                0x00, 0x00, 0x00, 0x00,
                0xB8, 0x02, 0x82, 0x94,
                0x16, 0x00, 0x83, 0x86,
                0x08, 0xAC, 0x01, 0x08,
                0x00, 0x00, 0x00, 0x00,
                0x14, 0xAC, 0x01, 0x08
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10D98, patchedPattern.Length);    // ram: 800740F8
        }
    }
    
}
