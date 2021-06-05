using System;

namespace Argenis.CRUD.Borders.UseCases.Clients
{
    public class GetClientRequest
    {
        public GetClientRequest(Guid clientId)
        {
            ClientId = clientId;
        }

        public Guid ClientId { get; set; }
    }
}
