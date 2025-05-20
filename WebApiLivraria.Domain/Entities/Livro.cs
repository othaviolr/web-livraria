using System;
using System.Collections.Generic;

namespace WebApiLivraria.Domain.Entities
{
    public class Livro
    {
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public Guid AutorId { get; private set; }
        public Guid EditoraId { get; private set; }
        public List<LivroGenero> LivroGeneros { get; private set; } = new();

        public Livro(string titulo, Guid autorId, Guid editoraId)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            AutorId = autorId;
            EditoraId = editoraId;
        }

        protected Livro() { }

        public void AtualizarTitulo(string titulo)
        {
            Titulo = titulo;
        }

        public void AtualizarAutor(Guid autorId)
        {
            AutorId = autorId;
        }

        public void AtualizarEditora(Guid editoraId)
        {
            EditoraId = editoraId;
        }

        public void AdicionarGenero(Guid generoId)
        {
            if (LivroGeneros.Exists(lg => lg.GeneroId == generoId))
                return;

            LivroGeneros.Add(new LivroGenero(Id, generoId));
        }

        public void RemoverGenero(Guid generoId)
        {
            var livroGenero = LivroGeneros.Find(lg => lg.GeneroId == generoId);
            if (livroGenero != null)
                LivroGeneros.Remove(livroGenero);
        }

        public void LimparGeneros()
        {
            LivroGeneros.Clear();
        }
    }
}
