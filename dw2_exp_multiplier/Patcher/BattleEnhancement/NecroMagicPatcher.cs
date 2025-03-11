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
            if (data == null)
                data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                patchBtyesUS(ref fs);
            else
                throw new NotImplementedException(GetName() + " Not Supported in JAP version");
        }

        public override bool ValidateBytes()
        {
            if (data == null)
                data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                return ValidateBytesUS();
            else
                throw new NotImplementedException(GetName() + " Not Supported in JAP version");
        }

        private void patchBtyesUS(ref FileStream fs)
        {
            byte[] patchedPattern = { 0xC8, 0x00, 0x05, 0x24 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x83CC, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private bool ValidateBytesUS()
        {
            byte[] bytes = { 0x64, 0x00, 0x05, 0x24 };
            for (int i = 0, j = 0x83CC; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            return true;
        }
    }
    
}
