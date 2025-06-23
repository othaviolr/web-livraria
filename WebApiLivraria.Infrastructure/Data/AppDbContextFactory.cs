using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using WebApiLivraria.Infrastructure.Context;

namespace WebApiLivraria.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Caminho para a pasta do projeto API onde está o appsettings.json
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "WebApiLivraria.Api");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("WebApiLivraria.Infrastructure"));

            return new AppDbContext(builder.Options);
        }
    }
}