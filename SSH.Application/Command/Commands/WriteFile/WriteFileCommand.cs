using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSH.Application.Command.Commands.WriteFile
{
    public class WriteFileCommand : BaseCommand, IRequest<WriteFileViewModel>
    {
        [Required]
        public string Path { get; set; }
        [Required]
        public string Data { get; set; }
    }
}
