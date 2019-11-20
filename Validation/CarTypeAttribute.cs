using System.ComponentModel.DataAnnotations;

using RaceApp.Models;

public class CarTypeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var car = (Car)validationContext.ObjectInstance;
        
        if (car.IsEnduro && (car.EngineBuilder == null || car.EngineType == null))
        {
            return new ValidationResult(GetErrorMessage());
        }

        return ValidationResult.Success;
    }

    public string GetErrorMessage()
    {
        return $"Enduro Cars require and Engine Builder and Engine Type";
    }
}