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

        public DW2Image(ref FileStream fs)
        {
            this.fs = fs;
            DW2Slus.ValidImageFile(ref fs);
        }

        public void WriteMainFile(ref byte[] data)
        {
            PsxSector.WriteSector(ref fs, ref data, FileIndex.SLUS_011_93_INDEX, FileIndex.SLUS_011_93_SIZE);
        }
        
        public byte[] ReadMainFile()
        {
            return PsxSector.ReadSector(ref fs, FileIndex.SLUS_011_93_INDEX, FileIndex.SLUS_011_93_SIZE); ;
        }
        
        public void WriteFile(ref byte[] data, int index)
        {
            PsxSector.WriteSector(ref this.fs, ref data, DW2Slus.GetLba(index), DW2Slus.GetSize(index));
        }
        
        public byte[] ReadFile(int index)
        {
            return PsxSector.ReadSector(ref this.fs, DW2Slus.GetLba(index), DW2Slus.GetSize(index));
        }
    }
}
