using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApiLivraria.Application.Interfaces;
using WebApiLivraria.Application.Services;
using WebApiLivraria.Application.UseCases.Auth;
using WebApiLivraria.Application.UseCases.Avaliacao.Criar;
using WebApiLivraria.Application.UseCases.Avaliacao.Editar;
using WebApiLivraria.Application.UseCases.Avaliacao.Excluir;
using WebApiLivraria.Application.UseCases.Avaliacao.Listar;
using WebApiLivraria.Application.UseCases.Avaliacao.Resumo;
using WebApiLivraria.Application.UseCases.Favorito;
using WebApiLivraria.Application.UseCases.Leitura.Atualizar;
using WebApiLivraria.Application.UseCases.Leitura.Resumo;
using WebApiLivraria.Application.UseCases.ListaDesejo;
using WebApiLivraria.Application.UseCases.RankingLivro;
using WebApiLivraria.Application.UseCases.Usuarios.AtualizarPerfil;
using WebApiLivraria.Application.UseCases.Usuarios.Excluir;
using WebApiLivraria.Application.UseCases.Usuarios.Login;
using WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilCompletoUseCase;
using WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico;
using WebApiLivraria.Application.UseCases.Usuarios.RegistrarUsuario;
using WebApiLivraria.Application.UseCases.UsuarioSeguindo;
using WebApiLivraria.Application.UseCases.UsuarioSeguindo.ObterSeguidores;
using WebApiLivraria.Application.UseCases.UsuarioSeguindo.ObterSeguindo;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Infrastructure.Configurations;
using WebApiLivraria.Infrastructure.Contexts;
using WebApiLivraria.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// MongoDB
builder.Services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<MongoDbContext>();

// Repositórios
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IEditoraRepository, EditoraRepository>();
builder.Services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IFavoritoRepository, FavoritoRepository>();
builder.Services.AddScoped<IListaDesejoRepository, ListaDesejoRepository>();
builder.Services.AddScoped<IUsuarioSeguindoRepository, UsuarioSeguindoRepository>();
builder.Services.AddScoped<ISinopseRepository, SinopseRepository>();

// Serviços
builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IEditoraService, EditoraService>();
builder.Services.AddScoped<IRankingService, RankingService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// UseCases
builder.Services.AddScoped<IRankingLivroUseCase, RankingLivroUseCase>();
builder.Services.AddScoped<ICriarAvaliacaoUseCase, CriarAvaliacaoUseCase>();
builder.Services.AddScoped<IListarAvaliacoesPorLivroUseCase, ListarAvaliacoesPorLivroUseCase>();
builder.Services.AddScoped<IObterResumoAvaliacaoLivroUseCase, ObterResumoAvaliacaoLivroUseCase>();
builder.Services.AddScoped<IEditarAvaliacaoUseCase, EditarAvaliacaoUseCase>();
builder.Services.AddScoped<IExcluirAvaliacaoUseCase, ExcluirAvaliacaoUseCase>();
builder.Services.AddScoped<IAuthUseCase, AuthUseCase>();
builder.Services.AddScoped<IAdicionarFavoritoUseCase, AdicionarFavoritoUseCase>();
builder.Services.AddScoped<RemoverFavoritoUseCase>();
builder.Services.AddScoped<ListarFavoritosUseCase>();
builder.Services.AddScoped<IAdicionarListaDesejoUseCase, AdicionarListaDesejoUseCase>();
builder.Services.AddScoped<RemoverListaDesejoUseCase>();
builder.Services.AddScoped<ListarListaDesejoUseCase>();
builder.Services.AddScoped<AtualizarLeituraUseCase>();
builder.Services.AddScoped<ObterPerfilCompletoUseCase>();
builder.Services.AddScoped<AtualizarPerfilUseCase>();
builder.Services.AddScoped<ExcluirUsuarioUseCase>();
builder.Services.AddScoped<IObterPerfilPublicoUseCase, ObterPerfilPublicoUseCase>();
builder.Services.AddScoped<ObterResumoStatusLeituraUseCase>();
builder.Services.AddScoped<SeguirUsuarioHandler>();
builder.Services.AddScoped<DeixarDeSeguirUsuarioHandler>();
builder.Services.AddScoped<ObterSeguidoresHandler>();
builder.Services.AddScoped<ObterSeguindoHandler>();
builder.Services.AddScoped<LoginUsuarioUseCase>();
builder.Services.AddScoped<RegistrarUsuarioUseCase>();

// Autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiLivraria", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Informe o token JWT no campo abaixo. Exemplo: 'Bearer {seu_token}'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

// Middlewares customizados de exceção
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TratamentoExcecaoMiddleware>();

app.MapControllers();
app.Run();