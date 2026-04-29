# Biblioteca Virtual - Backend 

Este repositorio contiene el Backend de la plataforma de publicación y revisión de artículos académicos. El proyecto está construido utilizando **.NET 10** bajo el enfoque de **Monolito Modular** y **Clean Architecture** (CQRS + MediatR).

---

## Requisitos Previos

Antes de levantar el proyecto, asegúrate de tener instalado lo siguiente en tu entorno local:

* [SDK de .NET 10.0](https://dotnet.microsoft.com/download) o superior.
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) (Necesario para la base de datos PostgreSQL).
* **IDE Recomendado:** JetBrains Rider o Visual Studio 2022.
* **Cliente de Base de Datos:** [DBeaver](https://dbeaver.io/) (Recomendado para visualizar los esquemas).

---

## Guía de Inicio Rápido (Setup Local)

Sigue estos pasos en orden para configurar el entorno en tu máquina por primera vez.

### 1. Levantar la Base de Datos (Docker)
No necesitas instalar PostgreSQL directamente. El proyecto incluye un archivo `docker-compose.yml` que levanta un contenedor aislado.  
Copia el .env.example y completa las credenciales  
Abre una terminal en la raíz del proyecto y ejecuta:
```bash
docker-compose up -d
```

## Restaurar Herramientas y Paquetes
Para asegurarte de que tienes Entity Framework Core CLI instalado y las dependencias del proyecto, ejecuta:

```Bash
dotnet tool restore
dotnet restore
``` 

## Aplicar las Migraciones (Sincronizar la BD)
Para que tu base de datos local tenga exactamente las mismas tablas y esquemas (iam, workspace, etc.) que el resto del equipo, debes ejecutar la actualización de Entity Framework.

Abre la terminal en la raíz de la solución y ejecuta:

```Bash
dotnet ef database update --project src/Modules/IAM/IAM.Infrastructure --startup-project src/Core/Web.API --context IamDbContext
```

```bash
dotnet ef database update --project src/Modules/Workspace/Workspace.Infrastructure --startup-project src/Core/Web.API --context WorkspaceDbContext
```

```bash
dotnet ef database update --project src/Modules/Workflow/Workflow.Infrastructure --startup-project src/Core/Web.API --context WorkflowDbContext
```

```bash
dotnet ef database update --project src/Modules/Catalog/Catalog.Infrastructure --startup-project src/Core/Web.API --context CatalogDbContext
```


Verificación: Abre DBeaver, conéctate a localhost:5432 y verifica que dentro de la base de datos biblioteca_db existan los esquemas correspondientes.

### Si falla
Entrar a cada Modulo y colocarse en la capa de Infrastructure:  
Ejecutar en cada uno:

```bash
dotnet add package Microsoft.EntityFrameworkCore
```

```bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

```bash
dotnet add package Microsoft.EntityFrameworkCore.Relational
```

## Estructura del Proyecto
El código está organizado estrictamente en módulos para garantizar un bajo acoplamiento:

- src/Core/Web.API: Es el punto de entrada (Host). No contiene lógica de negocio. Solo arranca el servidor, configura Swagger y maneja la Inyección de Dependencias central.

- src/Modules/: Contiene los 4 Contextos Delimitados independientes:

    - IAM: Gestión de Identidad, Roles y Accesos.

    - Workspace: Espacio de trabajo del estudiante (Borradores, Coautores, subida de PDFs).

    - Workflow: Máquina de estados para el arbitraje y revisión por parte de los docentes.

    - Catalog: Catálogo público para búsqueda de artículos aprobados y generación de citas.

Cada módulo se subdivide en las 4 capas de Clean Architecture: Domain, Application, Infrastructure y Presentation.

## Reglas de Contribución para el Equipo
Para mantener la calidad de la Arquitectura de Software durante el desarrollo:

1. **Ramas (Branches):** Nunca hagas commit directo a main. Crea una rama con el formato feature/nombre-del-caso-de-uso (ej. feature/subir-documento).

2. **Dependencias:** * Domain no puede tener dependencias externas.

    - Un módulo NO puede acceder directamente a la base de datos o repositorio de otro módulo. Toda comunicación entre módulos debe hacerse mediante eventos en memoria con MediatR.

3. **Migraciones:** Si modificas una entidad de Dominio y necesitas actualizar la base de datos, avisa al equipo y genera una nueva migración con un nombre descriptivo antes de hacer tu Pull Request.