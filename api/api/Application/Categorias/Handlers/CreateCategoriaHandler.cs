using api.Application.Categorias.Commands;
using api.Application.Categorias.Dtos;
using api.Application.Mediator.Interfaces;
using api.Data;
using api.Models;

namespace api.Application.Categorias.Handlers
{
    public class CreateCategoriaHandler : ICommandHandler<CreateCategoriaCommand, CategoriaDto>
    {
        private readonly AppDbContext _db;
        private readonly IUnitOfWork _uow;

        public CreateCategoriaHandler(AppDbContext db, IUnitOfWork uow)
        {
            _db = db;
            _uow = uow;
        }

        public async Task<CategoriaDto> HandleAsync(CreateCategoriaCommand command, CancellationToken cancellationToken)
        {
            await _uow.BeginTransactionAsync(cancellationToken);
            
            try
            {
                var categoria = new Categoria
                {
                    Descricao = command.Descricao,
                    Finalidade = command.Finalidade
                };

                _db.Categorias.Add(categoria);
                await _uow.SaveChangesAsync(cancellationToken);
                await _uow.CommitAsync(cancellationToken);

                return new CategoriaDto { Id = categoria.Id, Descricao = categoria.Descricao, Finalidade = categoria.Finalidade };
            } catch
            {
                await _uow.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
