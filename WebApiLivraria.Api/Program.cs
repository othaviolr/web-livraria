using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApiLivraria.Application.Interfaces;
using WebApiLivraria.Application.Services;
using WebApiLivraria.Application.UseCases.Auth;
using WebApiLivraria.Application.UseCases.Avaliacao.Criar;
using WebApiLivraria.Application.UseCases.Avaliacao.Listar;
using WebApiLivraria.Application.UseCases.RankingLivro;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Infrastructure.Context;
using WebApiLivraria.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IEditoraRepository, EditoraRepository>();
builder.Services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IEditoraService, EditoraService>();
builder.Services.AddScoped<IRankingService, RankingService>();

builder.Services.AddScoped<IRankingLivroUseCase, RankingLivroUseCase>();
builder.Services.AddScoped<ICriarAvaliacaoUseCase, CriarAvaliacaoUseCase>();
builder.Services.AddScoped<IListarAvaliacoesPorLivroUseCase, ListarAvaliacoesPorLivroUseCase>();
builder.Services.AddScoped<IAuthUseCase, AuthUseCase>();
builder.Services.AddScoped<ITokenService, TokenService>();

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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TratamentoExcecaoMiddleware>();

app.MapControllers();

app.Run();
