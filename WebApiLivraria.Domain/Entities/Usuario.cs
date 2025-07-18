using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Entities
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = null!;

        public string Nome { get; private set; }
        public string Email { get; private set; }

        public string? NomeUsuario { get; private set; }
        public string? FotoUrl { get; private set; }
        public string? Cidade { get; private set; }
        public string? Bio { get; private set; }
        public string Role { get; private set; } = "Leitor";

        public string? SenhaHash { get; private set; }
        public string? TokenRecuperacaoSenha { get; private set; }
        public DateTime? DataExpiracaoTokenRecuperacaoSenha { get; private set; }

        [BsonIgnore]
        private ICollection<Avaliacao> _avaliacoes = new List<Avaliacao>();

        [BsonIgnore]
        private ICollection<ListaDesejo> _listasDesejo = new List<ListaDesejo>();

        [BsonIgnore]
        private ICollection<Favorito> _favoritos = new List<Favorito>();

        [BsonIgnore]
        private ICollection<Leitura> _livrosLidos = new List<Leitura>();

        [BsonIgnore]
        private ICollection<UsuarioSeguindo> _seguidores = new List<UsuarioSeguindo>();

        [BsonIgnore]
        private ICollection<UsuarioSeguindo> _seguindo = new List<UsuarioSeguindo>();

        [BsonIgnore]
        public IEnumerable<Avaliacao> Avaliacoes => _avaliacoes;

        [BsonIgnore]
        public IEnumerable<ListaDesejo> ListasDesejo => _listasDesejo;

        [BsonIgnore]
        public IEnumerable<Favorito> Favoritos => _favoritos;

        [BsonIgnore]
        public IEnumerable<Leitura> LivrosLidos => _livrosLidos;

        [BsonIgnore]
        public IEnumerable<UsuarioSeguindo> Seguidores => _seguidores;

        [BsonIgnore]
        public IEnumerable<UsuarioSeguindo> Seguindo => _seguindo;

        protected Usuario() { }

        public Usuario(string nome, string email, string? senhaHash = null)
        {
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Role = "Leitor";
        }

        public void SetId(string id)
        {
            Id = id;
        }

        public void AtualizarPerfil(string nomeUsuario, string? fotoUrl, string? cidade, string role, string? bio)
        {
            NomeUsuario = nomeUsuario;
            FotoUrl = fotoUrl;
            Cidade = cidade;
            Role = role;
            Bio = bio;
        }

        public void DefinirSenha(string senhaHash)
        {
            SenhaHash = senhaHash;
        }

        public void DefinirTokenRecuperacaoSenha(string token, DateTime dataExpiracao)
        {
            TokenRecuperacaoSenha = token;
            DataExpiracaoTokenRecuperacaoSenha = dataExpiracao;
        }

        public void RedefinirSenha(string novaSenhaHash)
        {
            SenhaHash = novaSenhaHash;
            TokenRecuperacaoSenha = null;
            DataExpiracaoTokenRecuperacaoSenha = null;
        }

        public void AdicionarAvaliacao(Avaliacao avaliacao)
        {
            _avaliacoes.Add(avaliacao);
        }

        public void DefinirAvaliacoes(IEnumerable<Avaliacao> avaliacoes)
        {
            _avaliacoes = new List<Avaliacao>(avaliacoes);
        }

        public void AdicionarListaDesejo(ListaDesejo listaDesejo)
        {
            _listasDesejo.Add(listaDesejo);
        }

        public void DefinirListasDesejo(IEnumerable<ListaDesejo> listas)
        {
            _listasDesejo = new List<ListaDesejo>(listas);
        }

        public void AdicionarFavorito(Favorito favorito)
        {
            _favoritos.Add(favorito);
        }

        public void DefinirFavoritos(IEnumerable<Favorito> favoritos)
        {
            _favoritos = new List<Favorito>(favoritos);
        }

        public void AdicionarLivroLido(Leitura leitura)
        {
            _livrosLidos.Add(leitura);
        }

        public void DefinirLivrosLidos(IEnumerable<Leitura> leituras)
        {
            _livrosLidos = new List<Leitura>(leituras);
        }

        public void AdicionarSeguidor(UsuarioSeguindo seguidor)
        {
            _seguidores.Add(seguidor);
        }

        public void DefinirSeguidores(IEnumerable<UsuarioSeguindo> seguidores)
        {
            _seguidores = new List<UsuarioSeguindo>(seguidores);
        }

        public void AdicionarSeguindo(UsuarioSeguindo seguindo)
        {
            _seguindo.Add(seguindo);
        }

        public void DefinirSeguindo(IEnumerable<UsuarioSeguindo> seguindo)
        {
            _seguindo = new List<UsuarioSeguindo>(seguindo);
        }
    }
}