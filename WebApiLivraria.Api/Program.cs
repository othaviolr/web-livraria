using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Application.Interfaces;
using WebApiLivraria.Application.Services;
using WebApiLivraria.Application.UseCases.RankingLivro;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Infrastructure.Context;
using WebApiLivraria.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IEditoraRepository, EditoraRepository>();

builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IEditoraService, EditoraService>();
builder.Services.AddScoped<IRankingLivroUseCase, RankingLivroUseCase>();
builder.Services.AddScoped<IRankingService, RankingService>();

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

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TratamentoExcecaoMiddleware>();

app.MapControllers();

app.Run();
