using api.Application.Transacoes.Handlers;
using api.Application.Transacoes.Queries;
using api.Data;
using api.Models;
using api.Models.Enums;
using api.Tests.Shared;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace api.Tests.Application.Transacoes.Handlers
{
    public class GetAllTransacoesHandlerTests
    {
        [Fact]
        public async Task HandleAsync_DeveRetornarListaDeTransacoes()
        {
            // Arrange
            var db = DbContextFactory.Create();

            var pessoa = new Pessoa
            {
                Nome = "Maria"
            };

            var categoria = new Categoria
            {
                Descricao = "Alimentação",
                Finalidade = Finalidade.Despesa
            };

            db.Pessoas.Add(pessoa);
            db.Categorias.Add(categoria);
            await db.SaveChangesAsync();

            var transacao = new Transacao
            {
                Descricao = "Supermercado",
                Valor = 200,
                Tipo = TipoTransacao.Despesa,
                PessoaId = pessoa.Id,
                CategoriaId = categoria.Id,
                Pessoa = pessoa,
                Categoria = categoria
            };

            db.Transacoes.Add(transacao);
            await db.SaveChangesAsync();

            var handler = new GetAllTransacoesHandler(db);
            var query = new GetAllTransacoesQuery();

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);

            var dto = result.First();

            dto.Id.Should().Be(transacao.Id);
            dto.Descricao.Should().Be("Supermercado");
            dto.Valor.Should().Be(200);
            dto.Tipo.Should().Be(TipoTransacao.Despesa);

            dto.Pessoa.Should().NotBeNull();
            dto.Pessoa.Id.Should().Be(pessoa.Id);
            dto.Pessoa.Nome.Should().Be("Maria");

            dto.Categoria.Should().NotBeNull();
            dto.Categoria.Id.Should().Be(categoria.Id);
            dto.Categoria.Descricao.Should().Be("Alimentação");
        }

        [Fact]
        public async Task HandleAsync_QuandoNaoExistirTransacoes_DeveRetornarListaVazia()
        {
            // Arrange
            var db = DbContextFactory.Create();

            var handler = new GetAllTransacoesHandler(db);
            var query = new GetAllTransacoesQuery();

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
