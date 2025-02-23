FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

ENV KEYCLOAK_BASE_ADDRESS=""
ENV KEYCLOAK_CLIENT_ID=""
ENV KEYCLOAK_CLIENT_SECRET=""
ENV KEYCLOAK_REALM=""

USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MvcApplication/MvcApplication.csproj", "MvcApplication/"]
RUN dotnet restore "MvcApplication/MvcApplication.csproj"
COPY . .
WORKDIR "/src/MvcApplication"
RUN dotnet build "MvcApplication.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MvcApplication.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MvcApplication.dll"]
