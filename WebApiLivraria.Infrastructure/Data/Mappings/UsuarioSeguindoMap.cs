using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Infrastructure.Data.Mappings
{
    public class UsuarioSeguindoMap : IEntityTypeConfiguration<UsuarioSeguindo>
    {
        public void Configure(EntityTypeBuilder<UsuarioSeguindo> builder)
        {
            builder.ToTable("UsuariosSeguindo");

            builder.HasKey(us => new { us.SeguidorId, us.SeguindoId });

            builder.HasOne(us => us.Seguidor)
                .WithMany(u => u.Seguindo)
                .HasForeignKey(us => us.SeguidorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(us => us.Seguindo)
                .WithMany(u => u.Seguidores)
                .HasForeignKey(us => us.SeguindoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
