// Usings necessários para validação, rotas, manipulação HTTP, DTOs e serviços da aplicação.
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentChallenge.Application.DTOs.Requests.Motorcycle;
using RentChallenge.Application.Interfaces.APIs;
using RentChallenge.Application.Services.APIs;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Exceptions;

namespace RentChallenge.API.Controllers
{
    // Define que o controller responderá por rotas que começam com 'api/motos'
    // e ativa recursos automáticos de binding, validação e respostas de erro via [ApiController]
    [Route("api/motos")]
    [ApiController]
    public class MotorcycleController(IMotorcycleApiService service) : ControllerBase
    {
        // Injeção de dependência do serviço da aplicação para motos.
        // Atribui o serviço recebido no construtor para uso interno.
        private readonly IMotorcycleApiService _service = service;

        // POST api/motos
        // Endpoint para registrar uma nova moto usando dados recebidos no corpo da requisição (DTO).
        [HttpPost]
        public async Task<ActionResult> RegisterAsync(RegisterMotorcycleRequestDTO dto)
        {
            await _service.RegisterAsync(dto); // Chama o service para registrar a moto.
            return Created($"api/motorcycles/{dto.Identifier}", dto); // Retorna 201 Created com rota e dados.
        }

        // GET api/motos?placa=XYZ1234
        // Lista todas as motos ou filtra por placa, se fornecida como query string.
        [HttpGet]
        public async Task<ActionResult> GetAllAsync(string? placa)
        {
            var motorcycles = await _service.GetAllAsync(placa); // Busca as motos (todas ou filtradas).
            return Ok(motorcycles); // Corrigido: retorna a lista de motos.
        }

        // GET api/motos/{identificador}
        // Busca uma moto específica pelo identificador passado na URL.
        [HttpGet("{identificador}")]
        public async Task<ActionResult<Motorcycle>> GetByIdentifierAsync(string identificador)
        {
            var motorcycle = await _service.GetByIdentifierAsync(identificador); // Busca no service.
            return Ok(motorcycle); // Retorna a moto encontrada com status 200.
        }

        // PUT api/motos/{identificador}/placa
        // Atualiza a placa da moto identificada por 'identificador'.
        [HttpPut("{identificador}/placa")]
        public async Task<ActionResult> UpdateNumberPlateAsync(string identificador, [FromBody] UpdateMotorcycleNumberPlateDTO dto)
        {
            await _service.UpdateNumberPlateAsync(identificador, dto.NumberPlate); // Atualiza a placa.
            return Ok(); // Retorna 200 OK indicando sucesso.
        }

        // DELETE api/motos/{identificador}
        // Remove uma moto com base no identificador passado na URL.
        [HttpDelete("{identificador}")]
        public async Task<ActionResult> DeleteAsync(string identificador)
        {
            await _service.DeleteAsync(identificador); // Chama o service para remover a moto.
            return Ok(); // Retorna 200 OK após remoção bem-sucedida.
        }
    }
}
