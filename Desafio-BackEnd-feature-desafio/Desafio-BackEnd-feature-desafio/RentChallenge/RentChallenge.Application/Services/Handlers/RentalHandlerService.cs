using RentChallenge.Application.Interfaces.Handlers;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Define o namespace da camada de aplicação, onde residem os serviços de manipulação (handlers) de entidades e eventos.
namespace RentChallenge.Application.Services.Handlers
{
    // Serviço handler responsável por tratar a persistência da entidade Rental (aluguel).
    // Implementa a interface IRentalHandlerService e injeta o repositório necessário via construtor primário.
    public class RentalHandlerService(
        IRentalRepository repository
    ) : IRentalHandlerService
    {
        private readonly IRentalRepository _repository = repository;

        // Adiciona a entidade Rental ao repositório de forma assíncrona.
        // Não realiza validação ou commit de transação — assume que isso será tratado externamente.
        public async Task HandleAsync(Rental rental) =>
            await _repository.AddAsync(rental);
    }
}
