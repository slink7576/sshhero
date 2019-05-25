using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSH.Application.Command.Commands.ExecuteCustom;
using SSH.Application.Command.Commands.Reboot;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    public class CommandController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ExecuteCustomCommandViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ExecuteCustom([FromBody] ExecuteCustomCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Reboot([FromBody] RebootCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
