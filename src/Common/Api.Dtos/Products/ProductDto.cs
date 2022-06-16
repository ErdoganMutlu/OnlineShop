using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Products;

public  class ProductDto
{
    public int Id { get; set; }
        
    [Required]
    public string Name { get; set; }
        
    [Required]
    public virtual double Price { get; set; }
        
    [Required]
    public virtual string Details { get; set; }
        
}