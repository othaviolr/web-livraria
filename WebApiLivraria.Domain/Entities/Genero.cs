using System;

namespace WebApiLivraria.Domain.Entities
{
    public class Genero
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        public Genero(string nome)
        {
            Nome = nome;
        }

        protected Genero() { }

        public void AtualizarNome(string nome)
        {
            Nome = nome;
        }
    }
}
