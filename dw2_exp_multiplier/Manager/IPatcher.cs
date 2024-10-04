﻿using dw2_exp_multiplier.Entity;
using System.Collections.Generic;
using System.IO;


namespace dw2_exp_multiplier.Manager
{
    public abstract class IPatcher
    {
        public abstract void Patch(ref FileStream fs);
    }

}