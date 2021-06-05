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
        private const string GET_QUERY = @"SELECT name name, age age, user_id id FROM client where user_id = @user_id";
        private const string INSERT_QUERY = @"INSERT INTO master.dbo.client (user_id, name, age) VALUES(@user_id, @name, @age);"


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

        public async Task CreateClient(Client client)
        {
            var paramethers = new DynamicParameters();
            paramethers.Add("user_id", client.Id , System.Data.DbType.Guid);
            paramethers.Add("name", client.Name, System.Data.DbType.AnsiStringFixedLength);
            paramethers.Add("age", client.Age, System.Data.DbType.Int32);

            using var connection = _helper.GetConnection();
            await connection.QueryFirstAsync<Client>(INSERT_QUERY, paramethers);
        }
    }
}
