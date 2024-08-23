using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DiarioPagosApp.Models
{
    public class SaleDetailViewModel : SaleDetail
    {
        public IEnumerable<SelectListItem> Products {  get; set; }

        [Required]
        [Display(Name = "Venta")]
        public int SaleId { get; set; }
    }
}
