FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["./src/Mottu.Locacao.Motos.Api/Mottu.Locacao.Motos.Api.csproj", "Mottu.Locacao.Motos.Api/"]
COPY ["./src/Mottu.Locacao.Motos.Application/Mottu.Locacao.Motos.Application.csproj", "Mottu.Locacao.Motos.Application/"]
COPY ["./src/Mottu.Locacao.Motos.Data/Mottu.Locacao.Motos.Data.csproj", "Mottu.Locacao.Motos.Data/"]
COPY ["./src/Mottu.Locacao.Motos.Domain/Mottu.Locacao.Motos.Domain.csproj", "Mottu.Locacao.Motos.Domain/"]
COPY ["./src/Mottu.Locacao.Motos.IoC/Mottu.Locacao.Motos.IoC.csproj", "Mottu.Locacao.Motos.IoC/"]

RUN dotnet restore "Mottu.Locacao.Motos.Api/Mottu.Locacao.Motos.Api.csproj"

COPY . .
WORKDIR "src/Mottu.Locacao.Motos.Api/"
RUN dotnet build "Mottu.Locacao.Motos.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mottu.Locacao.Motos.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mottu.Locacao.Motos.Api.dll"]