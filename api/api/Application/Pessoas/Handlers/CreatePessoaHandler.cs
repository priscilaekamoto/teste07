using api.Application.Pessoas.Commands;
using api.Application.Pessoas.Dtos;
using api.Data;
using api.Models;
using api.Application.Mediator.Interfaces;

namespace api.Application.Pessoas.Handlers
{
    public class CreatePessoaHandler : ICommandHandler<CreatePessoaCommand, PessoaDto>
    {
        private readonly AppDbContext _db;
        public CreatePessoaHandler(AppDbContext db) => _db = db;

        public async Task<PessoaDto> HandleAsync(CreatePessoaCommand command, CancellationToken cancellationToken)
        {
            var pessoa = new Pessoa
            {
                Nome = command.Nome,
                Idade = command.Idade
            };

            _db.Pessoas.Add(pessoa);
            await _db.SaveChangesAsync();

            return new PessoaDto { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade };
        }
    }
}