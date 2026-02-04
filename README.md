# .NET Backend Technical Challenge

Este repositório contém a implementação de um desafio técnico backend utilizando **.NET 9**, abordando lógica de programação, consumo de dados, arquitetura em camadas e construção de uma API REST.

## Objetivo

Demonstrar conhecimentos em C#/.NET, lógica de programação, arquitetura backend, acesso a dados, padrões de projeto e construção de APIs REST, incluindo testes automatizados.

O projeto está dividido em múltiplas questões, cada uma representando um tipo diferente de problema.

---

## Tecnologias Utilizadas

- .NET 9
- ASP.NET Core Web API
- Dapper
- SQLite
- MediatR
- Swagger (Swashbuckle)
- xUnit
- Moq / NSubstitute
- FluentAssertions
- Coverlet (Code Coverage)

---

## Como executar o projeto

### Pré-requisitos

- .NET SDK 9 instalado (<https://dotnet.microsoft.com>)

---

### Abrindo a solution

```bash
dotnet build Exercicio.sln
```

Ou abra diretamente pela IDE:

```bash
Exercicio.sln
```

- Questão 1

```bash
cd Questao1
dotnet run
```

- Questão 2

```bash
cd Questao2
dotnet run
```

- Questões 3 e 4

```md
Contêm respostas teóricas e SQL:
```

Questao3 - Documento com respostas conceituais

Questao4 - Script SQL

- Questão 5

Projeto Web API seguindo arquitetura em camadas:

```bash
Domain
Application
Infrastructure
```

Inclui:

- Commands / Queries (CQRS)

- MediatR

- Persistência com SQLite via Dapper

- Swagger para documentação

- Testes unitários

Executando a API

```bash
cd Questao5
dotnet run
```

> A API ficará disponível em <https://localhost:5001/swagger> ou <http://localhost:5000/swagger>

## Executando os testes

Dentro da pasta Questao5:

```bash
dotnet test
```

Com cobertura:

```bash
dotnet test /p:CollectCoverage=true
```

## Arquitetura (Questão 5)

- Domain -> Entidades e regras de negócio

- Application -> Commands, Queries, Handlers

- Infrastructure -> Acesso a dados (Dapper + SQLite)

> Separação clara de responsabilidades e uso de CQRS.

## Decisões técnicas

- Uso de CQRS com MediatR para separar leitura e escrita
- Dapper escolhido por performance e simplicidade
- SQLite para facilitar execução local
- Arquitetura em camadas visando manutenibilidade
- Testes unitários para validação da lógica de negócio

## Observações

- Projeto desenvolvido com foco em boas práticas

- Separação de camadas

- Testes automatizados

- Código orientado a manutenção e legibilidade

- Swagger disponível para exploração da API
