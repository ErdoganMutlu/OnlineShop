using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dtos.Products;

namespace Api.ProductServiceClients.Products;

public interface IProductsClientService
{
    Task<ProductDto> GetProductByIdAsync(int id);
    Task<IList<ProductDto>> GetProductsByIdAsync(IList<int> productIds);
}