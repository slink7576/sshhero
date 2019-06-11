using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSH.Application.Command.Commands.DeleteObject
{
    public class DeleteObjectCommand : BaseCommand, IRequest<DeleteObjectViewModel>
    {
        [Required]
        public string Path { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
