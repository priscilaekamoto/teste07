using api.Application.Transacoes.Commands;
using api.Application.Transacoes.Handlers;
using api.Models;
using api.Models.Enums;
using api.Tests.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace api.Tests.Application.Transacoes.Handlers
{
    public class CreateTransacaoHandlerTests
    {
        [Fact]
        public async Task Deve_Criar_Transacao_Com_Sucesso()
        {
            // Arrange
            var db = DbContextFactory.Create();
            var uow = new TestUnitOfWork(db);

            var pessoa = new Pessoa { Nome = "João", Idade = 30 };
            var categoria = new Categoria
            {
                Descricao = "Salário",
                Finalidade = Finalidade.Receita
            };

            db.Pessoas.Add(pessoa);
            db.Categorias.Add(categoria);
            await db.SaveChangesAsync();

            var handler = new CreateTransacaoHandler(db, uow);

            var command = new CreateTransacaoCommand
            {
                Descricao = "Pagamento",
                Valor = 3000,
                Tipo = TipoTransacao.Receita,
                PessoaId = pessoa.Id,
                CategoriaId = categoria.Id
            };

            // Act
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.Code.Should().Be(StatusCodes.Status200OK);
            result.Id.Should().BeGreaterThan(0);
            result.Descricao.Should().Be("Pagamento");
            result.Valor.Should().Be(3000);
            result.Tipo.Should().Be(TipoTransacao.Receita);

            result.Pessoa!.Id.Should().Be(pessoa.Id);
            result.Categoria!.Id.Should().Be(categoria.Id);

            db.Transacoes.Should().HaveCount(1);
        }

        [Fact]
        public async Task Deve_Retornar_Erro_Quando_Pessoa_Nao_Existir()
        {
            // Arrange
            var db = DbContextFactory.Create();
            var uow = new TestUnitOfWork(db);

            var categoria = new Categoria
            {
                Descricao = "Alimentação",
                Finalidade = Finalidade.Despesa
            };

            db.Categorias.Add(categoria);
            await db.SaveChangesAsync();

            var handler = new CreateTransacaoHandler(db, uow);

            var command = new CreateTransacaoCommand
            {
                Descricao = "Mercado",
                Valor = 100,
                Tipo = TipoTransacao.Despesa,
                PessoaId = 999,
                CategoriaId = categoria.Id
            };

            // Act
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.Code.Should().Be(StatusCodes.Status400BadRequest);
            result.Messages.Should().Contain("Pessoal não encontrada!");
            db.Transacoes.Should().BeEmpty();
        }

        [Fact]
        public async Task Deve_Retornar_Erro_Quando_Categoria_Nao_Existir()
        {
            // Arrange
            var db = DbContextFactory.Create();
            var uow = new TestUnitOfWork(db);

            var pessoa = new Pessoa { Nome = "Ana", Idade = 25 };
            db.Pessoas.Add(pessoa);
            await db.SaveChangesAsync();

            var handler = new CreateTransacaoHandler(db, uow);

            var command = new CreateTransacaoCommand
            {
                Descricao = "Compra",
                Valor = 200,
                Tipo = TipoTransacao.Despesa,
                PessoaId = pessoa.Id,
                CategoriaId = 999
            };

            // Act
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.Code.Should().Be(StatusCodes.Status400BadRequest);
            result.Messages.Should().Contain("Categoria não encontrada!");
            db.Transacoes.Should().BeEmpty();
        }

        [Fact]
        public async Task Deve_Retornar_Erro_De_Validacao_Quando_Regra_De_Negocio_Falhar()
        {
            // Arrange
            var db = DbContextFactory.Create();
            var uow = new TestUnitOfWork(db);

            var pessoa = new Pessoa { Nome = "Carlos", Idade = 40 };
            var categoria = new Categoria
            {
                Descricao = "Salário",
                Finalidade = Finalidade.Receita
            };

            db.Pessoas.Add(pessoa);
            db.Categorias.Add(categoria);
            await db.SaveChangesAsync();

            var handler = new CreateTransacaoHandler(db, uow);

            var command = new CreateTransacaoCommand
            {
                Descricao = "Erro",
                Valor = -10, // inválido
                Tipo = TipoTransacao.Receita,
                PessoaId = pessoa.Id,
                CategoriaId = categoria.Id
            };

            // Act
            var result = await handler.HandleAsync(command, CancellationToken.None);

            // Assert
            result.Code.Should().NotBe(StatusCodes.Status200OK);
            result.Messages.Should().NotBeEmpty();
            db.Transacoes.Should().BeEmpty();
        }
    }
}
