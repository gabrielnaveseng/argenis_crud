using Argenis.CRUD.Borders.Entities;

namespace Argenis.CRUD.Borders.UseCases.Clients
{
    public class GetClientResponse
    {
        public GetClientResponse(Client client)
        {
            Client = client;
        }

        public readonly Client Client;
    }
}
