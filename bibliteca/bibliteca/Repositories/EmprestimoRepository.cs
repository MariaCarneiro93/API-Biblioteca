using biblioteca.Data;
using biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace biblioteca.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly AppDbContext _context;

        public EmprestimoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Emprestimo>> GetAllAsync()
        {
            return await _context.Emprestimos
                .Include(e => e.Livro)
                .ToListAsync();
        }

        public async Task<Emprestimo?> GetByIdAsync(Guid id)
        {
            return await _context.Emprestimos
                .Include(e => e.Livro)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Emprestimo emprestimo)
        {
            _context.Emprestimos.Add(emprestimo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Emprestimo emprestimo)
        {
            _context.Emprestimos.Update(emprestimo);
            await _context.SaveChangesAsync();
        }
    }
}