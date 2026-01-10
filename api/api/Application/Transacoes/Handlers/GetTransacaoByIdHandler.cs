using api.Application.Dtos;
using api.Application.Mediator.Interfaces;
using api.Application.Transacoes.Queries;
using api.Data;

namespace api.Application.Transacoes.Handlers
{
    public class GetTransacaoByIdHandler : IQueryHandler<GetTransacaoByIdQuery, TransacaoDto?>
    {
        private readonly AppDbContext _db;

        public GetTransacaoByIdHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<TransacaoDto?> HandleAsync(GetTransacaoByIdQuery query)
        {
           var t = await _db.Transacoes.FindAsync(query.Id);
              if (t == null) return null;
                return new TransacaoDto
                {
                    Id = t.Id,
                    Descricao = t.Descricao,
                    Valor = t.Valor,
                    Tipo = t.Tipo,
                    CategoriaId = t.CategoriaId,
                    PessoaId = t.PessoaId
                };
        }
    }
}
