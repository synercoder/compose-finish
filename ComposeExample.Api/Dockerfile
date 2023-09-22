#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ComposeExample.Api/ComposeExample.Api.csproj", "ComposeExample.Api/"]
COPY ["ComposeExample.Extensions/ComposeExample.Extensions.csproj", "ComposeExample.Extensions/"]
RUN dotnet restore "ComposeExample.Api/ComposeExample.Api.csproj"
COPY . .
WORKDIR "/src/ComposeExample.Api"
RUN dotnet build "ComposeExample.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "ComposeExample.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ComposeExample.Api.dll"]