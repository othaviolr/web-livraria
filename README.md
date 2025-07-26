# 📚 Livraria do Tavin - API

Uma API RESTful robusta para gerenciamento de uma livraria online, com **DDD**, **EF Core** e foco em **escalabilidade, performance e qualidade de código**.

> 🔮 Este projeto está evoluindo para se tornar uma **plataforma completa de livraria**.

---

## ⚙️ Tecnologias e Ferramentas

- ✅ **ASP.NET Core 8**
- ✅ **Entity Framework Core**
- ✅ **SQL Server**
- ✅ **Domain-Driven Design (DDD)**
- ✅ **AutoMapper**
- ✅ **FluentValidation**
- ✅ **Swagger / Swashbuckle**
- ✅ **Google OAuth 2.0** – login social integrado

---

## ✨ Funcionalidades

- 📘 Cadastro e gerenciamento de **livros**, **autores** e **gêneros**
- 🔗 Relacionamento **n:n** entre livros e gêneros
- ⭐ Avaliação de livros com notas e comentários
- 📊 Cálculo da **média das avaliações**
- 🏆 **Ranking de livros** com filtros:
  - 🎭 Por gênero
  - 📅 Por ano de publicação
- ❤️ Sistema de interação com livros:
  - Favoritar
  - Lista de desejos
- 🔐 **Autenticação via Google**
- 🧩 Arquitetura modular, limpa e escalável
- 💡 Front-end moderno com **React** + consumo da API
- 👤 Perfis de usuário com histórico
- 🛠️ Painel administrativo
  
---

## 🚀 Em Breve

- 🛍️ **E-commerce completo** com carrinho, pedidos e checkout

---

## 🧠 Arquitetura

- Domain → Entidades e regras do domínio
- Application → Casos de uso e lógica de aplicação
- Infrastructure → Acesso a dados e integrações externas
- API → Camada de apresentação com controllers REST

---

## 📫 Contato

Desenvolvido com 💙 por **Othavio**  
[🔗 GitHub](https://github.com/othaviolr) | [✉️ Email](mailto:othavionogueira2003@gmail.com)

---

## ▶️ Executando Localmente

```bash
# 1. Clone o repositório
git clone https://github.com/othaviolr/web-livraria.git

# 2. Acesse a pasta
cd web-livraria

# 3. Configure o appsettings.json com a string de conexão

# 4. Aplique as migrations
dotnet ef database update --project WebApiLivraria.Infrastructure

# 5. Execute a API
dotnet run --project WebApiLivraria.API
