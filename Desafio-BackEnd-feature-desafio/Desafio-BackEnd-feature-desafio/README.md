# RentChallenge

Sistema para gerenciamento de aluguel de motos e entregadores, seguindo Clean Architecture, .NET 8, integração com PostgreSQL, RabbitMQ e MinIO.

## Sumário

- [Descrição](#descrição)
- [Arquitetura](#arquitetura)
- [Tecnologias](#tecnologias)
- [Como rodar](#como-rodar)
- [Testes](#testes)
- [APIs e Casos de Uso](#apis-e-casos-de-uso)
- [Ambiente Docker](#ambiente-docker)
- [Contato](#contato)

---

## Descrição

O RentChallenge é uma aplicação backend para gerenciar motos, entregadores e locações, com eventos de mensageria e armazenamento de imagens de CNH em MinIO.

---

## Arquitetura

- **Domain:** Entidades, interfaces e regras de negócio.
- **Application:** Serviços de aplicação, validações, DTOs, mapeamentos e regras de orquestração.
- **Infrastructure:** Implementações de repositórios, serviços de mensageria (RabbitMQ), storage (MinIO) e contexto de dados (PostgreSQL).
- **API:** Controllers REST, middlewares e configuração de dependências.
- **Test:** Projetos de testes de unidade e integração usando xUnit.

---

## Tecnologias

- .NET 8
- PostgreSQL
- RabbitMQ
- MinIO
- xUnit (testes)
- Docker/Docker Compose

---

## Como rodar

1. **Pré-requisitos:**  
   - Docker e Docker Compose instalados

2. **Configuração:**  
   - Edite o arquivo `.env` com as variáveis de ambiente necessárias (veja exemplos em `appsettings.Development.json`).

3. **Suba os serviços:**  
   - docker-compose --env-file .env up --build

4. **Acesse a API:**  
   - [http://localhost:5000/swagger](http://localhost:5000/swagger) (Swagger UI)

---

## Testes

- Testes de unidade e integração estão na pasta `test/`.
- Para rodar:
  - dotnet test

---

## APIs e Casos de Uso

- Cadastro, consulta, alteração e remoção de motos.
- Cadastro e atualização de entregadores (incluindo upload de CNH).
- Locações de motos.
- Eventos de moto cadastrada via RabbitMQ.
- Armazenamento de imagens de CNH no MinIO.

Consulte o Swagger da aplicação para detalhes dos endpoints e contratos.

---

## Ambiente Docker

- **rentchallenge-api:** API principal (.NET 8)
- **postgres:** Banco de dados relacional
- **rabbitmq:** Mensageria
- **minio:** Armazenamento de arquivos (CNH)

---

