using Argenis.CRUD.Borders.Entities;
using System;
using System.Threading.Tasks;

namespace Argenis.CRUD.Borders.Repositories.Clients
{
    public interface IClientsRepository
    {
        Task<Client> GetClient(Guid clientId);
        Task CreateClient(Client client);
    }
}
