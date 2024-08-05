using System.ComponentModel.DataAnnotations;

namespace DiarioPagosApp.Models
{
    /// <summary>
    /// Modelo para la entidad Usario
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength:50, MinimumLength = 2, ErrorMessage =("El campo debe ser de minimo 2 caracteres y como maximo 50"))]
        [Display(Name = "Nombre")]
        public int  FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = ("El campo debe ser de minimo 2 caracteres y como maximo 50"))]
        [Display(Name = "Apellido")]
        public int LastName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [EmailAddress(ErrorMessage ="El correo no es valido")]
        [Display(Name = "Correo")]
        public int UserEmail { get; set; }

        [Display(Name = "Contraseña")]
        public int PassWordHas { get; set; }
    }
}
