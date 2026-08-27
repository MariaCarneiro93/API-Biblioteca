using biblioteca.Models;

namespace biblioteca.Repositories
{
    public interface IEmprestimoRepository
    {
        Task<List<Emprestimo>> GetAllAsync();
        Task<Emprestimo?> GetByIdAsync(Guid id);
        Task AddAsync(Emprestimo emprestimo);
        Task UpdateAsync(Emprestimo emprestimo);
    }
}