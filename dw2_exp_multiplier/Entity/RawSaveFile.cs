using System.IO;


namespace dw2_exp_multiplier.Entity
{
    public class RawSaveFile : ISaveFile
    {
        public override void Load(byte[] data)
        {
            this.SaveFile = new SaveFile(data);
        }

        public override void Save(string filename)
        {
            File.WriteAllBytes(filename, this.SaveFile.ToArray());
        }
    }
    
}