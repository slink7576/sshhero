﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Base
{
    public abstract class BaseViewModel
    {
        public bool IsError { get; set; }
        public string Error { get; set; }
    }
}
