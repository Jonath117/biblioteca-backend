# ETAPA 1: Construcción (Build)
# Usamos el SDK pesado que tiene las herramientas para compilar
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiamr la solución y restaurar las dependencias
# Esto se hace primero para aprovechar el caché de Docker
COPY ["BibliotecaVirtual.slnx", "./"]
COPY ["src/", "src/"]

RUN dotnet restore "src/Core/Web.API/Web.API.csproj"
RUN dotnet publish "src/Core/Web.API/Web.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ETAPA 2: Produccion (Runtime)
# imagen super ligera que solo sirve para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

EXPOSE 8080

# Copia los archivos compilados de la Etapa 1
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Web.API.dll"]