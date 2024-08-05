using System.ComponentModel.DataAnnotations;

namespace DiarioPagosApp.Models
{
    /// <summary>
    /// Modelo de la entidad Ventas
    /// </summary>
    public class Sale
    {
        public int SaleId { get; set; }
        [Required]
        [Display(Name = "Cliente")]
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Fecha de Venta")]
        public DateTime DateOfSale { get; set; }

        [Required]
        [Display(Name = "Estado de pago")]
        public string PaymentStatusId { get; set; }

        [Display(Name = "Total de Venta")]
        public decimal TotalSale { get; set; }
    }
}
