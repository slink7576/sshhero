using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSH.Core.Entities
{
    public class Credentials
    {
        [Required]
        public string Hostname { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Login { get; set; }
    }
}
