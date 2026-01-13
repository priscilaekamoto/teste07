using api.Application.Categorias.Handlers;
using api.Application.Categorias.Queries;
using api.Models;
using api.Models.Enums;
using api.Tests.Shared;
using FluentAssertions;

namespace api.Tests.Application.Categorias.Handlers
{
    public class GetAllTotalReceitasDespesasSaldoCategoriaHandlerTests
    {
        [Fact]
        public async Task Deve_Retornar_Totais_De_Receita_Despesa_E_Saldo_Por_Categoria()
        {
            // Arrange
            var db = DbContextFactory.Create();

            var categoria = new Categoria
            {
                Descricao = "Alimentação",
                Transacoes = new List<Transacao>
                {
                    new Transacao { Valor = 100, Tipo = TipoTransacao.Despesa, Descricao="transacao1" },
                    new Transacao { Valor = 50, Tipo = TipoTransacao.Despesa, Descricao="transacao2" },
                    new Transacao { Valor = 300, Tipo = TipoTransacao.Receita, Descricao="transacao3" }
                }
            };

            db.Categorias.Add(categoria);
            await db.SaveChangesAsync();

            var handler = new GetAllTotalReceitasDespesasSaldoCategoriaHandler(db);
            var query = new GetAllTotalReceitasdespesasSaldoCategoriaQuery();

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);

            var dto = result.First();

            dto.Descricao.Should().Be("Alimentação");
            dto.TotalReceita.Should().Be(300);
            dto.TotalDespesa.Should().Be(150);
            dto.Saldo.Should().Be(150); // 300 - 150
        }

        [Fact]
        public async Task Deve_Retornar_Valores_Zerados_Quando_Categoria_Nao_Tiver_Transacoes()
        {
            // Arrange
            var db = DbContextFactory.Create();

            db.Categorias.Add(new Categoria
            {
                Descricao = "Educação"
            });

            await db.SaveChangesAsync();

            var handler = new GetAllTotalReceitasDespesasSaldoCategoriaHandler(db);
            var query = new GetAllTotalReceitasdespesasSaldoCategoriaQuery();

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
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_Categorias()
        {
            // Arrange
            var db = DbContextFactory.Create();
            var handler = new GetAllTotalReceitasDespesasSaldoCategoriaHandler(db);
            var query = new GetAllTotalReceitasdespesasSaldoCategoriaQuery();

            // Act
            var result = await handler.HandleAsync(query);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
