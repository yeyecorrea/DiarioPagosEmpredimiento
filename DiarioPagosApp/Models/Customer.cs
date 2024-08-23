using System.ComponentModel.DataAnnotations;

namespace DiarioPagosApp.Models
{
    /// <summary>
    /// Modelo para la entidad cliente
    /// </summary>
    public class Customer
    {

        public int CustomerId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = ("El campo debe ser de minimo 2 caracteres y como maximo 50"))]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = ("El campo debe ser de minimo 2 caracteres y como maximo 50"))]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = ("El campo debe ser de minimo 5 caracteres y como maximo 50"))]
        [Display(Name = "Direccion")]
        public string Address { get; set; }

        [Phone(ErrorMessage = "El Numero de telefono no es valido")]
        [Display(Name = "Numero de telefono")]
        public string PhoneNumber { get; set; } 


        [EmailAddress(ErrorMessage = "El correo no es valido")]
        [Display(Name = "Correo")]
        public string CustomerEmail { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public int UserId { get; set; }
    }
}
