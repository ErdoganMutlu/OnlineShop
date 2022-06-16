using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.ObjectModels.Entities;

public class Order
{
    public Order()
    {
        OrderProducts = new List<OrderProduct>();
    }
        
    [Key]
    public virtual int Id { get; set; }
                
    public virtual int CustomerId { get; set; }
        
    public virtual IList<OrderProduct> OrderProducts { get; set; }
        
    public virtual DateTime OrderDateTime { get; set; }
}