using Dapper;
using Argenis.CRUD.Borders.Repositories.Activity;
using Argenis.CRUD.Borders.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Argenis.CRUD.Repositories.Activity
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly string GET_ACTIVITIES = @"SELECT 
                                                        a.description ActivityName,
                                                        a.id Id,
                                                        ag.description GroupName
                                                    FROM activity a
                                                    inner join activity_group ag on ag.group_id = a.group_id";

        private readonly string CHECK_HEALTH = @"SELECT 1";

        public ActivityRepository(IRepositoryHelper helper)
        {
            Helper = helper;
        }

        public IRepositoryHelper Helper { get; set; }

        public async Task<IEnumerable<Borders.Entities.Activity>> GetActivities(Guid? version)
        {
            using var connection = Helper.GetConnection();
            return await connection.QueryAsync<Borders.Entities.Activity>(GET_ACTIVITIES, null);
        }

        public async Task<bool> CheckHealth()
        {
            using var connection = Helper.GetConnection();
            try
            {
                return await connection.QueryFirstOrDefaultAsync<bool>(CHECK_HEALTH);
            }
            catch
            {
                return false;
            }
        }
    }
}
