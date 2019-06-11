using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSH.Application.Command.Commands.CreateObject
{
    public class CreateObjectCommand : BaseCommand, IRequest<CreateObjectViewModel>
    {
        [Required]
        public string Path { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsFile { get; set; }
    }
}
