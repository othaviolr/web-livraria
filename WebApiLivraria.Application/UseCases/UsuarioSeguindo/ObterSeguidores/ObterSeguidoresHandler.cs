using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Requests.UsuarioSeguindo;
using WebApiLivraria.Domain.Repositories;
using MongoDB.Bson;

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
            if (!ObjectId.TryParse(request.UsuarioId, out var usuarioId))
                throw new ArgumentException("Id do usuário inválido.");

            var seguidores = await _usuarioSeguindoRepository.ObterSeguidoresAsync(usuarioId.ToString());

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