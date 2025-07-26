FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia todos os arquivos de projeto para manter a estrutura e restaurar pacotes
COPY WebApiLivraria.Api/*.csproj ./WebApiLivraria.Api/
COPY WebApiLivraria.Application/*.csproj ./WebApiLivraria.Application/
COPY WebApiLivraria.Infrastructure/*.csproj ./WebApiLivraria.Infrastructure/
COPY WebApiLivraria.Domain/*.csproj ./WebApiLivraria.Domain/

RUN dotnet restore ./WebApiLivraria.Api/WebApiLivraria.Api.csproj

COPY . .

WORKDIR /app/WebApiLivraria.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/WebApiLivraria.Api/out ./

EXPOSE 8080

ENTRYPOINT ["dotnet", "WebApiLivraria.Api.dll"]
