using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argenis.CRUD.Borders.Repositories.Activity
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Entities.Activity>> GetActivities(Guid? version);
        Task<bool> CheckHealth();
    }
}
