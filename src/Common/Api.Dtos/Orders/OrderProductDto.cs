namespace Api.Dtos.Orders;

public class OrderProductDto
{
    public virtual int Id { get; set; }
    public virtual int OrderId { get; set; }
    public virtual int ProductId { get; set; }      
}