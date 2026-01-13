using api.Application.Pessoas.Handlers;
using api.Application.Pessoas.Queries;
using api.Models;
using api.Tests.Shared;
using FluentAssertions;

namespace api.Tests.Application.Pessoas.Handlers
{
    public class GetPessoaByIdHandlerTests
    {
        [Fact]
        public async Task Deve_Retornar_Pessoa_Quando_Id_Existir()
        {
            // Arrange
            var db = DbContextFactory.Create();

            var pessoa = new Pessoa
            {
                Nome = "Carlos",
                Idade = 35
            };

            db.Pessoas.Add(pessoa);
            await db.SaveChangesAsync();

            var handler = new GetPessoaByIdHandler(db);
            var query = new GetPessoaByIdQuery
            {
                Id = pessoa.Id
            };

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(pessoa.Id);
            result.Nome.Should().Be("Carlos");
            result.Idade.Should().Be(35);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Id_Nao_Existir()
        {
            // Arrange
            var db = DbContextFactory.Create();

            var handler = new GetPessoaByIdHandler(db);
            var query = new GetPessoaByIdQuery
            {
                Id = 999
            };

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().BeNull();
        }
    }
}
