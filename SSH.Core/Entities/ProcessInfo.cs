using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Core.Entities
{
    public class ProcessInfo
    {
        public string Name { get; set; }
        public double CPU { get; set; }
        public double Memory { get; set; }
        public int Id { get; set; }
    }
}
