using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DiarioPagosApp.Models
{
    public class SaleCreationViewModel : Sale
    {
        [Required]
        public IEnumerable<SelectListItem> Customers { get; set; }

        public IEnumerable<SelectListItem> PaymentStatus { get; set; }
    }
}
