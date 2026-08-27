using biblioteca.Models;
using biblioteca.Repositories;

namespace biblioteca.Services
{
    public class EmprestimoService
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;

        public EmprestimoService(
            IEmprestimoRepository emprestimoRepository,
            ILivroRepository livroRepository)
        {
            _emprestimoRepository = emprestimoRepository;
            _livroRepository = livroRepository;
        }

        public async Task<List<Emprestimo>> GetAllAsync()
        {
            return await _emprestimoRepository.GetAllAsync();
        }

        public async Task<Emprestimo?> GetByIdAsync(Guid id)
        {
            return await _emprestimoRepository.GetByIdAsync(id);
        }

        public async Task<(bool Sucesso, string Mensagem, Emprestimo? Emprestimo)> CreateAsync(
            Guid livroId,
            string nomeUsuario)
        {
            // Regra 1: o livro precisa existir
            var livro = await _livroRepository.GetByIdAsync(livroId);

            if (livro == null)
            {
                return (false, "Livro não encontrado.", null);
            }

            // Regra 2: o livro precisa estar disponível
            if (!livro.Disponivel)
            {
                return (false, "O livro já está emprestado.", null);
            }

            var emprestimo = new Emprestimo
            {
                Id = Guid.NewGuid(),
                LivroId = livroId,
                NomeUsuario = nomeUsuario,
                DataEmprestimo = DateTime.UtcNow
            };

            // O livro fica indisponível
            livro.Disponivel = false;

            await _emprestimoRepository.AddAsync(emprestimo);
            await _livroRepository.UpdateAsync(livro);

            return (true, "Empréstimo realizado com sucesso.", emprestimo);
        }

        public async Task<(bool Sucesso, string Mensagem)> DevolverAsync(Guid id)
        {
            var emprestimo = await _emprestimoRepository.GetByIdAsync(id);

            if (emprestimo == null)
            {
                return (false, "Empréstimo não encontrado.");
            }

            // Regra 3: não pode devolver duas vezes
            if (emprestimo.DataDevolucao != null)
            {
                return (false, "Este empréstimo já foi devolvido.");
            }

            emprestimo.DataDevolucao = DateTime.UtcNow;

            if (emprestimo.Livro != null)
            {
                emprestimo.Livro.Disponivel = true;

                await _livroRepository.UpdateAsync(emprestimo.Livro);
            }

            await _emprestimoRepository.UpdateAsync(emprestimo);

            return (true, "Livro devolvido com sucesso.");
        }
    }
}