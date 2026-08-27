using System.ComponentModel.DataAnnotations;

namespace biblioteca.Models
{
    public class Livro
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(150, MinimumLength = 2)]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O autor é obrigatório.")]
        [StringLength(100)]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ISBN é obrigatório.")]
        [StringLength(13, MinimumLength = 10)]
        public string ISBN { get; set; } = string.Empty;

        public bool Disponivel { get; set; } = true;

        public ICollection<Emprestimo> Emprestimos { get; set; }
            = new List<Emprestimo>();
    }
}