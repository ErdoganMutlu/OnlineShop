using System;
using System.Threading.Tasks;
using Api.ObjectModels.Entities;
using Api.Tools.ObjectModel;

namespace Api.Managers.OrderViews.Queries;

public interface IOrderViewsQueryManager
{
    Task<PaginatedResponse<OrderView>> FilterOrderViews(DateTime from, DateTime to, int page, int pageSize);
}