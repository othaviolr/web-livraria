using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.ListaDesejo
{
    public class ListarListaDesejoUseCase
    {
        private readonly IListaDesejoRepository _listaDesejoRepository;

        public ListarListaDesejoUseCase(IListaDesejoRepository listaDesejoRepository)
        {
            _listaDesejoRepository = listaDesejoRepository;
        }

        public async Task<List<ListaDesejoResponse>> Executar(Guid usuarioId)
        {
            var listaDesejos = await _listaDesejoRepository.ListarPorUsuario(usuarioId);

            return listaDesejos.Select(ld => new ListaDesejoResponse
            {
                LivroId = ld.LivroId,
                DataCriacao = ld.DataCriacao
            }).ToList();
        }

        public async Task<bool> VerificarListaDesejo(Guid usuarioId, int livroId)
        {
            return await _listaDesejoRepository.Existe(usuarioId, livroId);
        }
    }
}