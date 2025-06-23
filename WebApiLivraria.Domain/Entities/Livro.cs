using System;
using System.Collections.Generic;

namespace WebApiLivraria.Domain.Entities
{
    public class Livro
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public int AutorId { get; private set; }
        public Autor Autor { get; private set; }
        public Editora Editora { get; private set; }
        public int EditoraId { get; private set; }
        public DateTime AnoPublicacao { get; private set; } 
        public List<LivroGenero> LivroGeneros { get; private set; } = new();
        public List<Avaliacao> Avaliacoes { get; private set; } = new();
        public string? ImagemUrl { get; private set; }

        public Livro(string titulo, int autorId, int editoraId, DateTime anoPublicacao, string? imagemUrl = null)
        {
            Titulo = titulo;
            AutorId = autorId;
            EditoraId = editoraId;
            AnoPublicacao = anoPublicacao;
            ImagemUrl = imagemUrl;
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

        public void AtualizarAnoPublicacao(DateTime ano)
        {
            AnoPublicacao = ano;
        }

        public void AtualizarImagemUrl(string? imagemUrl)
        {
            ImagemUrl = imagemUrl;
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
        public double NotaMedia => Avaliacoes.Any() ? Avaliacoes.Average(a => a.Nota) : 0;
    }
}
