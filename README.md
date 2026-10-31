[README.md](https://github.com/user-attachments/files/34213429/README.md)
# VersaTools

**VersaTools** is a modular backend project for building and integrating useful utilities, including AI-powered tools. It combines an **ASP.NET Core 10 Web API** with a **Python FastAPI service** for AI-related processing, with an emphasis on separation of concerns and extensibility.

## Overview

The project separates its main application API from the Python service that handles AI operations. The .NET solution is organized into layers to keep application logic independent of delivery and infrastructure concerns.

- **ASP.NET Core API** — entry point for the application's HTTP endpoints.
- **Application layer** — home for application use cases and orchestration.
- **Domain layer** — core business concepts and rules.
- **Infrastructure layer** — integration with external systems and services.
- **Persistence layer** — location for data-access implementations.
- **Python FastAPI service** — separate service for AI-related operations.
- **Docker** — containerized execution of the Python service.

## Tech Stack

| Area | Technology |
| --- | --- |
| Main backend | C#, ASP.NET Core 10 |
| AI service | Python, FastAPI, Uvicorn |
| Architecture | Layered / Clean Architecture-inspired separation |
| Service communication | HTTP / REST |
| Containerization | Docker |

## Project Structure

```text
VersaTools/
├── presentation/
│   └── VersaTools.API/          # ASP.NET Core Web API
├── src/
│   ├── Core/
│   │   ├── Application/         # Application logic
│   │   └── Domain/              # Domain logic
│   ├── Infrastructure/          # External integrations
│   └── Persistence/             # Data access
└── server/
    ├── server.py                # FastAPI application
    └── Dockerfile               # Python service container
```

> The structure above reflects the project's previously discussed layout. Adjust it if the current repository has changed.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://docs.docker.com/get-docker/) for the containerized Python service
- Python 3.10 or later if you prefer to run the AI service without Docker

### 1. Clone the repository

```bash
git clone <your-repository-url>
cd VersaTools
```

Replace `<your-repository-url>` with the actual repository URL.

### 2. Start the Python service with Docker

From the repository root (using `server/Dockerfile` and the repository root as the build context):

```bash
docker build -f server/Dockerfile -t versatools-server .
docker run -d -p 8000:8000 --name versatools-server versatools-server
```

The service is then expected to listen on `http://localhost:8000`.

Check container status or inspect its logs:

```bash
docker ps
docker logs versatools-server
```

**Note:** This assumes the Dockerfile is in `server/` and supports a repository-root build context. Adjust the command to match its actual `COPY` instructions if necessary.

### 3. Start the ASP.NET Core API

Open a separate terminal in the repository root and run:

```bash
dotnet restore
dotnet run --project presentation/VersaTools.API
```

The API will print its listening address in the terminal. Configure the API's connection to the Python service using your project's existing configuration.

## AI Service

An early version of the Python service exposed a text-generation endpoint:

```http
POST /generate
Content-Type: application/json
```

Example request:

```bash
curl -X POST http://localhost:8000/generate \
  -H "Content-Type: application/json" \
  -d '{"prompt":"Explain the difference between encryption and hashing."}'
```

The response format and available AI features depend on the model and service implementation currently configured in the repository.

## Architecture

The intended dependency direction is:

```text
Clients
   |
   v
VersaTools.API
   |
   v
Application  --->  Domain
   |
   +----------> Infrastructure / Persistence (implementations)
   |
   +----------> Python FastAPI AI service (HTTP)
```

This arrangement aims to make it easier to add new utilities, replace external integrations, and test application logic independently of the transport layer.

## Development Notes

- Keep domain rules independent of API and external-service details.
- Define contracts around integrations so implementations can be replaced.
- Keep AI model configuration and any API keys out of source control.
- Document each new tool's endpoint, request format, and response format as it is added.

## Project Status

**In development.** The documented structure and commands reflect an earlier iteration of VersaTools; available tools, endpoints, and configuration may evolve.

## License

No license is specified in this README. Add a `LICENSE` file if you decide to publish the project under a particular license.
