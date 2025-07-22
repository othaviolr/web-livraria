using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Requests.UsuarioSeguindo;
using WebApiLivraria.Domain.Repositories;
using MongoDB.Bson;

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
            if (!ObjectId.TryParse(request.UsuarioId, out var usuarioId))
                throw new ArgumentException("Id do usuário inválido.");

            var seguindo = await _usuarioSeguindoRepository.ObterSeguindoAsync(usuarioId.ToString());

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