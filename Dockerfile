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

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet ef migrations bundle --context CatalogDbContext --project src/Modules/Catalog/Catalog.Infrastructure/ --startup-project src/Core/Web.API/ -o /app/publish/bundle-catalog
RUN dotnet ef migrations bundle --context WorkspaceDbContext --project src/Modules/Workspace/Workspace.Infrastructure/ --startup-project src/Core/Web.API/ -o /app/publish/bundle-workspace
RUN dotnet ef migrations bundle --context WorkflowDbContext --project src/Modules/Workflow/Workflow.Infrastructure/ --startup-project src/Core/Web.API/ -o /app/publish/bundle-workflow
RUN dotnet ef migrations bundle --context IamDbContext --project src/Modules/IAM/IAM.Infrastructure/ --startup-project src/Core/Web.API/ -o /app/publish/bundle-iam
# ETAPA 2: Produccion (Runtime)
# imagen super ligera que solo sirve para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

EXPOSE 8080

# Copia los archivos compilados de la Etapa 1
COPY --from=build /app/publish .

COPY entrypoint.sh .

RUN sed -i 's/\r$//' ./entrypoint.sh
RUN chmod +x ./entrypoint.sh bundle-*

ENTRYPOINT ["./entrypoint.sh"]
