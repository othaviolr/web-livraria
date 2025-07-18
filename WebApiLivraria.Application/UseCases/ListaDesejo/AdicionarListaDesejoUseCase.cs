using System;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.ListaDesejo
{
    public class AdicionarListaDesejoUseCase : IAdicionarListaDesejoUseCase
    {
        private readonly IListaDesejoRepository _repository;

        public AdicionarListaDesejoUseCase(IListaDesejoRepository repository)
        {
            _repository = repository;
        }

        public async Task Executar(AdicionarListaDesejoRequest request)
        {
            var existe = await _repository.Existe(request.UsuarioId, request.LivroId);
            if (existe)
                throw new Exception("Livro já está na lista de desejos.");

            var listaDesejo = new Domain.Entities.ListaDesejo(request.UsuarioId.ToString(), request.LivroId);
            await _repository.Adicionar(listaDesejo);
        }
    }
}