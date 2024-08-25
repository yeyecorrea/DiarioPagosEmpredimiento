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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfSale { get; set; } = DateTime.Today;

        [Required]
        [Display(Name = "Estado de pago")]
        public string PaymentStatusId { get; set; }


        [Display(Name = "Total de Venta")]
        [DataType(DataType.Currency)]
        public decimal TotalSale { get; set; }

        //// Referencia a tablas

        public string CUSTOMER_FIRST_NAME { get; set; }
    }
}
