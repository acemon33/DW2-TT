

using System;
using System.IO;

namespace dw2_exp_multiplier.Entity
{
    public abstract class ISaveFile
    {
        protected SaveFile SaveFile;
        
        public const string mcSupportedExtensions = "All supported|*.bin;*.ddf;*.gme;*.mc;*.mcd;*.mci;*.mcr;*.mem;*.ps;*.psm;*.srm;*.vgs;*.vm1;*.vmp;*.vmc;B???????????*";
        public const string mcExtensions = "Standard Memory Card|*.mcr;*.bin;*.ddf;*.mc;*.mcd;*.mci;*.ps;*.psm;*.srm;*.vm1;*.vmc|PSP/Vita Memory Card|*.VMP|PS Vita \"MCX\" PocketStation Memory Card|*.BIN|DexDrive Memory Card|*.gme|VGS Memory Card|*.mem;*.vgs|Raw Save File|B???????????*";

        public SaveFile GetSaveFile()
        {
            return this.SaveFile;
        }

        public abstract void Load(byte[] data);
        public abstract void Save(string filename);

        public static ISaveFile GetSaveFileLoader(string filename)
        {
            ISaveFile loader;
            
            byte[] buffer = File.ReadAllBytes(filename);
            if (buffer.Length < 16384)
                throw new Exception("The file size is not correct");
            
            if (buffer.Length == 16384)
                loader = new RawSaveFile();
            else
                loader = new MemoryCardFile();
            
            loader.Load(buffer);
            return loader;
        }
    }
    
}