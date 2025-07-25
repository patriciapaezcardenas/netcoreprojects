using System.ComponentModel.DataAnnotations;

namespace WebProject.Models;

public class Account
{
    public int Id { get; set; }

    [Required(ErrorMessage = "{0} es requerido.")]
    [StringLength(50)]
    [PrimeraLetraMayuscula]
    [Display(Name = "Nombre")]
    public string Name { get; set; }

    [Display(Name = "Tipo de cuenta")]
    [Required(ErrorMessage = "{0} es requerido.")]
    public int AccountTypeId { get; set; }
    public decimal Balance { get; set; }
    
    [StringLength(maximumLength:1000)]
    public string Description { get; set; }
}
