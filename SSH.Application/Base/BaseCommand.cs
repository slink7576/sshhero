using SSH.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSH.Application.Base
{
    public abstract class BaseCommand
    {
        [Required]
        public Credentials Credentials { get; set; }
    }
}
