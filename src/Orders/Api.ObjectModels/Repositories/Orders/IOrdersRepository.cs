using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.Tools;
using Api.Tools.ObjectModel;

namespace Api.ObjectModels.Repositories.Orders;

public interface IOrdersRepository
{
    Order AddOrUpdate(Order entity);
    Task SaveChangesAsync();
}