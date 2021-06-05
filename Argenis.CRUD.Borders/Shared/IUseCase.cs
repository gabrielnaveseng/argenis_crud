using System.Threading.Tasks;

namespace Argenis.CRUD.Borders.Shared
{
    public interface IUseCase<TRequest, TResponse> where TResponse : class, IResponse
    {
        Task<UseCaseResponse<TResponse>> Execute(TRequest request);
    }
}