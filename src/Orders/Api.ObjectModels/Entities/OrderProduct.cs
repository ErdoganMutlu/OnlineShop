using System.ComponentModel.DataAnnotations;

namespace Api.ObjectModels.Entities;

public class OrderProduct
{
    [Key]
    public virtual int Id { get; set; }
    public virtual int OrderId { get; set; }
    public virtual int ProductId { get; set; }      
}