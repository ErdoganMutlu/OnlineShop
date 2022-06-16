using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.Tools;
using Api.Tools.ObjectModel;

namespace Api.ObjectModels.Repositories.Customers;

public interface ICustomersRepository : IRepository<Customer>
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<PaginatedResponse<Customer>> GetWhereAsync(Expression<Func<Customer, bool>> predicate, int page, int pageSize);
}