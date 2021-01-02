#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WarhammerCore.WebApi/WarhammerCore.WebApi.csproj", "WarhammerCore.WebApi/"]
COPY ["WarhammerCore.Data/WarhammerCore.Data.csproj", "WarhammerCore.Data/"]
COPY ["WarhammerCore.Abstract/WarhammerCore.Abstract.csproj", "WarhammerCore.Abstract/"]
COPY ["WarhammerCore.Business/WarhammerCore.Business.csproj", "WarhammerCore.Business/"]
RUN dotnet restore "WarhammerCore.WebApi/WarhammerCore.WebApi.csproj"
COPY . .
WORKDIR "/src/WarhammerCore.WebApi"
RUN dotnet build "WarhammerCore.WebApi.csproj" -c Release -o /app/build

ENV ConnectionStrings_SqlServer = ""

FROM build AS publish
RUN dotnet publish "WarhammerCore.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WarhammerCore.WebApi.dll"]