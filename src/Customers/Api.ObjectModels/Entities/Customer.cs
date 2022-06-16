using System;
using System.ComponentModel.DataAnnotations;

namespace Api.ObjectModels.Entities;

public sealed class Customer
{
    [Key]
    public int Id { get; set; }
                
    public string Name { get; set; }
        
    public string Surname { get; set; }
        
    public string Details { get; set; }
}