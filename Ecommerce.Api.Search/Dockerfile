#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Ecommerce.Api.Search/Ecommerce.Api.Search.csproj", "Ecommerce.Api.Search/"]
RUN dotnet restore "Ecommerce.Api.Search/Ecommerce.Api.Search.csproj"
COPY . .
WORKDIR "/src/Ecommerce.Api.Search"
RUN dotnet build "Ecommerce.Api.Search.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Api.Search.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Api.Search.dll"]