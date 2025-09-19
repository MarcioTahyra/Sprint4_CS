# Sistema de Recomendação de Investimentos

## Descrição
Esta solução é uma **API RESTful** desenvolvida em **.NET 8** e **C# 12**, que gerencia clientes, ativos financeiros e carteiras de investimentos, além de fornecer **recomendações personalizadas**.  
O sistema utiliza **Entity Framework Core** para persistência em banco **Oracle**.

---

## Funcionalidades
- **Clientes:** Cadastro, consulta, atualização e remoção de clientes.  
- **Ativos:** Cadastro, consulta, atualização, remoção e importação de ativos via arquivo JSON.  
- **Carteiras:** Associação de ativos a clientes e controle de quantidade.  
- **Recomendações:** Geração de recomendações de investimentos com base no perfil e objetivo do cliente.

---

## Estrutura do Projeto
- **Models:** Entidades de domínio (`Cliente`, `Ativo`, `Carteira`, `CarteiraDeInvestimentos`, DTOs).  
- **Controllers:** Endpoints para operações CRUD e importação de dados.  
- **Services:** Lógica de negócio, como importação de ativos e cálculos de recomendação.  
- **Database:** Contexto do Entity Framework e repositórios.

---

## Como Executar
1. **Pré-requisitos:**
   - .NET 8 SDK 
   - Banco **Oracle** disponível (ajuste a string de conexão em `AppDbContext`)  

2. **Executar a aplicação:**
   ```bash
   dotnet restore
   dotnet build
   dotnet run


3. **Acessar Swagger para documentação e testes:**

   ```
   https://localhost:5001/swagger
   ```

---

## Principais Endpoints

| Método | Endpoint                          | Descrição                          |
| ------ | --------------------------------- | ---------------------------------- |
| GET    | /api/clientes                     | Lista todos os clientes            |
| POST   | /api/clientes                     | Cria um novo cliente               |
| GET    | /api/ativos                       | Lista todos os ativos              |
| POST   | /api/ativos/carregar-json         | Importa ativos de um arquivo JSON  |
| POST   | /api/recomendacao/carregar-ativos | Gera recomendação de investimentos |

---

## Documentação

A API possui **documentação automática via Swagger**, incluindo comentários XML dos controllers e modelos.

---

## Tecnologias

* **.NET 8**
* **C# 12**
* **Entity Framework Core**
* **Oracle Database**
* **Swagger / OpenAPI**
