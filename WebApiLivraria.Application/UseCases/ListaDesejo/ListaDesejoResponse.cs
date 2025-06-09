using System;

namespace WebApiLivraria.Application.UseCases.ListaDesejo;

public record ListaDesejoResponse(Guid Id, Guid UsuarioId, Guid LivroId, DateTime DataAdicionado);