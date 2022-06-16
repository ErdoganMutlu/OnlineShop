using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.ObjectModels.Repositories.Customers;

namespace Api.Managers.Customers.Commands;

internal class CustomersCommandManager : ICustomersCommandManager
{
    private readonly ICustomersRepository _customersRepository;

    public CustomersCommandManager(ICustomersRepository customersRepository)
    {
        _customersRepository = customersRepository;
    }
        
    public async Task<Customer> AddAsync(Customer customer)
    {
        _customersRepository.AddOrUpdate(customer);

        await _customersRepository.SaveChangesAsync();

        return customer;
    }

    public async Task<Customer> UpdateAsync(Customer customer)
    {
        _customersRepository.AddOrUpdate(customer);

        await _customersRepository.SaveChangesAsync();

        return customer;
    }
        

    public async Task<bool> DeleteAsync(int id)
    {
        _customersRepository.Delete(id);

        await _customersRepository.SaveChangesAsync();

        return true;
    }
}