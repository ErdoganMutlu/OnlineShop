using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Customers;

public  class CreateCustomerDto
{
    [Required]
    public string Name { get; set; }
        
    [Required]
    public virtual string Surname { get; set; }
        
    [Required]
    public virtual string Details { get; set; }
        
}