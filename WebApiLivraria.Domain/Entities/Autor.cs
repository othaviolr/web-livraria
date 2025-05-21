using System;

namespace WebApiLivraria.Domain.Entities
{
    public class Autor
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        public Autor(string nome)
        {
            Nome = nome;
        }

        protected Autor() { }

        public void AtualizarNome(string nome)
        {
            Nome = nome;
        }
    }
}
