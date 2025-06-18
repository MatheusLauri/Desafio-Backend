// Usings para trabalhar com requisições HTTP, controle de rotas e manipulação de arquivos.
// Também inclui os DTOs e interfaces da aplicação relacionados a entregadores.
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentChallenge.Application.DTOs.Requests.DeliveryMan;
using RentChallenge.Application.Interfaces.APIs;

namespace RentChallenge.API.Controllers
{
    // Define a rota base como 'api/entregadores'
    // [ApiController] ativa binding automático, validação e mensagens de erro padrão.
    [Route("api/entregadores")]
    [ApiController]
    public class DeliveryManController(IDeliveryManApiService service) : ControllerBase
    {
        // Injeta a interface do serviço de entregadores e armazena em uma variável privada.
        private readonly IDeliveryManApiService _service = service;

        // POST api/entregadores
        // Cadastra um novo entregador com os dados enviados no corpo da requisição.
        [HttpPost]
        public async Task<IActionResult> RegisterDeliveryMan([FromBody] RegisterDeliveryManDTO deliveryMan)
        {
            await _service.RegisterAsync(deliveryMan); // Chama o service para registrar o entregador.
            return Created(string.Empty, null); // Retorna 201 Created, mas sem corpo nem localização.
            // 🔍 Pode ser melhorado retornando a URI do novo recurso e o DTO registrado.
        }

        // POST api/entregadores/{identificador}/cnh
        // Realiza o upload da imagem da CNH de um entregador.
        [HttpPost("{identificador}/cnh")]
        public async Task<IActionResult> UploadDeliveryManCnh(string identificador, IFormFile file)
        {
            // Usa o stream do arquivo, o tipo MIME e o identificador para delegar ao service.
            await _service.UploadCnhImage(file.OpenReadStream(), identificador, file.ContentType);
            return Created(string.Empty, null); // Retorna 201 Created sem detalhes.
            // 🔍 Pode incluir a URL do recurso armazenado ou metadados do upload.
        }
    }
}
