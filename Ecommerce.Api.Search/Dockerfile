#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Ecoomerce.Api.Search/Ecommerce.Api.Search.csproj", "Ecoomerce.Api.Search/"]
RUN dotnet restore "Ecoomerce.Api.Search/Ecommerce.Api.Search.csproj"
COPY . .
WORKDIR "/src/Ecoomerce.Api.Search"
RUN dotnet build "Ecommerce.Api.Search.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Api.Search.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Api.Search.dll"]
