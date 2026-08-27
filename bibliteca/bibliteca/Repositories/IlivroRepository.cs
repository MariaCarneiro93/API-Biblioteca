using biblioteca.Models;

namespace biblioteca.Repositories
{
    public interface ILivroRepository
    {
        Task<List<Livro>> GetAllAsync();
        Task<Livro?> GetByIdAsync(Guid id);
        Task<Livro?> GetByISBNAsync(string isbn);
        Task AddAsync(Livro livro);
        Task UpdateAsync(Livro livro);
        Task DeleteAsync(Livro livro);
    }
}