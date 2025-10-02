# ===== build =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ./ ./
RUN dotnet restore BloggingSystem.Api/BloggingSystem.Api.csproj
RUN dotnet publish BloggingSystem.Api/BloggingSystem.Api.csproj -c Release -o /app /p:UseAppHost=false

# ===== runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser
COPY --from=build /app ./
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "BloggingSystem.Api.dll"]
