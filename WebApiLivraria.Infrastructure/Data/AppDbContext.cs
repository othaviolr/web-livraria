using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Editora> Editoras { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<LivroGenero> LivroGeneros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade LivroGenero para relacionamento muitos-para-muitos
            modelBuilder.Entity<LivroGenero>()
                .HasKey(lg => new { lg.LivroId, lg.GeneroId });

            modelBuilder.Entity<LivroGenero>()
                .HasOne(lg => lg.Livro)
                .WithMany(l => l.LivroGeneros)
                .HasForeignKey(lg => lg.LivroId);

            modelBuilder.Entity<LivroGenero>()
                .HasOne(lg => lg.Genero)
                .WithMany()
                .HasForeignKey(lg => lg.GeneroId);

            // Configurações adicionais, se quiser, por exemplo:
            // modelBuilder.Entity<Livro>().Property(l => l.Titulo).IsRequired().HasMaxLength(200);
            // modelBuilder.Entity<Autor>().Property(a => a.Nome).IsRequired().HasMaxLength(100);
            // ... etc
        }
    }
}
