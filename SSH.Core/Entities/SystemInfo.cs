using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSH.Core.Entities
{
    public class SystemInfo
    {
        [Required]
        public string Os { get; set; }
    }
}
