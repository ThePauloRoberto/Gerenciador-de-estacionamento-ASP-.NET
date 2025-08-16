# Minimal API - Gerenciador de Estacionamento

Projeto desenvolvido em ASP.NET Core utilizando Minimal API para gerenciar um estacionamento, com foco em simplicidade, performance e boas práticas de arquitetura.

## Funcionalidades

- Cadastro, edição e remoção de veículos
- Controle de entrada e saída de veículos
- Cadastro e autenticação de administradores
- Autenticação JWT
- Documentação automática com Swagger

## Estrutura do Projeto
```
minimal-api/
│
├── dominio/
│   ├── entidades/         # Entidades de domínio (ex: Administrador, Veiculo)
│   └── modelViews/        # Modelos de visualização e structs auxiliares
│
├── minimal-api.csproj     # Arquivo de configuração do projeto principal
├── Program.cs             # Ponto de entrada da aplicação
│
└── Teste/                 # Projeto de testes unitários
    └── Domain/
        └── Entidades/     # Testes das entidades de domínio
```


## Como Executar

1. **Restaurar dependências**
   ```sh
   dotnet restore
   ```

2. **Build do projeto**
   ```sh
   dotnet build
   ```

3. **Executar a aplicação**
   ```sh
   dotnet run --project minimal-api
   ```

4. **Acessar o Swagger**
   - Acesse `http://localhost:5042/swagger` no navegador para visualizar e testar a

## Executando os Testes

```sh
dotnet test
```

## Tecnologias Utilizadas

- ASP.NET Core Minimal API
- Entity Framework Core
- Swagger (Swashbuckle)
- JWT