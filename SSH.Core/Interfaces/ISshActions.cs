using Renci.SshNet;
using SSH.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Core.Interfaces
{
    public interface ISshActions
    {
        IEnumerable<ProcessInfo> GetProcesses();
        SystemInfo GetInfo();
        bool CheckConnection();
    }
}
