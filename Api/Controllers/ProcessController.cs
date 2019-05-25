using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSH.Application.Processes.Query.GetAllProcesses;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
  
    public class ProcessController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(typeof(ProcessesListViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProcesses([FromBody] GetAllProcessesCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
