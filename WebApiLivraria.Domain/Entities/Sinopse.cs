namespace WebApiLivraria.Domain.Entities
{
    public class Sinopse
    {
        public int LivroId { get; private set; }
        public string Texto { get; private set; }

        public virtual Livro Livro { get; private set; }

        protected Sinopse() { }

        public Sinopse(int livroId, string texto)
        {
            LivroId = livroId;
            Texto = texto;
        }

        public void AtualizarTexto(string texto)
        {
            Texto = texto;
        }
    }
}