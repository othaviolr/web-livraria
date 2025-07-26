# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiando os arquivos .csproj individualmente para restaurar os pacotes
COPY WebApiLivraria.Api/WebApiLivraria.Api.csproj WebApiLivraria.Api/
COPY WebApiLivraria.Application/WebApiLivraria.Application.csproj WebApiLivraria.Application/
COPY WebApiLivraria.Infrastructure/WebApiLivraria.Infrastructure.csproj WebApiLivraria.Infrastructure/
COPY WebApiLivraria.Domain/WebApiLivraria.Domain.csproj WebApiLivraria.Domain/

# Restaurar os pacotes
RUN dotnet restore WebApiLivraria.Api/WebApiLivraria.Api.csproj

# Copiar todo o restante do código
COPY . .

# Publicar a aplicação
WORKDIR /src/WebApiLivraria.Api
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "WebApiLivraria.Api.dll"]
