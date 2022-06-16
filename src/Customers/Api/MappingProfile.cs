using Api.Dtos;
using Api.Dtos.Customers;
using Api.ObjectModels.Entities;
using Api.Tools.ObjectModel;
using AutoMapper;


namespace Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<CustomerDto, Customer>();
        CreateMap<Customer, CustomerDto>();
        CreateMap<PaginatedResponse<Customer>, PaginatedResponseDto<CustomerDto>>();
    }
}