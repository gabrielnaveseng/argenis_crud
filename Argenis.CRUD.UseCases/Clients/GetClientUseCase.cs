using Argenis.CRUD.Borders.Repositories.Clients;
using Argenis.CRUD.Borders.Shared;
using Argenis.CRUD.Borders.UseCases.Clients;
using System.Threading.Tasks;

namespace Argenis.CRUD.UseCases.Clients
{
    public class GetClientUseCase : IGetClientUseCase
    {
        private readonly IClientsRepository _clientsRepository;

        public GetClientUseCase(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }

        public async Task<UseCaseResponse<GetClientResponse>> Execute(GetClientRequest request)
        {
            var client = await _clientsRepository.GetClient(request.ClientId);
            var result = new GetClientResponse(client);
            return UseCaseResponse<GetClientResponse>.CreateOkResponse(result);
        }
    }
}
