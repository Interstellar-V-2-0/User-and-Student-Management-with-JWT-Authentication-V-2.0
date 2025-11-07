# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos csproj(s) y restauramos dependencias en un solo paso para aprovechar cache
COPY ["UserStudentMgmt.Api/UserStudentMgmt.Api.csproj", "UserStudentMgmt.Api/"]
COPY ["UserStudentMgmt.Application/UserStudentMgmt.Application.csproj", "UserStudentMgmt.Application/"]
COPY ["UserStudentMgmt.Infrastructure/UserStudentMgmt.Infrastructure.csproj", "UserStudentMgmt.Infrastructure/"]
COPY ["UserStudentMgmt.Domain/UserStudentMgmt.Domain.csproj", "UserStudentMgmt.Domain/"]

RUN dotnet restore "UserStudentMgmt.Api/UserStudentMgmt.Api.csproj"

# Copiar todo y publicar
COPY . .
WORKDIR /src/UserStudentMgmt.Api
RUN dotnet publish -c Release -o /app/publish

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

COPY --from=build /app/publish .

# (Opcional) crear usuario no root para mayor seguridad
# RUN adduser --disabled-password appuser && chown -R appuser /app
# USER appuser

ENTRYPOINT ["dotnet", "UserStudentMgmt.Api.dll"]
