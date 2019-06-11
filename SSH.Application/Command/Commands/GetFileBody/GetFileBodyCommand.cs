using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSH.Application.Command.Commands.GetFileBody
{
    public class GetFileBodyCommand : BaseCommand, IRequest<GetFileBodyViewModel>
    {
        [Required]
        public string Path { get; set; }
    }
}
