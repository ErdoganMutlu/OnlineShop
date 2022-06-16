using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.Orders;

public class OrderFilterDto : IValidatableObject
{
    [Required(ErrorMessage = "Start date and time cannot be empty")]
    //validate:Must be greater than current date
    [DataType(DataType.DateTime)]
    public DateTime From { get; set; }

    [Required(ErrorMessage = "End date and time cannot be empty")]
    //validate:must be greater than StartDate
    [DataType(DataType.DateTime)]
    public DateTime To { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (From > To)
        {
            yield return new ValidationResult($"From must be bigger than To");
        }

        if (From > DateTime.Now || To > DateTime.Now)
        {
            yield return new ValidationResult($"Dates must be past");
        }
    }
}