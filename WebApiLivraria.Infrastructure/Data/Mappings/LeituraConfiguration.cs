using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Infrastructure.Data.Mappings
{
    public class LeituraConfiguration : IEntityTypeConfiguration<Leitura>
    {
        public void Configure(EntityTypeBuilder<Leitura> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(l => l.DataAtualizacao)
                .IsRequired();

            builder.HasOne(l => l.Livro)
                .WithMany(l => l.Leituras)
                .HasForeignKey(l => l.LivroId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.Usuario)
                .WithMany(u => u.LivrosLidos)
                .HasForeignKey(l => l.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}