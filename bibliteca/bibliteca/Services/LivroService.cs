using biblioteca.Models;
using biblioteca.Repositories;

namespace biblioteca.Services
{
    public class LivroService
    {
        private readonly ILivroRepository _repository;

        public LivroService(ILivroRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Livro>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Livro?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<(bool Sucesso, string Mensagem, Livro? Livro)> CreateAsync(Livro livro)
        {
            // Verifica se o ISBN já existe
            var existente = await _repository.GetByISBNAsync(livro.ISBN);

            if (existente != null)
            {
                return (false, "Já existe um livro cadastrado com esse ISBN.", null);
            }

            livro.Id = Guid.NewGuid();
            livro.Disponivel = true;

            await _repository.AddAsync(livro);

            return (true, "Livro cadastrado com sucesso.", livro);
        }

        public async Task<(bool Sucesso, string Mensagem)> UpdateAsync(
            Guid id,
            Livro dados)
        {
            var livro = await _repository.GetByIdAsync(id);

            if (livro == null)
            {
                return (false, "Livro não encontrado.");
            }

            // Verifica se outro livro já usa esse ISBN
            var isbnExistente = await _repository.GetByISBNAsync(dados.ISBN);

            if (isbnExistente != null && isbnExistente.Id != id)
            {
                return (false, "Já existe outro livro com esse ISBN.");
            }

            livro.Titulo = dados.Titulo;
            livro.Autor = dados.Autor;
            livro.ISBN = dados.ISBN;

            await _repository.UpdateAsync(livro);

            return (true, "Livro atualizado com sucesso.");
        }

        public async Task<(bool Sucesso, string Mensagem)> DeleteAsync(Guid id)
        {
            var livro = await _repository.GetByIdAsync(id);

            if (livro == null)
            {
                return (false, "Livro não encontrado.");
            }

            // Não permite excluir livro emprestado
            if (!livro.Disponivel)
            {
                return (false, "Não é possível excluir um livro emprestado.");
            }

            await _repository.DeleteAsync(livro);

            return (true, "Livro excluído com sucesso.");
        }
    }
}