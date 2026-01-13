using api.Application.Categorias.Handlers;
using api.Application.Categorias.Queries;
using api.Models;
using api.Models.Enums;
using api.Tests.Shared;
using FluentAssertions;

namespace api.Tests.Application.Categorias.Handlers
{
    public class GetCategoriaByIdHandlerTests
    {
        [Fact]
        public async Task Deve_Retornar_Categoria_Quando_Id_Existir()
        {
            // Arrange
            var db = DbContextFactory.Create();

            var categoria = new Categoria
            {
                Descricao = "Lazer",
                Finalidade = Finalidade.Despesa
            };

            db.Categorias.Add(categoria);
            await db.SaveChangesAsync();

            var handler = new GetCategoriaByIdHandler(db);
            var query = new GetCategoriaByIdQuery
            {
                Id = categoria.Id
            };

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(categoria.Id);
            result.Descricao.Should().Be("Lazer");
            result.Finalidade.Should().Be(Finalidade.Despesa);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Id_Nao_Existir()
        {
            // Arrange
            var db = DbContextFactory.Create();

            var handler = new GetCategoriaByIdHandler(db);
            var query = new GetCategoriaByIdQuery
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
