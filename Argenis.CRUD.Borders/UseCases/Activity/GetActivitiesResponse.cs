using Argenis.CRUD.Borders.Dtos.Activities;
using Argenis.CRUD.Borders.Shared;
using System.Collections.Generic;

namespace Argenis.CRUD.Borders.UseCases.Activity
{
    public class GetActivitiesResponse : IResponse
    {
        public OutputActivityModel? OutputActivityModel  { get; private set; }

        public GetActivitiesResponse(IEnumerable<Entities.Activity> activities)
        {
            OutputActivityModel = new OutputActivityModel(activities);
        }
    }
}
