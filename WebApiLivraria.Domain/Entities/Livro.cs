using System.Collections.Generic;

namespace WebApiLivraria.Domain.Entities
{
    public class Livro
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public int AutorId { get; private set; }
        public int EditoraId { get; private set; }
        public List<LivroGenero> LivroGeneros { get; private set; } = new();

        public Livro(string titulo, int autorId, int editoraId)
        {
            Titulo = titulo;
            AutorId = autorId;
            EditoraId = editoraId;
        }

        protected Livro() { }

        public void AtualizarTitulo(string titulo)
        {
            Titulo = titulo;
        }

        public void AtualizarAutor(int autorId)
        {
            AutorId = autorId;
        }

        public void AtualizarEditora(int editoraId)
        {
            EditoraId = editoraId;
        }

        public void AdicionarGenero(int generoId)
        {
            if (LivroGeneros.Exists(lg => lg.GeneroId == generoId))
                return;

            LivroGeneros.Add(new LivroGenero(Id, generoId));
        }

        public void RemoverGenero(int generoId)
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
