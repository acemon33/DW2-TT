using System.IO;

namespace dw2_exp_multiplier.Entity
{
    public class RawSaveFile : ISaveFile
    {
        public override void Load(string filename)
        {
            this.SaveFile = new SaveFile(File.ReadAllBytes(filename));
        }

        public override void Save(string filename)
        {
            File.WriteAllBytes(filename, this.SaveFile.ToArray());
        }
    }
}