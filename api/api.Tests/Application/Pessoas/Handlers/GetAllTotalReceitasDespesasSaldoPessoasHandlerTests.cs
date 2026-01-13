using api.Application.Pessoas.Handlers;
using api.Application.Pessoas.Queries;
using api.Models;
using api.Models.Enums;
using api.Tests.Shared;
using FluentAssertions;

namespace api.Tests.Application.Pessoas.Handlers
{
    public class GetAllTotalReceitasDespesasSaldoPessoasHandlerTests
    {
        [Fact]
        public async Task Deve_Retornar_Totais_De_Receita_Despesa_E_Saldo_Por_Pessoa()
        {
            // Arrange
            var db = DbContextFactory.Create();

            var pessoa = new Pessoa
            {
                Nome = "João",
                Transacoes = new List<Transacao>
                {
                    new Transacao { Valor = 1000, Tipo = TipoTransacao.Receita,Descricao = "Transacao1" },
                    new Transacao { Valor = 500, Tipo = TipoTransacao.Despesa,Descricao = "Transacao2" },
                    new Transacao { Valor = 300, Tipo = TipoTransacao.Despesa,Descricao = "Transacao3" },
                    new Transacao { Valor = 200, Tipo = TipoTransacao.Receita,Descricao = "Transacao4" }
                }
            };

            db.Pessoas.Add(pessoa);
            await db.SaveChangesAsync();

            var handler = new GetAllTotalReceitasDespesasSaldoPessoasHandler(db);
            var query = new GetAllTotalReceitasDespesasSaldoPessoasQuery();

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);

            var dto = result.First();

            dto.Nome.Should().Be("João");
            dto.TotalReceita.Should().Be(1200); // 1000 + 200
            dto.TotalDespesa.Should().Be(800);  // 500 + 300
            dto.Saldo.Should().Be(400);         // 1200 - 800
        }

        [Fact]
        public async Task Deve_Retornar_Valores_Zerados_Quando_Pessoa_Nao_Tiver_Transacoes()
        {
            // Arrange
            var db = DbContextFactory.Create();

            db.Pessoas.Add(new Pessoa
            {
                Nome = "Maria"
            });

            await db.SaveChangesAsync();

            var handler = new GetAllTotalReceitasDespesasSaldoPessoasHandler(db);
            var query = new GetAllTotalReceitasDespesasSaldoPessoasQuery();

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().HaveCount(1);

            var dto = result.First();

            dto.TotalReceita.Should().Be(0);
            dto.TotalDespesa.Should().Be(0);
            dto.Saldo.Should().Be(0);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_Pessoas()
        {
            // Arrange
            var db = DbContextFactory.Create();
            var handler = new GetAllTotalReceitasDespesasSaldoPessoasHandler(db);
            var query = new GetAllTotalReceitasDespesasSaldoPessoasQuery();

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
