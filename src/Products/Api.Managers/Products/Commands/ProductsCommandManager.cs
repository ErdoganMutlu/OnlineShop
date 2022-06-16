using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.ObjectModels.Repositories.Products;

namespace Api.Managers.Products.Commands;

internal class ProductsCommandManager : IProductsCommandManager
{
    private readonly IProductsRepository _productsRepository;

    public ProductsCommandManager(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }
        
    public async Task<Product> AddAsync(Product product)
    {
        _productsRepository.AddOrUpdate(product);

        await _productsRepository.SaveChangesAsync();

        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _productsRepository.AddOrUpdate(product);

        await _productsRepository.SaveChangesAsync();

        return product;
    }
        

    public async Task<bool> DeleteAsync(int id)
    {
        _productsRepository.Delete(id);

        await _productsRepository.SaveChangesAsync();

        return true;
    }
}