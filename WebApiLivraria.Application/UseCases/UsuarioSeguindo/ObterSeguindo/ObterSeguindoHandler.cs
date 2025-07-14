using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Requests.UsuarioSeguindo;
using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.UsuarioSeguindo.ObterSeguindo
{
    public class ObterSeguindoHandler
    {
        private readonly IUsuarioSeguindoRepository _usuarioSeguindoRepository;

        public ObterSeguindoHandler(IUsuarioSeguindoRepository usuarioSeguindoRepository)
        {
            _usuarioSeguindoRepository = usuarioSeguindoRepository;
        }

        public async Task<List<UsuarioResumoDto>> HandleAsync(ObterSeguindoRequest request)
        {
            var seguindo = await _usuarioSeguindoRepository.ObterSeguindoAsync(request.UsuarioId);

            return seguindo.Select(u => new UsuarioResumoDto
            {
                Nome = u.Nome,
                NomeUsuario = u.NomeUsuario,
                FotoUrl = u.FotoUrl,
                Bio = u.Bio
            }).ToList();
        }
    }
}