using System.Threading.Tasks;
using Api.ObjectModels.Entities;

namespace Api.Managers.Products.Commands;

public interface IProductsCommandManager
{
    Task<Product> AddAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
}