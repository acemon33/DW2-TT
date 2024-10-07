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
        private DW2Slus Slus;
        private FileStream fs = null;
        private bool IsVcd = false;
        private int PreOffset = 0;

        public DW2Image(ref FileStream fs, bool isVcd)
        {
            this.fs = fs;
            this.IsVcd = isVcd;
            if (this.IsVcd)
                this.PreOffset = 0x100000;
            DW2Slus.ValidImageFile(ref fs, PreOffset);
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
    }
}
