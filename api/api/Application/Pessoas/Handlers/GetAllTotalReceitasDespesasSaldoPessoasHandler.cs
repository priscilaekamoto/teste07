using api.Application.Pessoas.Queries;
using api.Data;
using api.Models.Enums;
using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Application.Pessoas.Handlers
{
    public class GetAllTotalReceitasDespesasSaldoPessoasHandler : IQueryHandler<GetAllTotalReceitasDespesasSaldoPessoasQuery, List<PessoaTotalReceitaDespesasSaldoDto>>
    {
        private readonly AppDbContext _db;

        public GetAllTotalReceitasDespesasSaldoPessoasHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<PessoaTotalReceitaDespesasSaldoDto>> HandleAsync(GetAllTotalReceitasDespesasSaldoPessoasQuery query)
        {
            return await _db.Pessoas
               .AsNoTracking()
               .Select(p => new PessoaTotalReceitaDespesasSaldoDto { 
                   Id = p.Id, 
                   Nome = p.Nome, 
                   TotalDespesa = p.Transacoes.Where(t=> t.Tipo == TipoTransacao.Despesa).Sum(h => h.Valor),
                   TotalReceita = p.Transacoes.Where(t=> t.Tipo == TipoTransacao.Receita).Sum(h => h.Valor),
                   Saldo = p.Transacoes.Where(t=> t.Tipo == TipoTransacao.Receita).Sum(h => h.Valor) - p.Transacoes.Where(t=> t.Tipo == TipoTransacao.Despesa).Sum(h => h.Valor)
               }).ToListAsync();
        }
    }
}
