using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Application.Command.Commands.ExecuteCustom
{
    public class ExecuteCustomCommandViewModel
    {
        public bool IsError { get; set; }
        public string Result { get; set; }
        public string Error { get; set; }
    }
}
