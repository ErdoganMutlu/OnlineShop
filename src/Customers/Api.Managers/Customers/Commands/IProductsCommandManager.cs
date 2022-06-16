using System.Threading.Tasks;
using Api.ObjectModels.Entities;

namespace Api.Managers.Customers.Commands;

public interface ICustomersCommandManager
{
    Task<Customer> AddAsync(Customer customer);
    Task<Customer> UpdateAsync(Customer customer);
    Task<bool> DeleteAsync(int id);
}