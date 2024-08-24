using System.ComponentModel.DataAnnotations;

namespace DiarioPagosApp.Models
{
    /// <summary>
    /// Modelo para la entidad producto
    /// </summary>
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = ("El campo debe ser de minimo 2 caracteres y como maximo 50"))]
        [Display(Name = "Nombre del Producto")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public int UserId { get; set; }
    }
}
