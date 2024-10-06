using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.Misc
{
    public class DialogFastForwardPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Progressing Dialogs by holding X button.\n";

        private byte[] data;

        public override string GetName() { return "Dialog Fast-Forward Patcher"; }

        public override void Patch(ref FileStream fs)
        {
            data = PsxSector.ReadSector(ref fs, FileIndex.SLUS_011_93_INDEX, FileIndex.SLUS_011_93_SIZE);

            ValidateBytes();

            patchBtyes(ref fs);
        }

        private void patchBtyes(ref FileStream fs)
        {
            byte[] patchedPattern = { 0x14, 0x00, 0x42, 0x90 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0xB4D4, patchedPattern.Length);
            
            PsxSector.WriteSector(ref fs, ref data, FileIndex.SLUS_011_93_INDEX, FileIndex.SLUS_011_93_SIZE);
        }

        private void ValidateBytes()
        {
            byte[] bytes =
            {
                0x14, 0x00, 0x42, 0x8c
            };

            for (int i = 0, j = 0xB4D4; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
