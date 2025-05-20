using System;

namespace WebApiLivraria.Domain.Entities
{
    public class LivroGenero
    {
        public Guid LivroId { get; private set; }
        public Livro Livro { get; private set; }

        public Guid GeneroId { get; private set; }
        public Genero Genero { get; private set; }

        public LivroGenero(Guid livroId, Guid generoId)
        {
            LivroId = livroId;
            GeneroId = generoId;
        }

        protected LivroGenero() { }
    }
}
