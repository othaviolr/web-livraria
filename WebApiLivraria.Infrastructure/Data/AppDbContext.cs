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
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<RankingLivro> RankingLivros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<ListaDesejo> ListasDesejo { get; set; }
        public DbSet<Sinopse> Sinopses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Livro)
                .WithMany(l => l.Avaliacoes)
                .HasForeignKey(a => a.LivroId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RankingLivro>()
                .HasOne(r => r.Livro)
                .WithMany()
                .HasForeignKey(r => r.LivroId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Livro>()
                .Property(l => l.ImagemUrl)
                .IsRequired(false);

            modelBuilder.Entity<Sinopse>()
                .HasKey(s => s.LivroId);

            modelBuilder.Entity<Livro>()
                .HasOne(l => l.Sinopse)
                .WithOne(s => s.Livro)
                .HasForeignKey<Sinopse>(s => s.LivroId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}