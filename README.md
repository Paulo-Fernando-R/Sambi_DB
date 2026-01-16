> [Ler em Português](README.pt-br.md)

![Sambi_DB Logo](admin/public/Icon.png)

# Sambi_DB

Sambi_DB is a custom database engine built with .NET 8, featuring a modern admin dashboard developed in React. It provides a robust backend for managing collections and documents, complete with a clean and interactive user interface.

## 🚀 Features

- **Custom Database Engine**: Built from the ground up using C# and .NET 8.
- **Admin Dashboard**: A modern, responsive web interface for managing your database.
- **Collection Management**: Create, view, and manage collections easily.
- **Document Operations**: efficient storage and retrieval mechanisms.
- **API Documentation**: Integrated Swagger UI for testing and exploring API endpoints.
- **Windows Service Support**: Capable of running as a standalone Windows Service.

## 🛠 Tech Stack

### Backend

- **Framework**: .NET 8 (ASP.NET Core)
- **Language**: C#
- **Documentation**: Swagger / OpenAPI
- **Containerization**: Docker support

### Frontend (Admin Panel)

- **Framework**: React
- **Build Tool**: Vite
- **Language**: TypeScript
- **UI Library**: Material UI (MUI)
- **State Management**: React Query (@tanstack/react-query)
- **Routing**: React Router

## 📂 Project Structure

- `Index/`: Core database logic, operations, and exception handling.
- `Presenters/`: API Controllers handling HTTP requests.
- `admin/`: Frontend source code (React application).
- `db.csproj`: Project configuration and build targets.

## 🏁 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (for building the frontend)

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
    - **Admin Dashboard**: `http://localhost:<PORT>/` (usually configured in `appsettings.json` or `Program.cs` ) (default port: 5000)
    - **Swagger API**: `http://localhost:<PORT>/swagger`

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

## 📚 API Documentation

Sambi_DB exposes a REST API to manage databases, collections, and documents (registers). Below is the list of available endpoints and their usage.

### 1. Database Operations

| Method   | Endpoint                          | Description                   | Query/Body Parameters                    |
| :------- | :-------------------------------- | :---------------------------- | :--------------------------------------- |
| `POST`   | `/Database/Create`                | Creates a new database.       | **Body:** `{ "DatabaseName": "string" }` |
| `DELETE` | `/Database/Delete/{DatabaseName}` | Deletes a database.           | **Body:** `{ "Confirm": true }`          |
| `GET`    | `/Database/List`                  | Lists all existing databases. | -                                        |

**Examples:**

_Create Database_

```json
{ "DatabaseName": "MyDatabase" }
```

_Delete Database_

```json
{ "Confirm": true }
```

### 2. Collection Operations

| Method   | Endpoint                            | Description                       | Query/Body Parameters                                                                          |
| :------- | :---------------------------------- | :-------------------------------- | :--------------------------------------------------------------------------------------------- |
| `POST`   | `/Collection/Create/{DatabaseName}` | Creates a new collection in a DB. | **Body:** `{ "CollectionName": "string" }`                                                     |
| `PUT`    | `/Collection/Update/{DatabaseName}` | Renames a collection.             | **Body:** `{ "CollectionName": "old_name", "NewCollectionName": "new_name", "Confirm": true }` |
| `DELETE` | `/Collection/Delete/{DatabaseName}` | Deletes a collection.             | **Body:** `{ "CollectionName": "string", "Confirm": true }`                                    |
| `GET`    | `/Collection/List/{DatabaseName}`   | Lists collections in a DB.        | -                                                                                              |

**Examples:**

_Create Collection_

```json
{ "CollectionName": "Users" }
```

_Rename Collection_

```json
{ "CollectionName": "Users", "NewCollectionName": "Customers", "Confirm": true }
```

_Delete Collection_

```json
{ "CollectionName": "Users", "Confirm": true }
```

### 3. Register (Document) Operations

| Method   | Endpoint                          | Description                      | Query/Body Parameters                                                               |
| :------- | :-------------------------------- | :------------------------------- | :---------------------------------------------------------------------------------- |
| `POST`   | `/Register/Create/{DatabaseName}` | Adds a document to a collection. | **Body:** `{ "CollectionName": "string", "Data": { ... } }`                         |
| `PUT`    | `/Register/Update/{DatabaseName}` | Updates a document.              | **Body:** `{ "CollectionName": "string", "RegisterId": "string", "Data": { ... } }` |
| `DELETE` | `/Register/Delete/{DatabaseName}` | Deletes a document.              | **Body:** `{ "CollectionName": "string", "RegisterId": "string", "Confirm": true }` |

**Examples:**

_add Register (Document)_

```json
{
  "CollectionName": "Users",
  "Data": {
    "name": "Jane Doe",
    "email": "jane@example.com"
  }
}
```

_Update Register_

```json
{
  "CollectionName": "Users",
  "RegisterId": "bbf2a446-bbe1-4c8d-b95a-2969923ea1ab",
  "Data": {
    "name": "Jane Does",
    "email": "jane.does@example.com"
  }
}
```

_Delete Register_

```json
{
  "CollectionName": "Users",
  "RegisterId": "bbf2a446-bbe1-4c8d-b95a-2969923ea1ab",
  "Confirm": true
}
```

#### Array Operations

Manage arrays within documents (e.g., adding tags or items to a list inside a document).

| Method   | Endpoint                                | Description                    | Query/Body Parameters                                                                                                                                                    |
| :------- | :-------------------------------------- | :----------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `POST`   | `/Register/Add/Array/{DatabaseName}`    | Adds an item to an array.      | **Body:** `{ "RegisterId": "string", "CollectionName": "string", "ArrayName": "string", "Data": { ... } }`                                                               |
| `PUT`    | `/Register/Update/Array/{DatabaseName}` | Updates an item in an array.   | **Body:** `{ "RegisterId": "string", "CollectionName": "string", "ArrayName": "string", "Property": "string", "Value": "string", "NewValue": { ... }, "Confirm": true }` |
| `DELETE` | `/Register/Delete/Array/{DatabaseName}` | Deletes an item from an array. | **Body:** `{ "RegisterId": "string", "CollectionName": "string", "ArrayName": "string", "Property": "string", "Value": "string", "Confirm": true }`                      |

**1. Add to Array**

```json
{
  "RegisterId": "bbf2a446-bbe1-4c8d-b95a-2969923ea1ab",
  "CollectionName": "Users",
  "ArrayName": "Tags",
  "Data": {
    "id": "2",
    "description": "developer"
  }
}
```

**2. Update Array Item**

```json
{
  "RegisterId": "bbf2a446-bbe1-4c8d-b95a-2969923ea1ab",
  "CollectionName": "Users",
  "ArrayName": "Tags",
  "Property": "id",
  "Value": "2",
  "NewValue": {
    "id": "2",
    "description": "lead-developer"
  },
  "Confirm": true
}
```

**3. Delete from Array**

```json
{
  "RegisterId": "bbf2a446-bbe1-4c8d-b95a-2969923ea1ab",
  "CollectionName": "Users",
  "ArrayName": "Tags",
  "Property": "id",
  "Value": "2",
  "Confirm": true
}
```

### 4. Query Operations

#### Get by ID

`POST /Query/ById/{DatabaseName}`

- **Body**:

```json
{
  "CollectionName": "string",
  "Id": "string"
}
```

#### Query by Properties

`POST /Query/ByProperty/{DatabaseName}`
Allows filtering documents based on specific conditions.

**Body Structure:**

```json
{
  "CollectionName": "string",
  "Limit": 20,
  "Skip": 0,
  "ConditionsBehavior": "&&", // "&&" (AND) or "||" (OR)
  "QueryConditions": [
    {
      "Key": "propertyName",
      "Value": "valueToSearch",
      "Operation": "==",
      "ArrayProperty": "" // Optional, for nested array properties
    }
  ]
}
```

### 5. Query Operators

When using `Query/ByProperty`, the following operators are available for the contents of the `Operation` field in `QueryConditions`:

| Operator             | Symbol | Description                                 | Example Condition                                   |
| :------------------- | :----- | :------------------------------------------ | :-------------------------------------------------- |
| **Equal**            | `==`   | Exact match.                                | `Key: "age", Value: "25", Operation: "=="`          |
| **Not Equal**        | `!=`   | Value is not equal.                         | `Key: "status", Value: "archived", Operation: "!="` |
| **Greater Than**     | `>`    | Value is greater than.                      | `Key: "price", Value: "100", Operation: ">"`        |
| **Greater Or Equal** | `>=`   | Value is greater or equal.                  | `Key: "score", Value: "50", Operation: ">="`        |
| **Less Than**        | `<`    | Value is less than.                         | `Key: "stock", Value: "10", Operation: "<"`         |
| **Less Or Equal**    | `<=`   | Value is less or equal.                     | `Key: "rank", Value: "5", Operation: "<="`          |
| **Like**             | `%`    | Partial string match (contains).            | `Key: "name", Value: "John", Operation: "%"`        |
| **In Array**         | `[==]` | Check if value exists in an array property. | `Key: "tags", Value: "urgent", Operation: "[==]"`   |

**Example of a specialized query:**
To find users where `age >= 18` AND `status == "active"`:

```json
{
  "CollectionName": "Users",
  "ConditionsBehavior": "&&",
  "QueryConditions": [
    { "Key": "age", "Value": "18", "Operation": ">=" },
    { "Key": "status", "Value": "active", "Operation": "==" }
  ]
}
```

### 6. Advanced Query Examples

**1. Filter by Array Content & Property**
Find registers where `roles` array contains an item with `id == 1` AND the register's `name` is `fer`.

```json
{
  "CollectionName": "coll",
  "ConditionsBehavior": "&&",
  "QueryConditions": [
    {
      "Key": "id",
      "Value": "1",
      "Operation": "[==]",
      "ArrayProperty": "roles"
    },
    {
      "Key": "name",
      "Value": "fer",
      "Operation": "=="
    }
  ]
}
```

**2. Range Query (Price Range)**
Find products with price greater than 50 AND less than or equal to 200.

```json
{
  "CollectionName": "Products",
  "ConditionsBehavior": "&&",
  "QueryConditions": [
    { "Key": "price", "Value": "50", "Operation": ">" },
    { "Key": "price", "Value": "200", "Operation": "<=" }
  ]
}
```

**3. Partial Text Search (OR condition)**
Find users where name contains "John" OR email contains "gmail".

```json
{
  "CollectionName": "Users",
  "ConditionsBehavior": "||",
  "QueryConditions": [
    { "Key": "name", "Value": "John", "Operation": "%" },
    { "Key": "email", "Value": "gmail", "Operation": "%" }
  ]
}
```
