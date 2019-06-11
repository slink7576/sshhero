using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSH.Application.Command.Commands.KillProcess
{
    public class KillProcessCommand : BaseCommand, IRequest<KillProcessViewModel>
    {
        [Required]
        public int Id { get; set; }
    }
}
