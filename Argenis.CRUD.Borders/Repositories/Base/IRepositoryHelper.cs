using System.Data;

namespace Argenis.CRUD.Borders.Repositories.Base
{
    public interface IRepositoryHelper
    {
        IDbConnection GetConnection();
    }
}
