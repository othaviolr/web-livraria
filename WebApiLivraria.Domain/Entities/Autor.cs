using System;

namespace WebApiLivraria.Domain.Entities
{
    public class Autor
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }

        public Autor(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }

        protected Autor() { }

        public void AtualizarNome(string nome)
        {
            Nome = nome;
        }
    }
}
