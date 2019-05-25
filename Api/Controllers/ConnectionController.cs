using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSH.Application.Connection.Command.CheckConnection;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    public class ConnectionController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<CheckConnectionViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CheckConnection([FromBody] CheckConnectionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
