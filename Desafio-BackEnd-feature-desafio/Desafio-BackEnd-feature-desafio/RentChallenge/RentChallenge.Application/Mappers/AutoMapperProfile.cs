using AutoMapper;
using RentChallenge.Application.DTOs.Requests.DeliveryMan;
using RentChallenge.Application.DTOs.Requests.Motorcycle;
using RentChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RentChallenge.Application.Mappers
{
    // AutoMapper profile que define regras de mapeamento entre DTOs e entidades.
    public class AutoMapperProfile : Profile
    {
        // Construtor da classe onde os mapeamentos são registrados
        public AutoMapperProfile()
        {
            // Mapeia diretamente RegisterMotorcycleRequestDTO para Motorcycle
            CreateMap<RegisterMotorcycleRequestDTO, Motorcycle>();

            // Mapeia o mesmo DTO para um EventMotorcycleRegistered (usado para eventos)
            CreateMap<RegisterMotorcycleRequestDTO, EventMotorcycleRegistered>()
                .ForMember(dest => dest.MotorcycleId, opt => opt.MapFrom(src => src.Identifier))       // Define o ID da moto
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))                     // Define o ano
                .ForMember(dest => dest.DateRecived, opt => opt.MapFrom(_ => DateTime.UtcNow))         // Gera a data atual (UTC) no momento do mapeamento
                .ForMember(dest => dest.SerializedPayload, opt => opt.MapFrom(src => SerializePayload(src))); // Serializa todo o DTO original em JSON

            // Mapeia diretamente o DTO do entregador para a entidade DeliveryMan
            CreateMap<RegisterDeliveryManDTO, DeliveryMan>();
        }

        // Método auxiliar privado que serializa o DTO de motocicleta em string JSON
        private string SerializePayload(RegisterMotorcycleRequestDTO src) => JsonSerializer.Serialize(src);
    }
}