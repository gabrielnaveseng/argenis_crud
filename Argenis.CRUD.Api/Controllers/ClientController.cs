using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Argenis.CRUD.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        /// <summary>
        /// Obtem o cliente cadastrado para o Id
        /// </summary>
        /// <param name="clientId">Id do cliente</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid? clientId) 
        {
            return new OkObjectResult(new { Client = Guid.NewGuid(), Nome = "Argenis "});
        }
    }
}
