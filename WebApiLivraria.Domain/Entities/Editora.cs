using System.Collections.Generic;

namespace WebApiLivraria.Domain.Entities
{
    public class Editora
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Biografia { get; private set; }
        public string ImagemUrl { get; private set; }

        public ICollection<Autor> Autores { get; private set; } = new List<Autor>();

        public Editora(string nome, string biografia, string imagemUrl)
        {
            Nome = nome;
            Biografia = biografia;
            ImagemUrl = imagemUrl;
        }

        protected Editora() { }

        public void AtualizarNome(string nome)
        {
            Nome = nome;
        }

        public void AtualizarBiografia(string biografia)
        {
            Biografia = biografia;
        }

        public void AtualizarImagemUrl(string imagemUrl)
        {
            ImagemUrl = imagemUrl;
        }
    }
}