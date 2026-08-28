using biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace biblioteca.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ISBN não pode ser duplicado
            modelBuilder.Entity<Livro>()
                .HasIndex(l => l.ISBN)
                .IsUnique();
    
        }
    }
}