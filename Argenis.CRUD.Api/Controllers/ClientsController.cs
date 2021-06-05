using Argenis.CRUD.Borders.UseCases.Clients;
using Argenis.CRUD.UseCases.Clients;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Argenis.CRUD.Api.Controllers
{
    [Route("api/v1/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IGetClientUseCase _getClientUseCase;

        public ClientsController(IGetClientUseCase getClientUseCase)
        {
            _getClientUseCase = getClientUseCase;
        }

        /// <summary>
        /// Obtem o cliente cadastrado para o Id
        /// </summary>
        /// <param name="clientId">Id do cliente</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid clientId) 
        {
            var request = new GetClientRequest(clientId);          
            var result = await _getClientUseCase.Execute(request);
            return new OkObjectResult(result.Result.Client);
        }
    }
}
