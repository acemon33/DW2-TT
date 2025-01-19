using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;


namespace dw2_exp_multiplier.Base
{
    public class DW2Image
    {
        private DW2Slus slus;
        private FileStream fs = null;
        private bool IsVcd = false;
        private int PreOffset = 0;

        public DW2Slus DW2Slus { get { return slus; } }

        public DW2Image(ref FileStream fs, bool isVcd, int version)
        {
            this.fs = fs;
            this.IsVcd = isVcd;
            if (this.IsVcd)
                this.PreOffset = 0x100000;
            this.slus = DW2Slus.ValidImageFile(ref fs, PreOffset, version);
        }

        public void WriteMainFile(ref byte[] data)
        {
            PsxSector.WriteSector(ref fs, ref data, FileIndex.SLUS_011_93_INDEX, FileIndex.SLUS_011_93_SIZE, PreOffset);
        }
        
        public byte[] ReadMainFile()
        {
            return PsxSector.ReadSector(ref fs, FileIndex.SLUS_011_93_INDEX, FileIndex.SLUS_011_93_SIZE, PreOffset); ;
        }
        
        public void WriteFile(ref byte[] data, int index)
        {
            PsxSector.WriteSector(ref this.fs, ref data, DW2Slus.GetLba(index), DW2Slus.GetSize(index), PreOffset);
        }
        
        public byte[] ReadFile(int index)
        {
            return PsxSector.ReadSector(ref this.fs, DW2Slus.GetLba(index), DW2Slus.GetSize(index), PreOffset);
        }

        public void WriteFileAtOffset(ref byte[] data, int offset)
        {
            PsxSector.WriteSector(ref this.fs, ref data, offset, (int)Math.Ceiling(new Decimal(data.Length / 0x800)), PreOffset);
        }
    }
}
