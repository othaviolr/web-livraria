using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApiLivraria.Application.Interfaces;
using WebApiLivraria.Application.Services;
using WebApiLivraria.Application.UseCases.Auth;
using WebApiLivraria.Application.UseCases.Avaliacao.Criar;
using WebApiLivraria.Application.UseCases.Avaliacao.Editar;
using WebApiLivraria.Application.UseCases.Avaliacao.Excluir;
using WebApiLivraria.Application.UseCases.Avaliacao.Listar;
using WebApiLivraria.Application.UseCases.Avaliacao.Resumo;
using WebApiLivraria.Application.UseCases.Favorito;
using WebApiLivraria.Application.UseCases.ListaDesejo;
using WebApiLivraria.Application.UseCases.Leitura.Atualizar;
using WebApiLivraria.Application.UseCases.RankingLivro;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Infra.Data.Repositories;
using WebApiLivraria.Infrastructure.Context;
using WebApiLivraria.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("WebApiLivraria.Infrastructure")
    ));

// Repositórios
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IEditoraRepository, EditoraRepository>();
builder.Services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IFavoritoRepository, FavoritoRepository>();
builder.Services.AddScoped<IListaDesejoRepository, ListaDesejoRepository>();

// Serviços
builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IEditoraService, EditoraService>();
builder.Services.AddScoped<IRankingService, RankingService>();

// UseCases
builder.Services.AddScoped<IRankingLivroUseCase, RankingLivroUseCase>();
builder.Services.AddScoped<ICriarAvaliacaoUseCase, CriarAvaliacaoUseCase>();
builder.Services.AddScoped<IListarAvaliacoesPorLivroUseCase, ListarAvaliacoesPorLivroUseCase>();
builder.Services.AddScoped<IObterResumoAvaliacaoLivroUseCase, ObterResumoAvaliacaoLivroUseCase>();
builder.Services.AddScoped<IEditarAvaliacaoUseCase, EditarAvaliacaoUseCase>();
builder.Services.AddScoped<IExcluirAvaliacaoUseCase, ExcluirAvaliacaoUseCase>();
builder.Services.AddScoped<IAuthUseCase, AuthUseCase>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAdicionarFavoritoUseCase, AdicionarFavoritoUseCase>();
builder.Services.AddScoped<RemoverFavoritoUseCase>();
builder.Services.AddScoped<ListarFavoritosUseCase>();
builder.Services.AddScoped<IAdicionarListaDesejoUseCase, AdicionarListaDesejoUseCase>();
builder.Services.AddScoped<RemoverListaDesejoUseCase>();
builder.Services.AddScoped<ListarListaDesejoUseCase>();
builder.Services.AddScoped<AtualizarLeituraUseCase>();

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
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TratamentoExcecaoMiddleware>();
app.MapControllers();
app.Run();