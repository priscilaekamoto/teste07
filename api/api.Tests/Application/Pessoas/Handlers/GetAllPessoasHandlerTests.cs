using api.Application.Pessoas.Handlers;
using api.Application.Pessoas.Queries;
using api.Models;
using api.Tests.Shared;
using FluentAssertions;

namespace api.Tests.Application.Pessoas.Handlers
{
    public class GetAllPessoasHandlerTests
    {
        [Fact]
        public async Task Deve_Retornar_Todas_As_Pessoas()
        {
            // Arrange
            var db = DbContextFactory.Create();

            db.Pessoas.AddRange(
                new Pessoa { Nome = "Ana", Idade = 30 },
                new Pessoa { Nome = "Carlos", Idade = 45 }
            );

            await db.SaveChangesAsync();

            var handler = new GetAllPessoasHandler(db);
            var query = new GetAllPessoasQuery();

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            result.Should().Contain(p =>
                p.Nome == "Ana" && p.Idade == 30);

            result.Should().Contain(p =>
                p.Nome == "Carlos" && p.Idade == 45);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_Pessoas()
        {
            // Arrange
            var db = DbContextFactory.Create();
            var handler = new GetAllPessoasHandler(db);
            var query = new GetAllPessoasQuery();

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
