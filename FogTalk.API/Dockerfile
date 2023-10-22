FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FogTalk.API/FogTalk.API.csproj", "FogTalk.API/"]
RUN dotnet restore "FogTalk.API/FogTalk.API.csproj"
COPY . .
WORKDIR "/src/FogTalk.API"
RUN dotnet build "FogTalk.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FogTalk.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FogTalk.API.dll"]
