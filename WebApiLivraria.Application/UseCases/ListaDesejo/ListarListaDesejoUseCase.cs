using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.ListaDesejo;

public class ListarListaDesejoUseCase
{
    private readonly IListaDesejoRepository _repository;

    public ListarListaDesejoUseCase(IListaDesejoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ListaDesejoResponse>> Executar(Guid usuarioId)
    {
        var lista = await _repository.ListarPorUsuario(usuarioId);

        return lista.Select(ld => new ListaDesejoResponse(ld.Id, ld.UsuarioId, ld.LivroId, ld.DataCriacao))
                    .ToList();
    }
}