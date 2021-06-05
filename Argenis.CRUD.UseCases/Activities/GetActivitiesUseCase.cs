using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Argenis.CRUD.Borders.Repositories.Activity;
using Argenis.CRUD.Borders.Repositories.Base;
using Argenis.CRUD.Borders.Shared;
using Argenis.CRUD.Borders.UseCases.Activity;
using Argenis.CRUD.Shared.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Argenis.CRUD.UseCases.Acitivity
{
    public class GetActivitiesUseCase : IGetActivitiesUseCase
    {
        private readonly IActivityRepository _activityRepository;
        private readonly TimeSpan CACHE_TIME = TimeSpan.FromDays(1);
        private readonly ILogger<GetActivitiesUseCase> _logger;

        public GetActivitiesUseCase(IActivityRepository activityRepository,ILogger<GetActivitiesUseCase> logger)
        {
            _activityRepository = activityRepository;
            _logger = logger;
        }

        public async Task<UseCaseResponse<GetActivitiesResponse>> Execute(GetActivitiesRequest request)
        {
            try
            {
                var activities = await _activityRepository.GetActivities(request.VersionKey);
                return UseCaseResponse<GetActivitiesResponse>.CreateOkResponse(new GetActivitiesResponse(activities));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Erro ao obter atividades. {JsonConvert.SerializeObject(new { Request = request })}");

                return UseCaseResponse<GetActivitiesResponse>.CreateInternalServerErrorResponse(null);
            }
        }
    }
}
