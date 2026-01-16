> [Read in English](README.md)

![Sambi_DB Logo](admin/public/Icon.png)

# Sambi_DB

Sambi_DB é um mecanismo de banco de dados personalizado construído com .NET 8, apresentando um painel administrativo moderno desenvolvido em React. Ele fornece um backend robusto para gerenciar coleções e documentos, completo com uma interface de usuário limpa e interativa.

## 🚀 Funcionalidades

- **Mecanismo de Banco de Dados Personalizado**: Construído do zero usando C# e .NET 8.
- **Painel Administrativo**: Uma interface web moderna e responsiva para gerenciar seu banco de dados.
- **Gerenciamento de Coleções**: Crie, visualize e gerencie coleções facilmente.
- **Operações de Documentos**: Mecanismos eficientes de armazenamento e recuperação.
- **Documentação da API**: Swagger UI integrado para testar e explorar endpoints da API.
- **Suporte a Serviço Windows**: Capaz de rodar como um Serviço Windows autônomo.

## 🛠 Tech Stack

### Backend

- **Framework**: .NET 8 (ASP.NET Core)
- **Linguagem**: C#
- **Documentação**: Swagger / OpenAPI
- **Containerização**: Suporte a Docker

### Frontend (Painel Admin)

- **Framework**: React
- **Ferramenta de Build**: Vite
- **Linguagem**: TypeScript
- **Biblioteca de UI**: Material UI (MUI)
- **Gerenciamento de Estado**: React Query (@tanstack/react-query)
- **Roteamento**: React Router

## 📂 Estrutura do Projeto

- `Index/`: Lógica central do banco de dados, operações e tratamento de exceções.
- `Presenters/`: Controladores de API lidando com requisições HTTP.
- `admin/`: Código fonte do frontend (aplicação React).
- `db.csproj`: Configuração do projeto e alvos de build.

## 🏁 Começando

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (para construir o frontend)

### Instalação e Execução

1.  **Clone o repositório:**

    ```bash
    git clone https://github.com/Paulo-Fernando-R/Sambi_DB.git
    cd Sambi_DB
    ```

2.  **Compile e Execute (IDE / CLI):**
    O projeto está configurado para compilar automaticamente o frontend quando você compila o backend.

    ```bash
    dotnet build
    dotnet run
    ```

    _Isso instalará as dependências do frontend, compilará o app React, copiará os artefatos para `wwwroot` e iniciará o servidor .NET._

3.  **Acesse a Aplicação:**
    - **Painel Administrativo**: `http://localhost:<PORT>/` (geralmente configurado em `appsettings.json` ou `Program.cs`) (porta padrão: 5000)
    - **Swagger API**: `http://localhost:<PORT>/swagger`

### Modo de Desenvolvimento

Se você quiser trabalhar no frontend com hot-reload ativado:

1.  Navegue para o diretório `admin`:

    ```bash
    cd admin
    ```

2.  Inicie o servidor de desenvolvimento:

    ```bash
    npm install
    npm run dev
    ```

3.  Execute o backend separadamente para servir a API.

## 🐳 Suporte a Docker

O projeto inclui um `Dockerfile` para containerização. Você pode construir a imagem docker usando:

```bash
docker build -t sambi_db .
```

## 📚 Documentação da API

Sambi_DB expõe uma API REST para gerenciar bancos de dados, coleções e documentos (registros). Abaixo está a lista de endpoints disponíveis e seu uso.

### 1. Operações de Banco de Dados

| Método   | Endpoint                          | Descrição                                  | Parâmetros de Query/Body                 |
| :------- | :-------------------------------- | :----------------------------------------- | :--------------------------------------- |
| `POST`   | `/Database/Create`                | Cria um novo banco de dados.               | **Body:** `{ "DatabaseName": "string" }` |
| `DELETE` | `/Database/Delete/{DatabaseName}` | Deleta um banco de dados.                  | **Body:** `{ "Confirm": true }`          |
| `GET`    | `/Database/List`                  | Lista todos os bancos de dados existentes. | -                                        |

**Exemplos:**

_Criar Banco de Dados_

```json
{ "DatabaseName": "MeuBancoDeDados" }
```

_Deletar Banco de Dados_

```json
{ "Confirm": true }
```

### 2. Operações de Coleção

| Método   | Endpoint                            | Descrição                       | Parâmetros de Query/Body                                                                           |
| :------- | :---------------------------------- | :------------------------------ | :------------------------------------------------------------------------------------------------- |
| `POST`   | `/Collection/Create/{DatabaseName}` | Cria uma nova coleção em um BD. | **Body:** `{ "CollectionName": "string" }`                                                         |
| `PUT`    | `/Collection/Update/{DatabaseName}` | Renomeia uma coleção.           | **Body:** `{ "CollectionName": "nome_antigo", "NewCollectionName": "novo_nome", "Confirm": true }` |
| `DELETE` | `/Collection/Delete/{DatabaseName}` | Deleta uma coleção.             | **Body:** `{ "CollectionName": "string", "Confirm": true }`                                        |
| `GET`    | `/Collection/List/{DatabaseName}`   | Lista coleções em um BD.        | -                                                                                                  |

**Exemplos:**

_Criar Coleção_

```json
{ "CollectionName": "Usuarios" }
```

_Renomear Coleção_

```json
{
  "CollectionName": "Usuarios",
  "NewCollectionName": "Clientes",
  "Confirm": true
}
```

_Deletar Coleção_

```json
{ "CollectionName": "Usuarios", "Confirm": true }
```

### 3. Operações de Registro (Documento)

| Método   | Endpoint                          | Descrição                            | Parâmetros de Query/Body                                                            |
| :------- | :-------------------------------- | :----------------------------------- | :---------------------------------------------------------------------------------- |
| `POST`   | `/Register/Create/{DatabaseName}` | Adiciona um documento a uma coleção. | **Body:** `{ "CollectionName": "string", "Data": { ... } }`                         |
| `PUT`    | `/Register/Update/{DatabaseName}` | Atualiza um documento.               | **Body:** `{ "CollectionName": "string", "RegisterId": "string", "Data": { ... } }` |
| `DELETE` | `/Register/Delete/{DatabaseName}` | Deleta um documento.                 | **Body:** `{ "CollectionName": "string", "RegisterId": "string", "Confirm": true }` |

**Exemplos:**

_Adicionar Registro (Documento)_

```json
{
  "CollectionName": "Usuarios",
  "Data": {
    "name": "Jane Doe",
    "email": "jane@example.com"
  }
}
```

_Atualizar Registro_

```json
{
  "CollectionName": "Usuarios",
  "RegisterId": "bbf2a446-bbe1-4c8d-b95a-2969923ea1ab",
  "Data": {
    "name": "Jane Does",
    "email": "jane.does@example.com"
  }
}
```

_Deletar Registro_

```json
{
  "CollectionName": "Usuarios",
  "RegisterId": "bbf2a446-bbe1-4c8d-b95a-2969923ea1ab",
  "Confirm": true
}
```

#### Operações de Array

Gerencie arrays dentro de documentos (ex: adicionando tags ou itens a uma lista dentro de um documento).

| Método   | Endpoint                                | Descrição                     | Parâmetros de Query/Body                                                                                                                                                 |
| :------- | :-------------------------------------- | :---------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `POST`   | `/Register/Add/Array/{DatabaseName}`    | Adiciona um item a um array.  | **Body:** `{ "RegisterId": "string", "CollectionName": "string", "ArrayName": "string", "Data": { ... } }`                                                               |
| `PUT`    | `/Register/Update/Array/{DatabaseName}` | Atualiza um item em um array. | **Body:** `{ "RegisterId": "string", "CollectionName": "string", "ArrayName": "string", "Property": "string", "Value": "string", "NewValue": { ... }, "Confirm": true }` |
| `DELETE` | `/Register/Delete/Array/{DatabaseName}` | Deleta um item de um array.   | **Body:** `{ "RegisterId": "string", "CollectionName": "string", "ArrayName": "string", "Property": "string", "Value": "string", "Confirm": true }`                      |

**1. Adicionar ao Array**

```json
{
  "RegisterId": "bbf2a446-bbe1-4c8d-b95a-2969923ea1ab",
  "CollectionName": "Usuarios",
  "ArrayName": "Tags",
  "Data": {
    "id": "2",
    "description": "desenvolvedor"
  }
}
```

**2. Atualizar Item do Array**

```json
{
  "RegisterId": "bbf2a446-bbe1-4c8d-b95a-2969923ea1ab",
  "CollectionName": "Usuarios",
  "ArrayName": "Tags",
  "Property": "id",
  "Value": "2",
  "NewValue": {
    "id": "2",
    "description": "desenvolvedor-lider"
  },
  "Confirm": true
}
```

**3. Deletar do Array**

```json
{
  "RegisterId": "bbf2a446-bbe1-4c8d-b95a-2969923ea1ab",
  "CollectionName": "Usuarios",
  "ArrayName": "Tags",
  "Property": "id",
  "Value": "2",
  "Confirm": true
}
```

### 4. Operações de Consulta (Query)

#### Buscar por ID

`POST /Query/ById/{DatabaseName}`

- **Body**:

```json
{
  "CollectionName": "string",
  "Id": "string"
}
```

#### Buscar por Propriedades

`POST /Query/ByProperty/{DatabaseName}`
Permite filtrar documentos com base em condições específicas.

**Estrutura do Body:**

```json
{
  "CollectionName": "string",
  "Limit": 20,
  "Skip": 0,
  "ConditionsBehavior": "&&", // "&&" (E) or "||" (OU)
  "QueryConditions": [
    {
      "Key": "propertyName",
      "Value": "valueToSearch",
      "Operation": "==",
      "ArrayProperty": "" // Opcional, para propriedades de array aninhadas
    }
  ]
}
```

### 5. Operadores de Consulta

Ao usar `Query/ByProperty`, os seguintes operadores estão disponíveis para o conteúdo do campo `Operation` em `QueryConditions`:

| Operador           | Símbolo | Descrição                                         | Condição de Exemplo                                  |
| :----------------- | :------ | :------------------------------------------------ | :--------------------------------------------------- |
| **Igual**          | `==`    | Correspondência exata.                            | `Key: "idade", Value: "25", Operation: "=="`         |
| **Diferente**      | `!=`    | Valor não é igual.                                | `Key: "status", Value: "arquivado", Operation: "!="` |
| **Maior Que**      | `>`     | Valor é maior que.                                | `Key: "preco", Value: "100", Operation: ">"`         |
| **Maior ou Igual** | `>=`    | Valor é maior ou igual.                           | `Key: "pontuacao", Value: "50", Operation: ">="`     |
| **Menor Que**      | `<`     | Valor é menor que.                                | `Key: "estoque", Value: "10", Operation: "<"`        |
| **Menor ou Igual** | `<=`    | Valor é menor ou igual.                           | `Key: "rank", Value: "5", Operation: "<="`           |
| **Like**           | `%`     | Correspondência parcial de string (contém).       | `Key: "nome", Value: "John", Operation: "%"`         |
| **No Array**       | `[==]`  | Verifica se valor existe em propriedade de array. | `Key: "tags", Value: "urgente", Operation: "[==]"`   |

**Exemplo de uma consulta especializada:**
Para encontrar usuários onde `idade >= 18` E `status == "ativo"`:

```json
{
  "CollectionName": "Usuarios",
  "ConditionsBehavior": "&&",
  "QueryConditions": [
    { "Key": "idade", "Value": "18", "Operation": ">=" },
    { "Key": "status", "Value": "ativo", "Operation": "==" }
  ]
}
```

### 6. Exemplos Avançados de Consulta

**1. Filtrar por Conteúdo de Array & Propriedade**
Encontrar registros onde o array `roles` contém um item com `id == 1` E o `nome` do registro é `fer`.

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

**2. Consulta por Faixa (Faixa de Preço)**
Encontrar produtos com preço maior que 50 E menor ou igual a 200.

```json
{
  "CollectionName": "Produtos",
  "ConditionsBehavior": "&&",
  "QueryConditions": [
    { "Key": "preco", "Value": "50", "Operation": ">" },
    { "Key": "preco", "Value": "200", "Operation": "<=" }
  ]
}
```

**3. Busca de Texto Parcial (Condição OU)**
Encontrar usuários onde nome contém "John" OU email contém "gmail".

```json
{
  "CollectionName": "Usuarios",
  "ConditionsBehavior": "||",
  "QueryConditions": [
    { "Key": "nome", "Value": "John", "Operation": "%" },
    { "Key": "email", "Value": "gmail", "Operation": "%" }
  ]
}
```
