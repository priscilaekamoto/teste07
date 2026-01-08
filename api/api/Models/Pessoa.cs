using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Pessoa
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = null!;

        [Range(0, int.MaxValue)]
        public int Idade { get; set; }
    }
}