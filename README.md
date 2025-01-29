# Projeto .NET 8 Web API - Sales Crud

Este é um projeto que segue o template de desenvolvimento arquitetural do cliente AMBEV, e contempla um crud básico da entidade Sale.
Segue a estrutura de uma Web API desenvolvida em .NET 8 o qual está organizada de forma clara para facilitar o desenvolvimento, testes e implantação.
O desenvolvimento seguiu o padrão Mediator, conforme especificados nas instruções

## 📂 Estrutura do Projeto

A estrutura do projeto é a seguinte:

```
.
├── bin/                  # Diretório gerado para binários (não versionado)
├── obj/                  # Diretório gerado para objetos de compilação (não versionado)
├── postgres-data/        # Dados do PostgreSQL (usado para desenvolvimento local em ambiente Docker)
├── src/                  # Código-fonte da aplicação
├── tests/                # Testes unitários e de integração
├── Ambev.DeveloperEvaluation.sln         # Arquivo de solução do Visual Studio
├── docker-compose.yml    # Configuração do Docker Compose para rodar a aplicação e o PostgreSQL
└── launchSettings.json   # Configurações de execução do projeto (perfis de depuração)
```

---

## 🚀 Como Executar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/) (opcional, para rodar o PostgreSQL em um contêiner)
- [PostgreSQL](https://www.postgresql.org/) (opcional, se não usar Docker)

---

### 1. Executando Localmente (sem Docker)

1. **Restaurar dependências**:
   Navegue até o diretório `src/Ambev.DeveloperEvaluation.WebApi` e execute:

   ```bash
   dotnet restore
   ```

2. **Executar a aplicação**:
   Ainda no diretório `src/Ambev.DeveloperEvaluation.WebApi`, execute:

   ```bash
   dotnet run
   ```

   A API estará disponível em `http://localhost:5000` ou `https://localhost:5001`.

3. **Será necessário atualizar o appsettings conforme suas configurações locais**

---

### 2. Executando com Docker Compose

1. **Subir os contêineres**:
   Na raiz do projeto, execute:

   ```bash
   docker-compose up --build
   ```

   Isso irá:
   - Construir a imagem da aplicação.
   - Subir um container com o PostgreSQL.
   - Subir um container com o PgAdmin
   - Subir um container com a aplicação Web API.
   - Subir um container de configuras de cache.
   - Subir um container de banco nao relacional.

2. **Acessar a API**:
   A API estará disponível em `http://localhost:5000` ou `https://localhost:5001`.

3. **Parar os contêineres**:
   Para parar os contêineres, execute:

   ```bash
   docker-compose down
   ```

---

### 3. Executando Testes

1. **Navegue até o diretório de testes**:
   ```bash
   cd tests/MyAmbev.DeveloperEvaluation.Functional
   cd tests/MyAmbev.DeveloperEvaluation.Integration
   cd tests/MyAmbev.DeveloperEvaluation.Unit
   ```

2. **Executar os testes**:
   ```bash
   dotnet test
   ```

---

## 🛠 Modelos de Ação

- Para executar as ações de create e update, o seguinte modelo pode ser utilizado:
- Para ações de create, os valores dos atributos Id, saleId serão atualizados e inseridos o valor correto no banco.

```json
{
  "Id": "50f8ca77-ecfa-42f0-b1aa-a69bc933d217",
  "saleNumber": "1",
  "saleDate": "2025-01-25T13:40:07.919Z",
  "customer": "Roberth Silva",
  "totalAmount": 100,
  "branch": "mercadoria 4",
  "items": [
    {
      "id": 3,
      "saleId": "50f8ca77-ecfa-42f0-b1aa-a69bc933d217",
      "sale": {
        "saleNumber": "1",
        "saleDate": "2025-01-25T13:40:07.919Z",
        "customer": "Roberth Silva",
        "totalAmount": 100,
        "branch": "mercadoria",
        "isCancelled": false,
        "createdAt": "2025-01-25T13:40:07.919Z",
        "updatedAt": "2025-01-25T13:40:07.919Z"
      },
      "product": "calca jeans",
      "quantity": 12,
      "unitPrice": 100,
      "discount": 0,
      "totalAmount": 100,
      "createdAt": "2025-01-25T13:40:07.919Z",
      "updatedAt": "2025-01-25T13:40:07.919Z"    
      }
  ],
  "isCancelled": false
}
```


## 🛠 Configurações

### Banco de Dados (PostgreSQL)

- O banco de dados PostgreSQL é configurado para rodar em um contêiner Docker.
- Os dados são persistidos no diretório `postgres-data/`.
- A string de conexão está configurada no arquivo `appsettings.json`:
- Os dados de conexao com o banco, devem ser preenchidos conforme especificados no arquivo doccker-compose

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=mydatabase;Username=myuser;Password=mypassword"
   }
   ```

---

### Docker Compose

O arquivo `docker-compose.yml` contém a configuração para rodar a aplicação e o PostgreSQL:

```yaml
version: '3.8'

services:
  db:
    image: postgres:latest
    environment:
      POSTGRES_USER: myuser
      POSTGRES_PASSWORD: mypassword
      POSTGRES_DB: mydatabase
    volumes:
      - ./postgres-data:/var/lib/postgresql/data
    ports:
      - "5432:5432"

  webapi:
    build:
      context: .
      dockerfile: src/MyProject.WebApi/Dockerfile
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Host=db;Port=5432;Database=mydatabase;Username=myuser;Password=mypassword
    ports:
      - "5000:80"
      - "5001:443"
    depends_on:
      - db
```

---


## 🧪 Testes

O projeto inclui testes unitários e de integração no diretório `tests/`. Para executar os testes:

1. Navegue até o diretório de testes:

   ```bash
      cd tests/MyAmbev.DeveloperEvaluation.Functional
      cd tests/MyAmbev.DeveloperEvaluation.Integration
      cd tests/MyAmbev.DeveloperEvaluation.Unit
   ```

2. Execute os testes:

   ```bash
   dotnet test
   ```

---

## 🐛 Depuração

### Visual Studio

1. Abra o arquivo `Ambev.DeveloperEvaluation.sln` no Visual Studio.
2. Selecione o perfil de execução desejado no menu de depuração (recomenda-se usar o perfil docker-compose).
3. Pressione `F5` para iniciar a depuração.

### Visual Studio Code

1. Abra a pasta raiz do projeto no VS Code.
2. Use o arquivo `.vscode/launch.json` para configurar a depuração.
3. Pressione `F5` para iniciar a depuração.


---

## 📧 Contato

Para dúvidas ou sugestões, entre em contato:

- **Dev**: Roberth Silva
- **Email**: [roberth410@gmail.com]
- **GitHub**: [roberth-silva](https://github.com/roberth-silva)

---