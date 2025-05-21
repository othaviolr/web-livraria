using System;

namespace WebApiLivraria.Domain.Entities
{
    public class LivroGenero
    {
        public int LivroId { get; private set; }
        public Livro Livro { get; private set; }

        public int GeneroId { get; private set; }
        public Genero Genero { get; private set; }

        public LivroGenero(int livroId, int generoId)
        {
            LivroId = livroId;
            GeneroId = generoId;
        }

        protected LivroGenero() { }
    }
}
