using api.Application.Transacoes.Handlers;
using api.Application.Transacoes.Queries;
using api.Data;
using api.Models;
using api.Models.Enums;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace api.Tests.Application.Transacoes.Handlers
{
    public class GetTransacaoByIdHandlerTests
    {
        private AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task HandleAsync_QuandoTransacaoExistir_DeveRetornarTransacaoDto()
        {
            // Arrange
            var db = CreateDbContext();

            var pessoa = new Pessoa
            {
                Nome = "João"
            };

            var categoria = new Categoria
            {
                Descricao = "Salário"
            };

            db.Pessoas.Add(pessoa);
            db.Categorias.Add(categoria);
            await db.SaveChangesAsync();

            var transacao = new Transacao
            {
                Descricao = "Pagamento mensal",
                Valor = 5000,
                Tipo = TipoTransacao.Receita,
                PessoaId = pessoa.Id,
                CategoriaId = categoria.Id,
                Pessoa = pessoa,
                Categoria = categoria
            };

            db.Transacoes.Add(transacao);
            await db.SaveChangesAsync();

            var handler = new GetTransacaoByIdHandler(db);
            var query = new GetTransacaoByIdQuery(transacao.Id);

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(transacao.Id);
            result.Descricao.Should().Be("Pagamento mensal");
            result.Valor.Should().Be(5000);
            result.Tipo.Should().Be(TipoTransacao.Receita);

            result.Pessoa.Should().NotBeNull();
            result.Pessoa.Id.Should().Be(pessoa.Id);
            result.Pessoa.Nome.Should().Be("João");

            result.Categoria.Should().NotBeNull();
            result.Categoria.Id.Should().Be(categoria.Id);
            result.Categoria.Descricao.Should().Be("Salário");
        }

        [Fact]
        public async Task HandleAsync_QuandoTransacaoNaoExistir_DeveRetornarNull()
        {
            // Arrange
            var db = CreateDbContext();

            var handler = new GetTransacaoByIdHandler(db);
            var query = new GetTransacaoByIdQuery(999);

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().BeNull();
        }
    }
}
