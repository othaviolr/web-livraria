using System.Threading.Tasks;
using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.ListaDesejo
{
    public class RemoverListaDesejoUseCase
    {
        private readonly IListaDesejoRepository _listaDesejoRepository;

        public RemoverListaDesejoUseCase(IListaDesejoRepository listaDesejoRepository)
        {
            _listaDesejoRepository = listaDesejoRepository;
        }

        public async Task Executar(string usuarioId, string livroId)
        {
            await _listaDesejoRepository.Remover(usuarioId, livroId);
        }
    }
}