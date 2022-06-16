using Api.Dtos;
using Api.Dtos.Products;
using Api.ObjectModels.Entities;
using Api.Tools.ObjectModel;
using AutoMapper;


namespace Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<ProductDto, Product>();
        CreateMap<Product, ProductDto>();
        CreateMap<PaginatedResponse<Product>, PaginatedResponseDto<ProductDto>>();
    }
}