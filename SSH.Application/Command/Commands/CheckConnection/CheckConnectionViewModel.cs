using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Connection.Command.CheckConnection
{
    public class CheckConnectionViewModel : BaseViewModel
    {
        public bool IsAlive { get; set; }
    }
}
