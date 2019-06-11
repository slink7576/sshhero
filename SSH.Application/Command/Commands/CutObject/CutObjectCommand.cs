using MediatR;
using SSH.Application.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SSH.Application.Command.Commands.CutObject
{
    public class CutObjectCommand : BaseCommand, IRequest<CutObjectViewModel>
    {
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public string File { get; set; }
    }
}
