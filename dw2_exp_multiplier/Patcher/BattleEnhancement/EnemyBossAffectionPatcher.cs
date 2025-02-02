using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleEnhancement
{
    public class EnemyBossAffectionPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Enemy Boss can be affected or taken damage by Venom Infusion, motivation down Techs, giga-scissor-claw.\n";

        private byte[] data;

        public EnemyBossAffectionPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return "Enemy Boss Affection Patcher"; }

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
            byte[] patchedPattern = { 0x7B, 0xAE, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8648, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8D20, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }

        private void ValidateBytesUS()
        {
            byte[] bytes = { 0x10, 0x00, 0x40, 0x10 };
            for (int i = 0, j = 0x8648; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }

            bytes = new byte[]
            {
    0xDC, 0x03, 0x42, 0x8E, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x40, 0x10, 0x03, 0x00, 0x62, 0x2A,
    0x09, 0x00, 0x40, 0x14, 0x04, 0x00, 0x22, 0x32,
};
            for (int i = 0, j = 0x8D20; i < bytes.Length; i++)
            {
                if (bytes[i] != data[j + i])
                    throw new Exception(GetName());
            }
        }
    }
    
}
