#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ComposeExample.Manage/ComposeExample.Manage.csproj", "ComposeExample.Manage/"]
COPY ["ComposeExample.Extensions/ComposeExample.Extensions.csproj", "ComposeExample.Extensions/"]
COPY ["ComposeExample.MoviesClient/ComposeExample.MoviesClient.csproj", "ComposeExample.MoviesClient/"]
RUN dotnet restore "ComposeExample.Manage/ComposeExample.Manage.csproj"
COPY . .
WORKDIR "/src/ComposeExample.Manage"
RUN dotnet build "ComposeExample.Manage.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "ComposeExample.Manage.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ComposeExample.Manage.dll"]