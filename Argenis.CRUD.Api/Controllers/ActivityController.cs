using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Argenis.CRUD.Api.Models;
using Argenis.CRUD.Borders.UseCases.Activity;
using System;
using System.Threading.Tasks;

namespace Argenis.CRUD.Api.Controllers
{
    [Authorize]
    [Route("api/activity")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IGetActivitiesUseCase _getActivitiesUseCase;
        readonly IActionResultConverter actionResultConverter;

        public ActivityController(IGetActivitiesUseCase getActivitiesUseCase, IActionResultConverter actionResultConverter)
        {
            _getActivitiesUseCase = getActivitiesUseCase;
            this.actionResultConverter = actionResultConverter;
        }

        /// <summary>
        /// Obtem todas as atividades cadastradas para a versão mais recente do produto ou do guid fornecido
        /// </summary>
        /// <param name="versionKey">Guid version</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(GetActivitiesResponse))]
        [ProducesResponseType(404, Type = typeof(GetActivitiesResponse))]
        [ProducesResponseType(401, Type = typeof(GetActivitiesResponse))]

        public async Task<IActionResult> Get([FromQuery] Guid? versionKey)
        {
            var response = await _getActivitiesUseCase.Execute(new GetActivitiesRequest(versionKey));
            return actionResultConverter.Convert(response);
        }
    }
}
