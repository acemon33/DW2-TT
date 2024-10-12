using System;
using System.IO;
using dw2_exp_multiplier.Base;


namespace dw2_exp_multiplier.Patcher.BattleFix
{
    public class ChronoBreakerPatcher : IPatcher
    {
        public static readonly string TOOLTIP = "Correct the behavior when some (Attack / Assist / Counter-Attack) Techs following Chrono Breaker.\n";

        private byte[] data;

        public ChronoBreakerPatcher(DW2Image dw2Image) : base(dw2Image) { }

        public override string GetName() { return ""; }

        public override void Patch(ref FileStream fs)
        {
            data = this.DW2Image.ReadFile(FileIndex.STAG3000_PRO);

            byte[] patchedPattern = { 0x88, 0xD0, 0x01, 0x08 };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x8EBC, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0x20, 0x80, 0x03, 0x3C,
                0x30, 0x01, 0x63, 0x24,
                0x00, 0x00, 0x76, 0xA0,
                0x88, 0xB0, 0x01, 0x08,
                0x00, 0x14, 0x16, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10EC0, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0xCD, 0xD0, 0x01, 0x08,
                0x20, 0x80, 0x02, 0x3C
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x5530, patchedPattern.Length);

            patchedPattern = new byte[]
            {
                0x30, 0x01, 0x44, 0x24,
                0x00, 0x00, 0x82, 0x90,
                0xA5, 0x00, 0x03, 0x24,
                0x03, 0x00, 0x62, 0x14,
                0x00, 0x00, 0x00, 0x00,
                0xFF, 0xA2, 0x01, 0x08,
                0x00, 0x00, 0x80, 0xA0,
                0x9D, 0xB9, 0x01, 0x0C,
                0x00, 0x00, 0x04, 0x24,
                0x26, 0xA2, 0x01, 0x08,
                0x00, 0x00, 0x00, 0x00
            };
            Buffer.BlockCopy(patchedPattern, 0, data, 0x10FD4, patchedPattern.Length);

            this.DW2Image.WriteFile(ref data, FileIndex.STAG3000_PRO);
        }
    }
    
}
