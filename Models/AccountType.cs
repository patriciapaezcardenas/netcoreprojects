using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebProject.Models;

public class AccountType
{
    public int Id { get; set; }


    [Remote(action: "VerifyAccountTypeIsExist", controller: "AccountType", ErrorMessage = "El tipo de cuenta ya existe.")]
    [PrimeraLetraMayuscula()]
    [Required(ErrorMessage = "{0} es requerido.")]
    [StringLength(100, ErrorMessage = "{0} debe tener entre {2} y {1} caracteres.", MinimumLength = 3)]
    [Display(Name = "Tipo de Cuenta")]
    public string Name { get; set; } = string.Empty;
    public int UserId { get; set; }

    [Range(1,int.MaxValue, ErrorMessage ="El valor debe ser al menos uno.")]
    public int Order { get; set; }

    #region camposadicionales
    /* 
        [EmailAddress(ErrorMessage = "El formato del correo electrónico es inválido.")]
        public string? EmailAddress { get; set; }

        [Range(18, 100, ErrorMessage = "La edad debe estar entre {1} y {2}.")]

        public int? Age { get; set; }

        [Display(Name = "URL del sitio web")]
        [Url(ErrorMessage = "El formato de la URL es inválido.")]
        public string? Url { get; set; }

        [CreditCard(ErrorMessage = "El número de tarjeta de crédito es inválido.")]
        [Display(Name = "Número de Tarjeta de Crédito")]
        public string? CreditCard { get; set; } */
    #endregion
}