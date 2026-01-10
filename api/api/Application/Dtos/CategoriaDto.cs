using api.Models.Enums;

namespace api.Application.Dtos
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = null!;
        public Finalidade Finalidade { get; set; }
    }
}
