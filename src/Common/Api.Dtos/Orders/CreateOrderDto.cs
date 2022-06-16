using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Orders;

public  class CreateOrderDto
{
    [Required]
    public int CustomerId { get; set; }
        
    [Required]
    [MinLength(1)]
    public List<int> Products { get; set; }
}