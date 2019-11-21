using System.ComponentModel.DataAnnotations;

using RaceApp.Models;

public class CarTypeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var car = (Car)validationContext.ObjectInstance;

        if (car.IsEnduro && car.EngineBuilder == null)
        {
            return new ValidationResult(GetEngineBuilderErrorMessage());
        }
        else if (car.IsEnduro && car.EngineType == null)
        {
            return new ValidationResult(GetEngineTypeErrorMessage());
        }
        else
        {
            return ValidationResult.Success;
        }
    }

    public string GetEngineBuilderErrorMessage()
    {
        return $"Engine Builder required for Enduro Cars";
    }

    public string GetEngineTypeErrorMessage()
    {
        return $"Engine Type required for Enduro Cars";
    }
}