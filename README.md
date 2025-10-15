# Sistema de Recomendação de Investimentos com Inteligência Artificial

## Integrantes
- **Gustavo Kenzo** – 98481  
- **Vinícius Almeida Bernardino de Souza** – 97888  
- **Márcio Hitoshi Tahyra** – 552511  

---

## Descrição
Esta solução é uma **API RESTful** desenvolvida em **.NET 8** e **C# 12**, que gerencia clientes e ativos financeiros.  
O grande diferencial é o **módulo de Recomendação com IA**, que fornece **sugestões de investimento** e uma **justificativa analítica** gerada pela **Google Gemini API**.  

O sistema utiliza **Entity Framework Core** para persistência em **banco Oracle**.

---

## Configuração e Execução

### 1. Pré-requisitos
- .NET 8 SDK
- Acesso ao **Oracle Database**
- Chave válida da **Google Gemini API**

---

### 2. Configuração de Credenciais (Importante)
Preencha o arquivo `appsettings.json` com os seguintes valores:

```json
{
  "ConnectionStrings": {
    "OracleConnection": "User Id=XXX;Password=XXX;Data Source=oracle.fiap.com.br:1521/ORCL"
  },
  "GeminiApi": {
    "ApiKey": "[SUA_CHAVE_API_GEMINI]"
  }
}
````

#### 2.2. Ambiente Cloud (Azure/AWS)

https://sprint4cs-ggdqhdhrhzdja7fv.brazilsouth-01.azurewebsites.net/

As seguintes **variáveis de ambiente** e **connection strings** formam definidas:

| Serviço    | Nome da Variável Cloud                    | Valor                          |
| :--------- | :---------------------------------------- | :----------------------------- |
| Oracle DB  | `OracleConnection` (Connection String)    | Sstring de conexão             |
| Gemini API | `GeminiApi__ApiKey` (Application Setting) | Chave da Google Gemini API     |

---

### 3. Como Executar

```bash
dotnet restore
dotnet build
dotnet run
```

---

### 4. Acessar Swagger

A documentação e os testes podem ser acessados via Swagger:

[https://localhost:7146/swagger](https://localhost:7146/swagger)

---

## Funcionalidades

* **Clientes (CRUD):** Cadastro, consulta, atualização e remoção
* **Ativos (CRUD):** Gerenciamento e importação via arquivo JSON
* **Recomendações com IA:** Sugestões de investimentos com **justificativa gerada pelo Gemini API**

---

## Principais Endpoints (Destaque para a IA)

|  Método  | Endpoint                         | Descrição                                                                                |
| :------: | :------------------------------- | :--------------------------------------------------------------------------------------- |
|  **GET** | `/api/recomendacao/cliente/{id}` | **Endpoint principal:** retorna a carteira recomendada e a justificativa analítica da IA |
|  **GET** | `/api/clientes`                  | Lista todos os clientes                                                                  |
| **POST** | `/api/clientes`                  | Cria um novo cliente                                                                     |
|  **GET** | `/api/ativos`                    | Lista todos os ativos                                                                    |
| **POST** | `/api/ativos/carregar-json`      | Importa ativos de um arquivo JSON                                                        |
| **GET**  | `/api/ativos/pesquisa`           | Retorna ativos filtrados por Tipo e Risco (usando LINQ com filtros dinâmicos).           |
| **GET**  | `/api/cliente/por-perfil`        | Retorna clientes filtrados por Perfil do Investidor (usando LINQ).                       |

---

## Arquitetura do Sistema
<img width="4074" height="2958" alt="image" src="https://github.com/user-attachments/assets/a3906d44-a7fb-43d6-ab4d-4a2ff80ba3a2" />

---

## Documentação

A API possui **documentação automática via Swagger**, incluindo **comentários XML** dos controllers e modelos para facilitar a utilização.

---

## Tecnologias Utilizadas

* .NET 8
* C# 12
* Entity Framework Core
* Oracle Database
* Google Gemini API (Integração de IA)
* Swagger / OpenAPI
