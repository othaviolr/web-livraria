# 📚 Livraria do Tavin - API

Uma API RESTful moderna e escalável para gerenciamento de uma livraria online, construída com **ASP.NET Core**, **MongoDB** e **DDD**, com foco em **qualidade de código**, **organização** e **evolução contínua**.

> 🔮 Este projeto está em constante evolução para se tornar uma **plataforma completa para amantes de livros**.

---

## ⚙️ Tecnologias e Ferramentas

- ✅ **ASP.NET Core 8**
- ✅ **MongoDB**
- ✅ **Domain-Driven Design**
- ✅ **AutoMapper**
- ✅ **Swagger / Swashbuckle**
- ✅ **Autenticação com Google OAuth 2.0**

---

## ✨ Funcionalidades

- 📘 Cadastro e gerenciamento de **livros**, **autores**, **editoras** e **gêneros**
- 🔗 Relacionamento **n:n** entre livros e gêneros
- ⭐ Avaliação de livros com notas e comentários
- 📊 Cálculo da **média das avaliações**
- 🏆 **Ranking de livros** com filtros por:
  - 🎭 Gênero
  - 📅 Ano de publicação
- 📚 Estante personalizada com:
  - Status de leitura (Quero ler, Lendo, Lido etc)
  - Lista de desejos
  - Livros favoritos
- 👤 **Perfil público de usuários**
- 🔐 Autenticação via Google
- 🧩 Arquitetura modular e organizada por camadas
- 💡 Front-end em React com integração total via API
- 🛠️ Painel administrativo (em desenvolvimento)

---

## 🧠 Arquitetura

A API segue os princípios de DDD e Clean Architecture, dividida em:

- **Domain** → Entidades ricas, value objects e regras de negócio
- **Application** → Casos de uso e interfaces de repositório
- **Infrastructure** → Implementações reais com MongoDB e serviços externos
- **API** → Camada de apresentação com endpoints REST

---

## ▶️ Executando Localmente

```bash
# 1. Clone o repositório
git clone https://github.com/othaviolr/web-livraria.git

# 2. Acesse a pasta
cd web-livraria

# 3. Configure o appsettings.json com a string de conexão do MongoDB Atlas

# 4. Execute a API
dotnet run --project WebApiLivraria.API
