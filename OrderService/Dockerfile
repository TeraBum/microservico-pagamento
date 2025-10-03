# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia o arquivo de projeto e restaura dependências
COPY *.csproj ./
RUN dotnet restore

# Copia todo o restante do código
COPY . ./

# Publica a aplicação em modo Release
RUN dotnet publish -c Release -o out

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Configura o Kestrel para escutar todas as interfaces
ENV DOTNET_URLS=http://+:5050

# Copia a aplicação publicada do stage build
COPY --from=build /app/out ./

# Expõe a porta
EXPOSE 5050

# Comando de entrada
ENTRYPOINT ["dotnet", "OrderService.dll"]
