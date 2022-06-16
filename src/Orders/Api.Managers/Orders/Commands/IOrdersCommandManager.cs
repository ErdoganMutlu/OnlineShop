using System.Collections.Generic;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;

namespace Api.Managers.Orders.Commands;

public interface IOrdersCommandManager
{
    Task<Order> AddAsync(int customerId, IEnumerable<int> products);
}