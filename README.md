# 📚 Livraria do Tavin - API

API RESTful para gerenciamento de uma livraria online, com arquitetura DDD, Entity Framework Core e foco em qualidade de código. Projeto desenvolvido como demonstração de boas práticas e preparação para processos seletivos de empresas como Uber, iFood, Nubank, entre outras.

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- Arquitetura DDD (Domain-Driven Design)
- AutoMapper
- FluentValidation
- Swagger / Swashbuckle

---

## ✅ Funcionalidades

- Cadastro de livros, autores e gêneros
- Relacionamento entre livros e gêneros (n:n)
- Avaliação de livros com notas e comentários
- Cálculo da média das avaliações
- Ranking de livros com filtros por:
  - Gênero
  - Ano de publicação
- Filtros no front-end com suporte via API
- Arquitetura modular e escalável

---

## 🧠 Em desenvolvimento

- Login com conta Google (OAuth 2.0)
- Sistema de interação com livros:
  - Favoritar
  - Adicionar à lista de desejos
- Perfis de usuários com histórico de interações
- Painel administrativo

---

## ▶️ Como rodar localmente

```bash
# 1. Clone o repositório
git clone https://github.com/othaviolr/web-livraria.git

# 2. Acesse a pasta do projeto
cd web-livraria

# 3. Configure o appsettings.json com a string de conexão do seu SQL Server

# 4. Execute as migrations
dotnet ef database update --project WebApiLivraria.Infrastructure

# 5. Rode a aplicação
dotnet run --project WebApiLivraria.API
