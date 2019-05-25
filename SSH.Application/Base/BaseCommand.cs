using SSH.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Base
{
    public abstract class BaseCommand
    {
        public Credentials Credentials { get; set; }
    }
}
