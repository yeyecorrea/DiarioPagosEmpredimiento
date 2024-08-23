using System.ComponentModel.DataAnnotations;

namespace DiarioPagosApp.Models
{
    public class UserRegistrationViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = ("El campo debe ser de minimo 2 caracteres y como maximo 50"))]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = ("El campo debe ser de minimo 2 caracteres y como maximo 50"))]
        [DataType(DataType.Text)]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no es valido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string PassWord { get; set; }
    }
}
