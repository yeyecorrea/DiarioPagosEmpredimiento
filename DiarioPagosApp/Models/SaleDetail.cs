﻿using System.ComponentModel.DataAnnotations;

namespace DiarioPagosApp.Models
{
    /// <summary>
    /// Modelo para la entidad detalles de venta
    /// </summary>
    public class SaleDetail
    {
        public int SalesDetailId { get; set; }
        [Required]
        [Display(Name = "Venta")]
        public int SaleId { get; set; }

        [Required]
        [Display(Name = "Producto")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Cantidad")]
        public int Amount { get; set; }

        [Required]
        [Display(Name = "Precio")]
        public decimal Price { get; set; }

    }
}
