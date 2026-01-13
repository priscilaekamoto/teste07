using api.Application.Categorias.Commands;
using api.Application.Categorias.Handlers;
using api.Models.Enums;
using api.Tests.Shared;
using FluentAssertions;

namespace api.Tests.Application.Categorias.Handlers
{
    public class CreateCategoriaHandlerTests
    {
        [Fact]
        public async Task Deve_Criar_Categoria_Com_Sucesso()
        {
            // Arrange
            var db = DbContextFactory.Create();
            var uow = new TestUnitOfWork(db);

            var handler = new CreateCategoriaHandler(db, uow);

            var command = new CreateCategoriaCommand
            {
                Descricao = "Alimentação",
                Finalidade = Finalidade.Despesa
            };

            // Act
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Descricao.Should().Be("Alimentação");
            result.Finalidade.Should().Be(Finalidade.Despesa);

            var categoriaNoBanco = db.Categorias.Find(result.Id);
            categoriaNoBanco.Should().NotBeNull();
            categoriaNoBanco!.Descricao.Should().Be("Alimentação");
            categoriaNoBanco.Finalidade.Should().Be(Finalidade.Despesa);
        }
    }
}
