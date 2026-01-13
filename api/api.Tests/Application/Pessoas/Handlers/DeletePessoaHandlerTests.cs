using api.Application.Pessoas.Commands;
using api.Application.Pessoas.Handlers;
using api.Models;
using api.Tests.Shared;
using FluentAssertions;

namespace api.Tests.Application.Pessoas.Handlers
{
    public class DeletePessoaHandlerTests
    {
        [Fact]
        public async Task Deve_Deletar_Pessoa_Quando_Existir()
        {
            // Arrange
            var db = DbContextFactory.Create();
            var uow = new TestUnitOfWork(db);

            var pessoa = new Pessoa
            {
                Nome = "Maria",
                Idade = 28
            };

            db.Pessoas.Add(pessoa);
            await db.SaveChangesAsync();

            var handler = new DeletePessoaHandler(db, uow);

            var command = new DeletePessoaCommand
            {
                Id = pessoa.Id
            };

            // Act
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();

            var pessoaNoBanco = db.Pessoas.Find(pessoa.Id);
            pessoaNoBanco.Should().BeNull();
        }

        [Fact]
        public async Task Deve_Retornar_False_Quando_Pessoa_Nao_Existir()
        {
            // Arrange
            var db = DbContextFactory.Create();
            var uow = new TestUnitOfWork(db);

            var handler = new DeletePessoaHandler(db, uow);

            var command = new DeletePessoaCommand
            {
                Id = 999 // id inexistente
            };

            // Act
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            db.Pessoas.Should().BeEmpty();
        }
    }
}
