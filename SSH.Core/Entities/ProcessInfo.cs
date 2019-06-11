using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSH.Core.Entities
{
    public class ProcessInfo
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double CPU { get; set; }
        [Required]
        public double Memory { get; set; }
        [Required]
        public int Id { get; set; }
    }
}
