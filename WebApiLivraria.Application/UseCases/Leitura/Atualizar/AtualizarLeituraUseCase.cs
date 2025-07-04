using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Infrastructure.Context;
using EntLeitura = WebApiLivraria.Domain.Entities.Leitura;

namespace WebApiLivraria.Application.UseCases.Leitura.Atualizar
{
    public class AtualizarLeituraUseCase
    {
        private readonly AppDbContext _context;

        public AtualizarLeituraUseCase(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EntLeitura> ExecutarAsync(Guid usuarioId, AtualizarLeituraDto dto)
        {
            var leitura = await _context.Leituras
                .FirstOrDefaultAsync(l => l.UsuarioId == usuarioId && l.LivroId == dto.LivroId);

            if (leitura == null)
            {
                leitura = new EntLeitura(usuarioId, dto.LivroId, dto.Status);
                await _context.Leituras.AddAsync(leitura);
            }
            else
            {
                leitura.AtualizarStatus(dto.Status);
                _context.Leituras.Update(leitura);
            }

            await _context.SaveChangesAsync();

            return leitura;
        }
    }
}