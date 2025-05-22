using WebApiLivraria.Domain.Constantes.Autor;
using WebApiLivraria.Domain.Constantes.Editora;
using WebApiLivraria.Domain.Constantes.Genero;
using WebApiLivraria.Domain.Constantes.Livro;

namespace WebApiLivraria.Domain.Constantes
{
    public static class MensagensPadrao
    {
        public static string LivroCriadoSucesso => MensagensLivro.LivroCriadoSucesso;
        public static string AutorCriadoSucesso => MensagensAutor.AutorCriadoSucesso;
        public static string EditoraCriadaSucesso => MensagensEditora.EditoraCriadaSucesso;
        public static string GeneroCriadoSucesso => MensagensGenero.GeneroCriadoSucesso;
    }
}
