using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace biblioteca.Models
{
    public class Emprestimo
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [StringLength(100, MinimumLength = 2)]
        public string NomeUsuario { get; set; } = string.Empty;

        public DateTime DataEmprestimo { get; set; }

        public DateTime? DataDevolucao { get; set; }

        public Guid LivroId { get; set; }

        [JsonIgnore]
        public Livro? Livro { get; set; }
    }
}
