using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class EnemyBossConfusionPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Enemy Boss can be affected by Confusion Effect.\n";

        private byte[] data;

        public EnemyBossConfusionPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Enemy Boss Confusion Patcher"; }

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
            byte[] patchedPattern = { 0xA8, 0xAD, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8328, patchedPattern.Length);

            patchedPattern = new byte[] { 0x15, 0xA9, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x70DC, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private void ValidateBytesUS()
        {
            byte[] bytes = { 0xDC, 0x03, 0xA2, 0x8C };
            for (int i = 0, j = 0x8328; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }

            bytes = new byte[] { 0xDC, 0x03, 0x62, 0x8C };
            for (int i = 0, j = 0x70DC; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
