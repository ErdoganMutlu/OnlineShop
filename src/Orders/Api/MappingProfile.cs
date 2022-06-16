using Api.Dtos;
using Api.Dtos.Orders;
using Api.Dtos.Products;
using Api.ObjectModels.Entities;
using Api.Tools.ObjectModel;
using AutoMapper;


namespace Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<OrderProduct, OrderProductDto>();
        
        CreateMap<OrderView, OrderViewDto>();
        CreateMap<OrderViewDto, OrderView>();
        CreateMap<PaginatedResponse<OrderView>, PaginatedResponseDto<OrderViewDto>>();
    }
}