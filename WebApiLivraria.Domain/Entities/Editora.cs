using System;

namespace WebApiLivraria.Domain.Entities
{
    public class Editora
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        public Editora(string nome)
        {
            Nome = nome;
        }

        protected Editora() { }

        public void AtualizarNome(string nome)
        {
            Nome = nome;
        }
    }
}
