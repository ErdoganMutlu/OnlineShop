using System;
using System.ComponentModel.DataAnnotations;

namespace Api.ObjectModels.Entities;

public class Product
{
    public Product()
    {
        
    }
    
    [Key]
    public virtual int Id { get; set; }
                
    public virtual string Name { get; set; }
        
    public virtual double Price { get; set; }
        
    public virtual string Details { get; set; }
}