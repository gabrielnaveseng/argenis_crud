using Argenis.CRUD.Borders.Repositories.Base;
using Argenis.CRUD.Shared.Configurations;
using System.Data;
using System.Data.SqlClient;

namespace Argenis.CRUD.Repositories.Base
{
    public class RepositoryHelper : IRepositoryHelper
    {
        private readonly string Connection;
        public RepositoryHelper(ApplicationConfig appConfiguration)
        {
            Connection = appConfiguration.ConnectionStrings.DefaultConnection;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(Connection);
        }
    }
}

