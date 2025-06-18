using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RentChallenge.Application;
using RentChallenge.Consumer;
using RentChallenge.Infrastructure;
using RentChallenge.Infrastructure.Data;

namespace RentChallenge.API
{
    // Classe utilitária responsável por configurar e resolver as dependências da aplicação.
    public class Configuration
    {
        // Método estático que aplica as configurações ao builder do WebApplication.
        public static void ResolveDepedencies(WebApplicationBuilder builder)
        {
            // Carrega configurações de arquivos JSON e variáveis de ambiente:
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Configuração base obrigatória
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true) // Config específico por ambiente (Development, Production, etc.)
                .AddEnvironmentVariables(); // Permite sobrescrever config com variáveis de ambiente (útil em produção ou container)

            // Registra os controllers da API (necessário para que endpoints funcionem)
            builder.Services.AddControllers();

            // Registra suporte à exploração de endpoints via Swagger ou minimal APIs
            builder.Services.AddEndpointsApiExplorer();

            // Chama métodos de extensão que encapsulam injeções de dependência por camada (boas práticas de modularização):
            builder.Services.AddInfrastructure(builder.Configuration); // camada de infraestrutura (DB, serviços externos, etc.)
            builder.Services.AddConsumer();                             // consumidores (mensageria, fila, etc.)
            builder.Services.AddApplication();                          // camada de aplicação (serviços, use cases, regras de negócio)

            // Configura o Swagger para geração de documentação da API
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RentChallenge API", Version = "v1" });
            });
        }
    }
}
