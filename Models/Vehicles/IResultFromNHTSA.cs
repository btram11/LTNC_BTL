﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface IResultFromNHTSA
    {
        public int Count { get; set; }
        public string Message { get; set; }
    }
}
