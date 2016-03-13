﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public interface ICreateAndModify
    {
        DateTime CreateTime { get; set; }
        DateTime ModifyTime { get; set; }
    }
}
