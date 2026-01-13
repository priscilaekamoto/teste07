using api.Application.Categorias.Handlers;
using api.Application.Categorias.Queries;
using api.Models;
using api.Models.Enums;
using api.Tests.Shared;
using FluentAssertions;

namespace api.Tests.Application.Categorias.Handlers
{
    public class GetAllCategoriasHandlerTests
    {
        [Fact]
        public async Task Deve_Retornar_Todas_As_Categorias()
        {
            // Arrange
            var db = DbContextFactory.Create();

            db.Categorias.AddRange(
                new Categoria
                {
                    Descricao = "Alimentação",
                    Finalidade = Finalidade.Despesa
                },
                new Categoria
                {
                    Descricao = "Salário",
                    Finalidade = Finalidade.Receita
                }
            );

            await db.SaveChangesAsync();

            var handler = new GetAllCategoriasHandler(db);
            var query = new GetAllCategoriasQuery();

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            result.Should().Contain(c =>
                c.Descricao == "Alimentação" &&
                c.Finalidade == Finalidade.Despesa);

            result.Should().Contain(c =>
                c.Descricao == "Salário" &&
                c.Finalidade == Finalidade.Receita);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_Categorias()
        {
            // Arrange
            var db = DbContextFactory.Create();

            var handler = new GetAllCategoriasHandler(db);
            var query = new GetAllCategoriasQuery();

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
