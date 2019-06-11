using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSH.Application.Command.Commands.GetFiles
{
    public class GetFilesCommand : BaseCommand, IRequest<GetFilesViewModel>
    {
        [Required]
        public string Path { get; set; }
    }
}
