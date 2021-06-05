using System.Threading.Tasks;

namespace Argenis.CRUD.Borders.Shared
{
    public interface IUseCaseOnlyResponse<TResponse> where TResponse : class, IResponse
    {
        Task<UseCaseResponse<TResponse>> Execute();
    }
}