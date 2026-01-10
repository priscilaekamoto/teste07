using api.Application.Categorias.Queries;
using api.Data;
using api.Models.Enums;
using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Application.Categorias.Handlers
{
    public class GetAllTotalReceitasDespesasSaldoCategoriaHandler : IQueryHandler<GetAllTotalReceitasdespesasSaldoCategoriaQuery, List<CategoriaTotalReceitasDespesasSaldoDto>>
    {
        private readonly AppDbContext _db;

        public GetAllTotalReceitasDespesasSaldoCategoriaHandler(AppDbContext db)
        {
            _db = db;
        }

        public Task<List<CategoriaTotalReceitasDespesasSaldoDto>> HandleAsync(GetAllTotalReceitasdespesasSaldoCategoriaQuery query)
        {
            return _db.Categorias
                .AsNoTracking()
               .Select(c => new CategoriaTotalReceitasDespesasSaldoDto
               {
                   Id = c.Id,
                   Descricao = c.Descricao,
                   TotalDespesa = c.Transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(h => h.Valor),
                   TotalReceita = c.Transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(h => h.Valor),
                   Saldo = c.Transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(h => h.Valor) - c.Transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(h => h.Valor)
               }).ToListAsync();
        }
    }
}
