FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ComposeExample.Web/ComposeExample.Web.csproj", "ComposeExample.Web/"]
COPY ["ComposeExample.Extensions/ComposeExample.Extensions.csproj", "ComposeExample.Extensions/"]
COPY ["ComposeExample.MoviesClient/ComposeExample.MoviesClient.csproj", "ComposeExample.MoviesClient/"]
RUN dotnet restore "ComposeExample.Web/ComposeExample.Web.csproj"
COPY . .
WORKDIR "/src/ComposeExample.Web"
RUN dotnet build "ComposeExample.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "ComposeExample.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ComposeExample.Web.dll"]