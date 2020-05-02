# Projeto Luby Tasks
Projeto de gerenciamento simples de tarefas com autenticação de usuários.

## Arquitetura
Baseada na arquiterura [CQRS](https://docs.microsoft.com/pt-br/azure/architecture/patterns/cqrs), somente tendo implementado `Commands` e `Queries`, deixando de lado emissão de eventos e etc.

## Autenticação
Autenticação com token [JWT](https://jwt.io/introduction/).

O token pode ser gerado na URL `https://localhost:44399/auth/` passando o seguinte objeto JSON:
```
{
	"login": "sapequinha",
	"password": "123"
}
```

## Response da requisição HTTP
Todos os endpoints retornam um mesmo tipo de objeto, sendo:
```
{
  "data": [
    {
      "id": "int",
      "title": "string",
      "description": "string",
      "concluded": true,
      "user": {
        "id": "int",
        "name": "string"
      },
      "userName": "string",
      "createDate": "datetimeoffset",
      "lastModified": "datetimeoffset",
      "removed": "bool"
    }
  ],
  "statusCode": "int",
  "errorMessage": "string",
  "totalRows": "int"
}
```
`data`: Os dados que foram solicitados.

`statusCode`: Status da requisição HTTP.

`errorMessage`: Mensagem de erro contendo o motivo pelo qual não foi possível realizar a operação.

`totalRows`: Quantidade de items dentro da propriedade `data`.

## Documentação da API
Toda a documentação da API, mapeamento de `endpoints` e `schemas` esta disponível por meio da ferramente [Swagger](https://swagger.io/) que pode ser acessada pela URL https://localhost:44399/.

### Configuração de ambiente

#### 1. Bando de dados
O banco de dados na aplicação é o [SQL Server](https://www.microsoft.com/pt-br/sql-server).

Para gerar o banco assim como as tabelas, basta rolar o script que esta em `db/luby_tasks.sql`.
```
create database luby_tasks  collate Latin1_General_CS_AS;
go

use luby_tasks;
go

create table dbo.users
(
	id int identity(1,1) primary key not null,
	[name] nvarchar(150) collate Latin1_General_CS_AS not null,
	[login] nvarchar(50) collate Latin1_General_CS_AS not null,
	[password] nvarchar(512) collate Latin1_General_CS_AS not null,
	create_date datetimeoffset not null default getdate(),
	last_modified datetimeoffset not null default getdate(),
	removed bit not null default 0
);
go
create unique index U_Login on dbo.users (login) where removed = 0
go

create table tasks
(
	id int identity(1,1) primary key not null,
	[title] nvarchar(150) collate Latin1_General_CS_AS not null,
	[description] nvarchar(4000) collate Latin1_General_CS_AS,
	concluded bit not null default 1,
	[id_user] int not null,
	create_date datetimeoffset not null default getdate(),
	last_modified datetimeoffset not null default getdate(),
	removed bit not null default 0
	constraint FK_users_1 foreign key ([id_user]) references dbo.users
);
go
```
#### 2. Configuração da API
Para ter acesso ao banco de dados, a `connectionSctring` dentro do arquivo `appsettings.Development.json` deverá ser alterada conforme o `User` e `Password` usados para acessar o banco.
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost; Initial Catalog=luby_tasks; Integrated Security=false; User Id=sapequinha; Password=123;",
    ...
  }
}
```












