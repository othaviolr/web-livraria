using System;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.ListaDesejo;

public class RemoverListaDesejoUseCase
{
    private readonly IListaDesejoRepository _repository;

    public RemoverListaDesejoUseCase(IListaDesejoRepository repository)
    {
        _repository = repository;
    }

    public async Task Executar(Guid usuarioId, Guid livroId)
    {
        await _repository.Remover(usuarioId, livroId);
    }
}