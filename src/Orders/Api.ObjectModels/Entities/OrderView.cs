using System;
using System.ComponentModel.DataAnnotations;

namespace Api.ObjectModels.Entities;

public class OrderView
{
    [Key]
    public virtual int Id { get; set; }
    
    public virtual int OrderId { get; set; }
    
    public virtual DateTime OrderDateTime { get; set; }
    
    public virtual int CustomerId { get; set; }
    
    public virtual string CustomerName { get; set; }
    
    public virtual string CustomerSurname { get; set; }
    
    public virtual int ProductId { get; set; }
    
    public string ProductName { get; set; }
    
    public virtual double ProductPrice { get; set; }
    
    public virtual string ProductDetails { get; set; }
}