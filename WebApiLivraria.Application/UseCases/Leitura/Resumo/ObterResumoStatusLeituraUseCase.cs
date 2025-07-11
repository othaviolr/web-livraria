using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Domain.Enums;
using WebApiLivraria.Infrastructure.Context;

namespace WebApiLivraria.Application.UseCases.Leitura.Resumo
{
    public class ObterResumoStatusLeituraUseCase
    {
        private readonly AppDbContext _context;

        public ObterResumoStatusLeituraUseCase(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResumoStatusLeituraDto> ExecutarAsync(Guid usuarioId)
        {
            var leituras = await _context.Leituras
                .Where(l => l.UsuarioId == usuarioId)
                .ToListAsync();

            var resenhasCount = await _context.Avaliacoes
                .CountAsync(a => a.UsuarioId == usuarioId);

            var resumo = new ResumoStatusLeituraDto
            {
                QueroLer = leituras.Count(l => l.Status == StatusLeitura.QueroLer),
                Lendo = leituras.Count(l => l.Status == StatusLeitura.Lendo),
                Lidos = leituras.Count(l => l.Status == StatusLeitura.Lido),
                Abandonei = leituras.Count(l => l.Status == StatusLeitura.Abandonei),
                Relendo = leituras.Count(l => l.Status == StatusLeitura.Relendo),
                Resenhas = resenhasCount
            };

            return resumo;
        }
    }
}