using System.Threading.Tasks;

namespace Api.Managers.OrderViews;

public interface IOrderViewsRabbitMqManager
{
    Task<bool> OrderReceivedAsync(string content);
}