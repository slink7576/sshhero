using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSH.Application.Command.Commands.ExecuteCustom
{
    public class ExecuteCustomCommand : BaseCommand, IRequest<ExecuteCustomViewModel>
    {
        [Required]
        public string Command { get; set; }
        public bool IsSudo { get; set; }
    }
}
