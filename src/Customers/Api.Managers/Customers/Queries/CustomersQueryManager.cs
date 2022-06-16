using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.ObjectModels.Repositories.Customers;
using Api.Tools.Extentions;
using Api.Tools.ObjectModel;

namespace Api.Managers.Customers.Queries;

internal class CustomersQueryManager : ICustomersQueryManager
{
    private readonly ICustomersRepository _customersRepository;

    public CustomersQueryManager(ICustomersRepository customersRepository)
    {
        _customersRepository = customersRepository;
    }

    public async Task<PaginatedResponse<Customer>> GetCustomersByNameAsync(string name, int page, int pageSize)
    {
        Expression<Func<Customer, bool>> expression = obj => true;

        expression = expression.And(e => e.Name == name);

        return await _customersRepository.GetWhereAsync(expression, page, pageSize);
    }
        
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _customersRepository.GetAllAsync();
    }
        
    public async Task<Customer> GetByIdAsync(int id)
    {
        return await _customersRepository.GetByIdAsync(id);
    }
}