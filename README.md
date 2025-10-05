# Microserviço de Pedidos e Pagamentos

Este microserviço faz parte do sistema de e-commerce da TeraBum, responsável por criar pedidos a partir do carrinho do usuário, registrar o status do pagamento e atualizar o histórico de pedidos. A aplicação foi desenvolvida em .NET 9 com Entity Framework Core e banco de dados PostgreSQL hospedado no Supabase.


## Objetivos da API

- Receber requisições para criação de pedidos a partir do carrinho do usuário.
- Registrar itens, quantidade, preço unitário e calcular total do pedido.
- Atualizar status de pagamento do pedido (mock/simulação).
- Permitir consulta de pedidos por usuário.
- Ser independente e escalável, podendo ser integrado com outros microserviços.

## Implantação

Como está rodando local, podem testar:

## Implantação Local

1. Clone este repositório.
2. Abra o terminal na pasta `OrderService`.
3. Configure a connection string do Supabase no `appsettings.json`:

Linha que precisa ser ajustada: "DefaultConnection": "Host=seu_host;Port=5432;Database=postgres;User Id=usuario;Password=senha;SSL Mode=Require;"


4. Instale os pacotes do .NET:
dotnet restore

5. Atualize o banco com as migrations:
dotnet ef database update

6. Rode a aplicação:
dotnet run

7. Acesse o Swagger em `http://localhost:5050/swagger` para testar os endpoints.

--------------------------------------------------------------------------------------------------------------------------------------------------

## Modelagem da Aplicação

Entidades principais:

- **Order**: representa um pedido do usuário
  - UserId: Guid
  - TotalAmount: decimal
  - Status: string
  - Items: lista de OrderItem

- **OrderItem**: representa um item dentro do pedido
  - ProductId: Guid
  - Quantity: int
  - UnitPrice: decimal

Diagrama de classes:

<img width="311" height="347" alt="image" src="https://github.com/user-attachments/assets/d376d826-048f-46e7-a0a9-aa934505f72d" />

## Tecnologias Utilizadas

- .NET 9
- C#
- Entity Framework Core
- PostgreSQL (Supabase)
- Docker (para containerizar a aplicação)
- Swagger (documentação de API)
- Git/GitHub (controle de versão)

## API Endpoints

### Criar Pedido
- Método: POST
- URL: /orders
- Parâmetros:
  - UserId (Guid)
  - Items (lista de objetos com ProductId, Quantity e UnitPrice)
- Resposta:
  - 200 OK → retorna o pedido criado
  - 400 Bad Request → se Items estiver vazio

### Consultar Pedidos de um Usuário
- Método: GET
- URL: /orders/{userId}
- Parâmetros:
  - userId: Guid
- Resposta:
  - 200 OK → lista de pedidos do usuário

### Atualizar Status do Pedido
- Método: PATCH
- URL: /orders/{orderId}
- Parâmetros:
  - status: string
- Resposta:
  - 200 OK → pedido atualizado
  - 404 Not Found → pedido não encontrado
