using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSH.Application.Command.Commands.ExecuteCustom;
using SSH.Application.Command.Commands.GetFiles;
using SSH.Application.Command.Commands.KillProcess;
using SSH.Application.Command.Commands.Reboot;
using SSH.Application.Connection.Command.CheckConnection;
using SSH.Application.Processes.Query.GetAllProcesses;
using SSH.Application.System.Commands.GetSystemInfo;

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
        [ProducesResponseType(typeof(RebootViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Reboot([FromBody] RebootCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost]
        [ProducesResponseType(typeof(CheckConnectionViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CheckConnection([FromBody] CheckConnectionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost]
        [ProducesResponseType(typeof(SystemInfoViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSystemInfo([FromBody] GetSystemInfoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost]
        [ProducesResponseType(typeof(ProcessesListViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProcesses([FromBody] GetAllProcessesCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost]
        [ProducesResponseType(typeof(KillProcessViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> KillProcess([FromBody] KillProcessCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost]
        [ProducesResponseType(typeof(GetFilesViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFiles([FromBody] GetFilesCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}
