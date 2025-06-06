# 📚 Livraria do Tavin - API

Uma API RESTful moderna para gerenciamento de uma livraria online.  
Construída com **arquitetura DDD**, **Entity Framework Core**, e foco em **qualidade de código, escalabilidade e boas práticas**.

---

## 🛠️ Tecnologias e Ferramentas

- ✅ **ASP.NET Core 8**
- ✅ **Entity Framework Core**
- ✅ **SQL Server**
- ✅ **Domain-Driven Design (DDD)**
- ✅ **AutoMapper**
- ✅ **FluentValidation**
- ✅ **Swagger / Swashbuckle** – documentação interativa

---

## ✨ Funcionalidades Implementadas

- 📘 Cadastro e gerenciamento de **livros**, **autores** e **gêneros**
- 🔗 Relacionamento **n:n** entre livros e gêneros
- ⭐ Sistema de **avaliação de livros** com notas e comentários
- 📊 Cálculo automático da **média de avaliações**
- 🏆 **Ranking de livros** com suporte a filtros por:
  - 🎭 Gênero
  - 📅 Ano de publicação
- 📎 Estrutura modular e escalável, com separação de responsabilidades

---

## 🚧 Em Desenvolvimento

- 🔐 Autenticação com **Google OAuth 2.0**
- ❤️ Sistema de interação:
  - Favoritar livros
  - Adicionar à lista de desejos
- 👤 Perfis de usuários com histórico de interações
- 🛠️ Painel administrativo para gerenciamento completo

---

## 🧪 Como Executar Localmente

```bash
# 1. Clone o repositório
git clone https://github.com/othaviolr/web-livraria.git

# 2. Acesse a pasta do projeto
cd web-livraria

# 3. Configure a string de conexão no appsettings.json

# 4. Execute as migrations
dotnet ef database update --project WebApiLivraria.Infrastructure

# 5. Rode a aplicação
dotnet run --project WebApiLivraria.API
