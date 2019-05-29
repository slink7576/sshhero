using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.System.Commands.GetSystemInfo
{
    public class SystemInfoViewModel : BaseViewModel
    {
        public string OS { get; set; }
    }
}
