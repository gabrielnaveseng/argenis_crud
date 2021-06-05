using Argenis.CRUD.Borders.Repositories.Clients;
using Argenis.CRUD.UseCases.Clients;
using System.Threading.Tasks;
using Xunit;
using Moq;
using System;
using Argenis.CRUD.Borders.Entities;
using Argenis.CRUD.Borders.UseCases.Clients;
using FluentAssertions;

namespace Argenis.CRUD.Tests.UseCases
{
    public class GetClientUseCaseTest
    {
        [Fact]
        public async Task Execute_WhenGuidIsValid_Sucess()
        {
            var clientId = Guid.NewGuid();
            var repository = new Mock<IClientsRepository>();
            var client = new Client("Joao", 25, clientId);

            repository.Setup(x => x.GetClient(It.IsAny<Guid>())).ReturnsAsync(client);

            var useCase = new GetClientUseCase(repository.Object);
            var result = await useCase.Execute(new GetClientRequest(clientId));

            result.Result.Client.Should().BeEquivalentTo(client);
        }

        [Fact]
        public async Task Execute_WhenGuidIsEmpty_Exception()
        {
            var clientId = Guid.Empty;
            var repository = new Mock<IClientsRepository>();
            var client = new Client("Joao", 25, clientId);

            repository.Setup(x => x.GetClient(It.IsAny<Guid>())).ReturnsAsync(client);

            var useCase = new GetClientUseCase(repository.Object);

            Func<Task> act = async () => await useCase.Execute(new GetClientRequest(clientId));

            act.Should().Throw<InvalidOperationException>().WithMessage("Invalid request");
        }
    }
}
