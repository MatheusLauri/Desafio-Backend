using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using RentChallenge.Application.DTOs.Requests.DeliveryMan;
using RentChallenge.Application.Interfaces.Registrations;
using RentChallenge.Application.Services.APIs;
using RentChallenge.Domain.Interfaces.Repositories;
using RentChallenge.Domain.Interfaces.Storage;
using System.Threading.Tasks;
using Xunit;

public class DeliveryManApiServiceTests
{
    [Fact]
    public async Task RegisterAsync_InvalidData_ThrowsValidationException()
    {
        // Cria um mock do validador que simula o retorno de um ValidationResult inválido (sem sucesso).
        var validator = new Mock<IValidator<RegisterDeliveryManDTO>>();
        validator.Setup(v => v.ValidateAsync(It.IsAny<RegisterDeliveryManDTO>(), default))
                 .ReturnsAsync(new ValidationResult { });

        // Cria mocks para as outras dependências do serviço, que não serão chamadas neste teste.
        var registrationService = new Mock<IDeliveryManRegistrationService>();
        var repository = new Mock<IDeliveryManRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var mapper = new Mock<IMapper>();
        var fileStorage = new Mock<IFileStorageService>();

        // Instancia o serviço real, injetando os mocks criados.
        var service = new DeliveryManApiService(
            validator.Object,
            registrationService.Object,
            repository.Object,
            unitOfWork.Object,
            mapper.Object,
            fileStorage.Object
        );

        // Executa o método RegisterAsync com um DTO vazio e espera que uma ValidationException seja lançada.
        await Assert.ThrowsAsync<ValidationException>(() =>
            service.RegisterAsync(new RegisterDeliveryManDTO()));
    }

}
