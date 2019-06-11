using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSH.Core.Entities
{
    public class FileNode
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsFile { get; set; }
    }
}
