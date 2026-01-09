using api.Models.Enums;

namespace api.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = null!;
        public Finalidade Finalidade { get; set; }

    }
}
