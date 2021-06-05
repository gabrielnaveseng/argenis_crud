using System;

namespace Argenis.CRUD.Borders.UseCases.Activity
{
    public class GetActivitiesRequest
    {
        public readonly Guid? VersionKey;

        public GetActivitiesRequest(Guid? versionKey)
        {
            VersionKey = versionKey;
        }
    }
}
