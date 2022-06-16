using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.Tools.ObjectModel;

namespace Api.Managers.Customers.Queries;

public interface ICustomersQueryManager
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> GetByIdAsync(int id);
        
    Task<PaginatedResponse<Customer>> GetCustomersByNameAsync(string name, int page, int pageSize);
}