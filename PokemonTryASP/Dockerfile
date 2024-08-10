FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PokemonTryASP/PokemonTryASP.csproj", "PokemonTryASP/"]
RUN dotnet restore "PokemonTryASP/PokemonTryASP.csproj"
COPY . .
WORKDIR "/src/PokemonTryASP"
RUN dotnet build "PokemonTryASP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokemonTryASP.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PokemonTryASP.dll"]