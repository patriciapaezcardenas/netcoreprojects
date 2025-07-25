using System.ComponentModel.DataAnnotations;

public class PrimeraLetraMayuscula : System.ComponentModel.DataAnnotations.ValidationAttribute
{

    /// <summary>
    /// Valida si la primera letra del campo es mayúscula.  
    /// </summary>
    /// <param name="value">Valor del campo</param>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return ValidationResult.Success;
        }

        var primeraLetra = value.ToString()![0];

        if (!char.IsUpper(primeraLetra))
        {
            return new ValidationResult($"La primera letra debe ser mayúscula en '{validationContext.DisplayName}'.");
        }

        return ValidationResult.Success;
    }
}