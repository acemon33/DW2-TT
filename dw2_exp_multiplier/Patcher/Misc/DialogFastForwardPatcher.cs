using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class DialogFastForwardPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Progressing Dialogs & Battle Menus by holding X button.\n";

        private byte[] data;
        private byte[] Stag3000Data;

        public DialogFastForwardPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Dialog Fast-Forward Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            if (data == null)
            {
                data = this.DW2Image.ReadMainFile();
                Stag3000Data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);
            }

            if (this.DW2Image.DW2Slus.GetVersion() == DW2Slus.US_VERSION)
                patchBtyesUS(ref fs);
            else
                patchBtyesJAP(ref fs);
        }

        public override bool ValidateBytes()
        {
            if (data == null)
            {
                data = this.DW2Image.ReadMainFile();
                Stag3000Data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);
            }

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

            patchedPattern = new byte[] { 0x14, 0x00, 0x82, 0x90 };
            Buffer.BlockCopy(patchedPattern, 0, Stag3000Data, 0x19B4, patchedPattern.Length);
            Buffer.BlockCopy(patchedPattern, 0, Stag3000Data, 0x35E4, patchedPattern.Length);
            patchedPattern = new byte[] { 0x14, 0x00, 0x62, 0x90 };
            Buffer.BlockCopy(patchedPattern, 0, Stag3000Data, 0x3CC4, patchedPattern.Length);
            Buffer.BlockCopy(patchedPattern, 0, Stag3000Data, 0x2538, patchedPattern.Length);
            patchedPattern = new byte[] { 0x1c, 0x00, 0x82, 0x90 };
            Buffer.BlockCopy(patchedPattern, 0, Stag3000Data, 0x19D8, patchedPattern.Length);
            Buffer.BlockCopy(patchedPattern, 0, Stag3000Data, 0x3654, patchedPattern.Length);
            patchedPattern = new byte[] { 0x1c, 0x00, 0x62, 0x90 };
            Buffer.BlockCopy(patchedPattern, 0, Stag3000Data, 0x3CD4, patchedPattern.Length);
            Buffer.BlockCopy(patchedPattern, 0, Stag3000Data, 0x2528, patchedPattern.Length);
            this.DW2Image.WriteFile(ref Stag3000Data, FileIndex.STAG3000_PRO);
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
            
            bytes = new byte[] { 0x14, 0x00, 0x82, 0x8c };
            for (int i = 0, j = 0x19B4; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag3000Data[j + i])
                    return false;
            }
            for (int i = 0, j = 0x35E4; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag3000Data[j + i])
                    return false;
            }
            bytes = new byte[] { 0x14, 0x00, 0x62, 0x8C };
            for (int i = 0, j = 0x3CC4; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag3000Data[j + i])
                    return false;
            }
            for (int i = 0, j = 0x2538; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag3000Data[j + i])
                    return false;
            }
            bytes = new byte[] { 0x1c, 0x00, 0x82, 0x8c };
            for (int i = 0, j = 0x19D8; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag3000Data[j + i])
                    return false;
            }
            for (int i = 0, j = 0x3654; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag3000Data[j + i])
                    return false;
            }
            
            bytes = new byte[] { 0x1c, 0x00, 0x62, 0x8c };
            for (int i = 0, j = 0x3CD4; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag3000Data[j + i])
                    return false;
            }
            for (int i = 0, j = 0x2528; i < bytes.Length; i++)
            {
                if (bytes[i] != Stag3000Data[j + i])
                    return false;
            }

            return true;
        }

        private void patchBtyesJAP(ref FileStream fs)
        {// TODO: complete
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
