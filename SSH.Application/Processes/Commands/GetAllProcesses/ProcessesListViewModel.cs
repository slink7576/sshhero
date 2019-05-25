using SSH.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Processes.Query.GetAllProcesses
{
    public class ProcessesListViewModel
    {
        public IEnumerable<ProcessInfo> Processes { get; set; }
    }
}
