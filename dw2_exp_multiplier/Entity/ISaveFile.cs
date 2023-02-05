namespace dw2_exp_multiplier.Entity
{
    public abstract class ISaveFile
    {
        protected SaveFile SaveFile;
        public SaveFile GetSaveFile()
        {
            return this.SaveFile;
        }

        public abstract void Load(string filename);
        public abstract void Save(string filename);

        public static ISaveFile GetSaveFileLoader(string filename)
        {
            ISaveFile loader = new RawSaveFile();
            loader.Load(filename);
            return loader;
        }
    }
}