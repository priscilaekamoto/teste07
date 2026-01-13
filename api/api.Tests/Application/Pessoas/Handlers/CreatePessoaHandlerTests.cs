using api.Application.Pessoas.Commands;
using api.Application.Pessoas.Handlers;
using api.Tests.Shared;
using FluentAssertions;

namespace api.Tests.Application.Pessoas.Handlers
{
    public class CreatePessoaHandlerTests
    {
        [Fact]
        public async Task HandleAsync_DeveCriarPessoa_ComSucesso()
        {
            // Arrange
            var db = DbContextFactory.Create();
            var uow = new TestUnitOfWork(db);
            var handler = new CreatePessoaHandler(db, uow);

            var command = new CreatePessoaCommand
            {
                Nome = "PriTeste",
                Idade = 30
            };

            // Act
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.Nome.Should().Be("PriTeste");
            result.Idade.Should().Be(30);

            db.Pessoas.Should().HaveCount(1);
        }
    }
}
