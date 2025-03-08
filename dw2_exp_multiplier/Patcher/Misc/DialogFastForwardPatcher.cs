using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class DialogFastForwardPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Progressing Dialogs by holding X button.\n";

        private byte[] data;

        public DialogFastForwardPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Dialog Fast-Forward Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            if (data == null)
                data = this.DW2Image.ReadMainFile();

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                patchBtyesUS(ref fs);
            else
                patchBtyesJAP(ref fs);
        }

        public override bool ValidateBytes()
        {
            if (data == null)
                data = this.DW2Image.ReadMainFile();

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                return ValidateBytesUS();
            else
                return ValidateBytesJAP();
        }

        private void patchBtyesUS(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x14, 0x00, 0x42, 0x90 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xB4D4, patchedPattern.Length);
            
            this.DW2Image.WriteMainFile(ref data);
        }

        private bool ValidateBytesUS()
        {
            byte[] bytes =
            {
                0x14, 0x00, 0x42, 0x8c
            };

            for (int i = 0, j = 0xB4D4; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            return true;
        }

        private void patchBtyesJAP(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x10, 0x00, 0x42, 0x90 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x857C, patchedPattern.Length);

            this.DW2Image.WriteMainFile(ref data);
        }

        private bool ValidateBytesJAP()
        {
            byte[] bytes = { 0x10, 0x00, 0x42, 0x8c };

            for (int i = 0, j = 0x857C; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    return false;
            }
            return true;
        }
    }
    
}
