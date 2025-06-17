# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copiar arquivos de projeto
COPY *.sln .
COPY Mottu.Locacao.Motos.Api/*.csproj ./Mottu.Locacao.Motos.Api/
COPY Mottu.Locacao.Domain/*.csproj ./Mottu.Locacao.Domain/
COPY Mottu.Locacao.Application/*.csproj ./Mottu.Locacao.Application/
COPY Mottu.Locacao.Data/*.csproj ./Mottu.Locacao.Data/
COPY Mottu.Locacao.IoC/*.csproj ./Mottu.Locacao.IoC/

# Restaurar pacotes
RUN dotnet restore

# Copiar tudo e publicar
COPY . .
WORKDIR /app/Mottu.Locacao.Motos.Api
RUN dotnet publish -c Release -o /out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "Mottu.Locacao.Motos.Api.dll"]