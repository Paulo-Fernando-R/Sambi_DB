![Sambi_DB Logo](admin/public/Icon.png)

# Sambi_DB

Sambi_DB is a custom database engine built with .NET 8, featuring a modern admin dashboard developed in React. It provides a robust backend for managing collections and documents, complete with a clean and interactive user interface.

## 🚀 Features

-   **Custom Database Engine**: Built from the ground up using C# and .NET 8.
-   **Admin Dashboard**: A modern, responsive web interface for managing your database.
-   **Collection Management**: Create, view, and manage collections easily.
-   **Document Operations**: efficient storage and retrieval mechanisms.
-   **API Documentation**: Integrated Swagger UI for testing and exploring API endpoints.
-   **Windows Service Support**: Capable of running as a standalone Windows Service.

## 🛠 Tech Stack

### Backend

-   **Framework**: .NET 8 (ASP.NET Core)
-   **Language**: C#
-   **Documentation**: Swagger / OpenAPI
-   **Containerization**: Docker support

### Frontend (Admin Panel)

-   **Framework**: React
-   **Build Tool**: Vite
-   **Language**: TypeScript
-   **UI Library**: Material UI (MUI)
-   **State Management**: React Query (@tanstack/react-query)
-   **Routing**: React Router

## 📂 Project Structure

-   `Index/`: Core database logic, operations, and exception handling.
-   `Presenters/`: API Controllers handling HTTP requests.
-   `admin/`: Frontend source code (React application).
-   `db.csproj`: Project configuration and build targets.

## 🏁 Getting Started

### Prerequisites

-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   [Node.js](https://nodejs.org/) (for building the frontend)

### Installation & Running

1.  **Clone the repository:**

    ```bash
    git clone https://github.com/Paulo-Fernando-R/Sambi_DB.git
    cd Sambi_DB
    ```

2.  **Build and Run (IDE / CLI):**
    The project is configured to automatically build the frontend when you build the backend.

    ```bash
    dotnet build
    dotnet run
    ```

    _This will install frontend dependencies, build the React app, copy the artifacts to `wwwroot`, and start the .NET server._

3.  **Access the Application:**
    -   **Admin Dashboard**: `http://localhost:<PORT>/` (usually configured in `appsettings.json` or `Program.cs` ) (default port: 5000)
    -   **Swagger API**: `http://localhost:<PORT>/swagger`

### Development Mode

If you want to work on the frontend with hot-reload enabled:

1.  Navigate to the `admin` directory:

    ```bash
    cd admin
    ```

2.  Start the development server:

    ```bash
    npm install
    npm run dev
    ```

3.  Run the backend separately to serve the API.

## 🐳 Docker Support

The project includes a `Dockerfile` for containerization. You can build the docker image using:

```bash
docker build -t sambi_db .
```
