using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class NecroMagicPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Get 200 MP of Faint Digimon.\n";

        private byte[] data;

        public NecroMagicPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Necro Magic Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.JAP_VERSION)
                throw new NotImplementedException(GetName() + " Not Supported in JAP version");

            data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            ValidateBytesUS();
            patchBtyesUS(ref fs);
        }

        private void patchBtyesUS(ref FileStream fs)
        {
            byte[] patchedPattern = { 0xC8, 0x00, 0x05, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x83CC, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private void ValidateBytesUS()
        {
            byte[] bytes = { 0x64, 0x00, 0x05, 0x24 };
            for (int i = 0, j = 0x83CC; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
