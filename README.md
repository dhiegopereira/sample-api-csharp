### Estrutura do Projeto:

```
- Controllers
    - CustomerController.cs
    - LoginController.cs
- Data
    - ApplicationDbContext.cs
- DTOs
    - CustomerDTO.cs
    - LoginDTO.cs
- Interfaces
    - IEntityRepository.cs
- Middlewares
    - JwtMiddleware.cs
- Migrations
    - ...
- Modals
    - Customer.cs
- Properties
    - launchSettings.json
- Repositories
    - CustomerRepository.cs
    - LoginRepository.cs
- Services
    - CustomerService.cs
- appsettings.json
- Program.cs
```

### Explicação do Fluxo:

1. **Controllers:**
   - `CustomerController.cs`: Recebe as requisições HTTP relacionadas aos clientes e encaminha para o serviço de cliente.
   - `LoginController.cs`: Lida com as solicitações de login, autenticação e geração de tokens.

2. **Data:**
   - `ApplicationDbContext.cs`: Classe que representa o contexto do banco de dados e configura o mapeamento dos modelos de dados para as tabelas do banco utilizando o Entity Framework.

3. **DTOs (Data Transfer Objects):**
   - `CustomerDTO.cs`: Objeto de transferência de dados para clientes, usado para receber e enviar dados entre o cliente e o servidor.
   - `LoginDTO.cs`: DTO para dados relacionados à autenticação, utilizado para a entrada de dados de login.

4. **Interfaces:**
   - `IEntityRepository.cs`: Define um conjunto comum de operações (como CRUD) que todos os repositórios devem implementar. Ajuda na padronização das operações de acesso a dados.

5. **Middlewares:**
   - `JwtMiddleware.cs`: Middleware para autenticação baseada em token (JWT). Ele intercepta as solicitações HTTP, verifica a presença de um token JWT válido e permite ou nega o acesso às rotas protegidas com base na validade do token.

6. **Migrations:** (Não detalhado)
   - Diretório contendo as migrações do Entity Framework para versionamento do banco de dados. Usado para manter a consistência entre o modelo de banco de dados e o código.

7. **Modals:**
   - `Customer.cs`: Modelo de dados que representa um cliente no banco de dados. Corresponde à estrutura da tabela de clientes no banco de dados.

8. **Repositories:**
   - `CustomerRepository.cs`: Implementação concreta do repositório para operações relacionadas a clientes. Estende a classe abstrata `AbstractRepository` e implementa métodos específicos de acesso a dados para clientes.
   - `LoginRepository.cs`: Repositório específico para operações de autenticação. Implementa métodos para verificar credenciais de login e gerar tokens JWT.

9. **Services:**
   - `CustomerService.cs`: Lógica de negócios relacionada aos clientes para acessar serviços externos.

10. **Arquivos de Configuração:**
    - `appsettings.json`: Arquivo de configuração que armazena configurações específicas da aplicação, como conexões de banco de dados, chaves secretas, etc.
---
#### Fluxo de Operações:


1. **Requisição HTTP:**
   - Quando um cliente faz uma solicitação de cadastro através da rota `POST /api/customers`, o `CustomerController` recebe a requisição HTTP.
   - Todas as validações são feitas no model e no DTO

2. **Persistência de Dados:**
   - O `CustomerController` utiliza métodos do `CustomerRepository` para persistir os dados do cliente no banco de dados. Ele chama diretamente o método apropriado do repositório para inserir o novo cliente.

3. **Resposta ao Cliente:**
   - Após o cadastro ser concluído com sucesso, o `CustomerController` retorna uma resposta adequada ao cliente que fez a solicitação.
---
#### Operações de CRUD:

- **Atualização, Listagem, Busca e Exclusão de Clientes:**
  - Para essas operações, o `CustomerController` também utiliza diretamente os métodos correspondentes no `CustomerRepository`. Ele chama os métodos apropriados do repositório para realizar as operações de CRUD no banco de dados.
---
#### Consulta de Endereço Usando API Externa:

- **Consulta de Endereço:**
  - Se houver a necessidade de consultar um serviço externo, como a API do ViaCEP, para obter dados adicionais, isso é realizado pelo `CustomerService`. Após receber uma solicitação do `CustomerController`, o serviço utiliza métodos próprios para chamar a API externa, obter os dados necessários e processá-los conforme necessário.
---
#### Autenticação e Geração de Tokens:

- **Autenticação e Geração de Tokens:**
  - Quando um cliente faz uma solicitação de login através da rota `POST /api/login`, o `LoginController` recebe a requisição e utiliza métodos do `LoginRepository` para validar as credenciais do usuário. Se as credenciais forem válidas, o `LoginController` gera um token JWT usando métodos próprios ou bibliotecas especializadas e o retorna ao cliente.

