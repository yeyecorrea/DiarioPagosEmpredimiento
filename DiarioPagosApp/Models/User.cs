﻿using System.ComponentModel.DataAnnotations;

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
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string  FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = ("El campo debe ser de minimo 2 caracteres y como maximo 50"))]
        [DataType(DataType.Text)]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [EmailAddress(ErrorMessage ="El correo no es valido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no es valido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo")]
        public string StandardEmail { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string PassWordHash { get; set; }
    }
}
