# Desafio-Backend
📚 Sumário

Visão Geral

Estrutura do Projeto

Tecnologias Utilizadas

Como Executar

Executando Testes

Funcionalidades e Casos de Uso

Infraestrutura com Docker

📖 Visão Geral

O Desafio-Backend é uma aplicação backend voltada para o controle de frota e entregadores, com suporte a operações de locação, integração com fila de eventos e armazenamento de imagens de CNH.

🏗️ Estrutura do Projeto

Domain: Contém as entidades de negócio, interfaces e regras puras da aplicação.

Application: Camada responsável por serviços, validações, DTOs, mapeamentos e lógica de orquestração.

Infrastructure: Implementações práticas (repositórios, RabbitMQ, MinIO, PostgreSQL).

API: Ponto de entrada HTTP com controllers, middlewares e configuração.

Test: Projetos de testes unitários e de integração usando xUnit.

🧰 Tecnologias Utilizadas

.NET 8

PostgreSQL

RabbitMQ

MinIO

xUnit

Docker + Docker Compose

🚀 Como Executar

Pré-requisitos:

Docker e Docker Compose instalados na máquina.

Passos

Configure suas variáveis de ambiente no arquivo .env (baseie-se no appsettings.Development.json).

Execute o comando:

bash

Copiar

Editar

docker-compose --env-file .env up --build

A API estará disponível via Swagger:

http://localhost:5000/swagger

🧪 Executando Testes

Os testes estão localizados no diretório test/. Para executá-los:

bash

Copiar

Editar

dotnet test

✅ Funcionalidades e Casos de Uso

Gerenciamento de motos: criação, consulta, atualização e exclusão.

Cadastro de entregadores com envio de imagem da CNH.

Registro de locações de motos.

Publicação de eventos de motos via RabbitMQ.

Armazenamento de arquivos no MinIO.

Detalhes completos das rotas e contratos estão disponíveis no Swagger da aplicação.

🐳 Infraestrutura com Docker

O ambiente é composto por:

rentchallenge-api: API principal desenvolvida em .NET 8

postgres: Banco de dados relacional

rabbitmq: Serviço de mensageria

minio: Armazenamento de arquivos (como CNHs)
