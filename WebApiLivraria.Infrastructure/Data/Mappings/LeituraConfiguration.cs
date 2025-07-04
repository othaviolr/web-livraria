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
                .HasConversion<string>()
                .IsRequired();

            builder.Property(l => l.DataAtualizacao)
                .IsRequired();

            builder.HasOne(l => l.Livro)
                .WithMany()
                .HasForeignKey(l => l.LivroId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.Usuario)
                .WithMany()
                .HasForeignKey(l => l.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}