using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Products;

public  class CreateProductDto
{
    [Required]
    public string Name { get; set; }
        
    [Required]
    public virtual double Price { get; set; }
        
    [Required]
    public virtual string Details { get; set; }
        
}