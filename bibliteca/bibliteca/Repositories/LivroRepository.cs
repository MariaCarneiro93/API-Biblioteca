using biblioteca.Data;
using biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace biblioteca.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly AppDbContext _context;

        public LivroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Livro>> GetAllAsync()
        {
            return await _context.Livros
                .ToListAsync();
        }

        public async Task<Livro?> GetByIdAsync(Guid id)
        {
            return await _context.Livros
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Livro?> GetByISBNAsync(string isbn)
        {
            return await _context.Livros
                .FirstOrDefaultAsync(l => l.ISBN == isbn);
        }

        public async Task AddAsync(Livro livro)
        {
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Livro livro)
        {
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Livro livro)
        {
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
        }
    }
}