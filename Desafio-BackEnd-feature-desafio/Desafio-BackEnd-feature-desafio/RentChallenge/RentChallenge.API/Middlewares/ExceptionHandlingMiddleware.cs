// Usings para validação, exceções customizadas, manipulação HTTP e serialização JSON.
using FluentValidation;
using RentChallenge.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace RentChallenge.API.Middelwares // ⚠️ Dica: "Middelwares" está com erro de digitação. O correto seria "Middlewares"
{
    // Middleware customizado para tratamento global de exceções.
    public class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        // Armazena a próxima etapa do pipeline (ex: outro middleware ou controller).
        private readonly RequestDelegate _next = next;

        // Método principal do middleware: intercepta todas as requisições.
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Executa o próximo middleware no pipeline.
                await _next(context);
            }
            // Captura exceção do FluentValidation (erros de validação de DTOs).
            catch (ValidationException)
            {
                await HandleExceptionAsync(context, "Dados inválidos", HttpStatusCode.BadRequest);
            }
            // Captura exceções personalizadas de "não encontrado" definidas no domínio.
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.NotFound);
            }
            // Captura exceções do tipo BadHttpRequestException.
            catch (BadHttpRequestException)
            {
                await HandleExceptionAsync(context, "Request mal formada", HttpStatusCode.BadRequest);
            }
            // Captura qualquer outra exceção genérica.
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        // Método auxiliar para formatar e enviar a resposta de erro ao cliente.
        private async Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json"; // Define o tipo da resposta como JSON.
            context.Response.StatusCode = (int)statusCode;     // Define o status HTTP.

            var errorResponse = new { mensagem = message };    // Cria um objeto anônimo com a mensagem de erro.
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse)); // Serializa e escreve no body da resposta.
        }
    }
}
