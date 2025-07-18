using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiLivraria.Domain.Entities
{
    public class Livro
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = null!;

        public string Titulo { get; private set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string AutorId { get; private set; }

        [BsonIgnore]
        public Autor Autor { get; private set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string EditoraId { get; private set; }

        [BsonIgnore]
        public Editora Editora { get; private set; } = null!;

        public DateTime AnoPublicacao { get; private set; }

        public int NumeroPaginas { get; private set; }

        public string Idioma { get; private set; }

        public string? ImagemUrl { get; private set; }

        [BsonIgnore]
        public Sinopse? Sinopse { get; private set; }

        [BsonIgnore]
        public List<LivroGenero> LivroGeneros { get; private set; } = new();

        [BsonIgnore]
        public List<Avaliacao> Avaliacoes { get; private set; } = new();

        [BsonIgnore]
        public ICollection<Favorito> Favoritos { get; private set; } = new List<Favorito>();

        [BsonIgnore]
        public ICollection<ListaDesejo> ListasDesejo { get; private set; } = new List<ListaDesejo>();

        [BsonIgnore]
        public ICollection<Leitura> Leituras { get; private set; } = new List<Leitura>();

        protected Livro() { }

        public Livro(
            string titulo,
            string autorId,
            string editoraId,
            DateTime anoPublicacao,
            int numeroPaginas,
            string idioma,
            string? imagemUrl = null)
        {
            if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título é obrigatório.");
            if (string.IsNullOrWhiteSpace(autorId)) throw new ArgumentException("AutorId é obrigatório.");
            if (string.IsNullOrWhiteSpace(editoraId)) throw new ArgumentException("EditoraId é obrigatório.");
            if (numeroPaginas <= 0) throw new ArgumentException("Número de páginas inválido.");
            if (string.IsNullOrWhiteSpace(idioma)) throw new ArgumentException("Idioma é obrigatório.");

            Id = ObjectId.GenerateNewId().ToString();
            Titulo = titulo;
            AutorId = autorId;
            EditoraId = editoraId;
            AnoPublicacao = anoPublicacao;
            NumeroPaginas = numeroPaginas;
            Idioma = idioma;
            ImagemUrl = imagemUrl;
        }

        public void AtualizarTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título inválido.");
            Titulo = titulo;
        }

        public void AtualizarAutor(string autorId)
        {
            if (string.IsNullOrWhiteSpace(autorId)) throw new ArgumentException("AutorId inválido.");
            AutorId = autorId;
        }

        public void AtualizarEditora(string editoraId)
        {
            if (string.IsNullOrWhiteSpace(editoraId)) throw new ArgumentException("EditoraId inválido.");
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

        public void AtualizarSinopse(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) throw new ArgumentException("Texto da sinopse inválido.");

            if (Sinopse == null)
            {
                if (string.IsNullOrEmpty(Id))
                    throw new InvalidOperationException("Não é possível criar sinopse antes de o livro ter um Id.");

                Sinopse = new Sinopse(this.Id, texto);
            }
            else
            {
                Sinopse.AtualizarTexto(texto);
            }
        }

        public void AdicionarGenero(string generoId)
        {
            if (!LivroGeneros.Exists(lg => lg.GeneroId == generoId))
                LivroGeneros.Add(new LivroGenero(Id, generoId));
        }

        public void RemoverGenero(string generoId)
        {
            var livroGenero = LivroGeneros.Find(lg => lg.GeneroId == generoId);
            if (livroGenero != null)
                LivroGeneros.Remove(livroGenero);
        }

        public void LimparGeneros()
        {
            LivroGeneros.Clear();
        }

        public void AtualizarNumeroPaginas(int numeroPaginas)
        {
            if (numeroPaginas <= 0) throw new ArgumentException("Número de páginas inválido.");
            NumeroPaginas = numeroPaginas;
        }

        public void AtualizarIdioma(string idioma)
        {
            if (string.IsNullOrWhiteSpace(idioma)) throw new ArgumentException("Idioma inválido.");
            Idioma = idioma;
        }

        public void AdicionarOuAtualizarLeitura(Leitura leitura)
        {
            var leituraExistente = Leituras.FirstOrDefault(l => l.UsuarioId == leitura.UsuarioId && l.LivroId == leitura.LivroId);

            if (leituraExistente == null)
            {
                Leituras.Add(leitura);
            }
            else
            {
                leituraExistente.AtualizarStatus(leitura.Status);
            }
        }

        public void RemoverLeitura(string usuarioId)
        {
            var leitura = Leituras.FirstOrDefault(l => l.UsuarioId == usuarioId);
            if (leitura != null)
                Leituras.Remove(leitura);
        }

        public double NotaMedia => Avaliacoes.Any() ? Avaliacoes.Average(a => a.Nota) : 0;
    }
}