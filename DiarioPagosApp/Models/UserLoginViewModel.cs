using System.ComponentModel.DataAnnotations;

namespace DiarioPagosApp.Models
{
    public class UserLoginViewModel
    {

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no es valido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string PassWord { get; set; }

        public bool RememberMe { get; set; }
    }
}
