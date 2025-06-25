using System;
using System.Collections.Generic;

namespace WebApiLivraria.Domain.Entities
{
    public class Autor
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string? Biografia { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public string? LocalNascimento { get; private set; }
        public string? FotoUrl { get; private set; }

        public ICollection<Livro> Livros { get; private set; }

        protected Autor() { }

        public Autor(
            string nome,
            string? biografia = null,
            DateTime? dataNascimento = null,
            string? localNascimento = null,
            string? fotoUrl = null)
        {
            Nome = nome;
            Biografia = biografia;
            DataNascimento = dataNascimento;
            LocalNascimento = localNascimento;
            FotoUrl = fotoUrl;
            Livros = new List<Livro>();
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void Atualizar(
            string nome,
            string? biografia,
            DateTime? dataNascimento,
            string? localNascimento,
            string? fotoUrl)
        {
            Nome = nome;
            Biografia = biografia;
            DataNascimento = dataNascimento;
            LocalNascimento = localNascimento;
            FotoUrl = fotoUrl;
        }
    }
}