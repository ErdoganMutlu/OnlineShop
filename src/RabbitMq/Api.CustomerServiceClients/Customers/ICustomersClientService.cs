using System.Threading.Tasks;
using Api.Dtos.Customers;

namespace Api.CustomerServiceClients.Customers;

public interface ICustomersClientService
{
    Task<CustomerDto> GetCustomerByIdAsync(int id);
}