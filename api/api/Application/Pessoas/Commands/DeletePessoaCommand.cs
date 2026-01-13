using api.Shared.Mediator.Interfaces;

namespace api.Application.Pessoas.Commands
{
    public sealed class DeletePessoaCommand : ICommand<bool>
    {
        public int Id { get; set; }
        public DeletePessoaCommand()
        {
            
        }
        public DeletePessoaCommand(int id) => Id = id;
    }
}