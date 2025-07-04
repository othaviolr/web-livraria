using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Infrastructure.Data.Mappings;

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
        public DbSet<Leitura> Leituras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Autor>(entity =>
            {
                entity.Property(a => a.Nome)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(a => a.Biografia)
                    .HasMaxLength(2000)
                    .IsRequired(false);

                entity.Property(a => a.LocalNascimento)
                    .HasMaxLength(200)
                    .IsRequired(false);

                entity.Property(a => a.FotoUrl)
                    .HasMaxLength(500)
                    .IsRequired(false);

                entity.Property(a => a.DataNascimento)
                    .IsRequired(false);

                entity.HasOne(a => a.Editora)
                      .WithMany(e => e.Autores)
                      .HasForeignKey(a => a.EditoraId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Editora>(entity =>
            {
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Biografia)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.ImagemUrl)
                    .IsRequired();

                entity.HasMany(e => e.Autores)
                      .WithOne(a => a.Editora)
                      .HasForeignKey(a => a.EditoraId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

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

            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Avaliacoes)
                .HasForeignKey(a => a.UsuarioId)
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

            modelBuilder.Entity<Favorito>(entity =>
            {
                entity.HasKey(f => f.Id);

                entity.HasOne(f => f.Usuario)
                      .WithMany()
                      .HasForeignKey(f => f.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(f => f.Livro)
                      .WithMany()
                      .HasForeignKey(f => f.LivroId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(f => f.DataCriacao)
                      .IsRequired();
            });

            modelBuilder.Entity<ListaDesejo>(entity =>
            {
                entity.HasKey(ld => ld.Id);

                entity.HasOne(ld => ld.Usuario)
                      .WithMany()
                      .HasForeignKey(ld => ld.UsuarioId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ld => ld.Livro)
                      .WithMany()
                      .HasForeignKey(ld => ld.LivroId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(ld => ld.DataCriacao)
                      .IsRequired();
            });

            modelBuilder.ApplyConfiguration(new LeituraConfiguration());
        }
    }
}