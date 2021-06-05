using Argenis.CRUD.Borders.Entities;
using Argenis.CRUD.Borders.Repositories.Base;
using Argenis.CRUD.Borders.Repositories.Clients;
using Dapper;
using System;
using System.Threading.Tasks;

namespace Argenis.CRUD.Repositories.Clients
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly IRepositoryHelper _helper;
        private const string GET_QUERY = @"SELECT name name, age age, user_id id from client where user_id = @user_id";

        public ClientsRepository(IRepositoryHelper helper)
        {
            _helper = helper;
        }

        public async Task<Client> GetClient(Guid clientId)
        {
            var paramethers = new DynamicParameters();
            paramethers.Add("user_id", clientId, System.Data.DbType.Guid);
            
            using var connection = _helper.GetConnection();            
            return await connection.QueryFirstAsync<Client>(GET_QUERY, paramethers);
        }
    }
}
