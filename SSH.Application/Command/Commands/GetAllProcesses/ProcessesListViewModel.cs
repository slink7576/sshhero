using SSH.Application.Base;
using SSH.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Processes.Query.GetAllProcesses
{
    public class ProcessesListViewModel : BaseViewModel
    {
        public IEnumerable<ProcessInfo> Processes { get; set; }
    }
}
