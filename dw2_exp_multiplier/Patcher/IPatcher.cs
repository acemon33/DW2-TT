using dw2_exp_multiplier.Base;
using dw2_exp_multiplier.Entity;
using System.Collections.Generic;
using System.IO;


namespace dw2_exp_multiplier.Patcher
{
    public abstract class IPatcher
    {
        protected DW2Image DW2Image;

        public IPatcher(DW2Image dW2Image)
        {
            DW2Image = dW2Image;
        }

        public abstract void Patch(ref FileStream fs);
        public abstract bool ValidateBytes();
        public abstract string GetName();
    }

}
