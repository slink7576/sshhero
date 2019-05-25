using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSH.Application.System.Commands.GetSystemInfo;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    public class InfoController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<SystemInfoViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSystemInfo([FromBody] GetSystemInfoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
