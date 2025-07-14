using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Requests.UsuarioSeguindo;
using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.UsuarioSeguindo.ObterSeguidores
{
    public class ObterSeguidoresHandler
    {
        private readonly IUsuarioSeguindoRepository _usuarioSeguindoRepository;

        public ObterSeguidoresHandler(IUsuarioSeguindoRepository usuarioSeguindoRepository)
        {
            _usuarioSeguindoRepository = usuarioSeguindoRepository;
        }

        public async Task<List<UsuarioResumoDto>> HandleAsync(ObterSeguidoresRequest request)
        {
            var seguidores = await _usuarioSeguindoRepository.ObterSeguidoresAsync(request.UsuarioId);

            return seguidores.Select(u => new UsuarioResumoDto
            {
                Nome = u.Nome,
                NomeUsuario = u.NomeUsuario,
                FotoUrl = u.FotoUrl,
                Bio = u.Bio
            }).ToList();
        }
    }
}