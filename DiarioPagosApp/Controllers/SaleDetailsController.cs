using DiarioPagosApp.Models;
using DiarioPagosApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DiarioPagosApp.Controllers
{
    public class SaleDetailsController : Controller
    {
        private readonly IRepositoryProduct _repositoryProduct;
        private readonly IRepositorySaleDetail _repositorySaleDetail;
        public SaleDetailsController(IRepositoryProduct repositoryProduct, IRepositorySaleDetail repositorySaleDetail)
        {
            _repositoryProduct = repositoryProduct;
            _repositorySaleDetail = repositorySaleDetail;
        }

        public async Task<IActionResult> Index(int Id)
        {
            var saleDetail = await _repositorySaleDetail.ListSalesDetails(Id);
            if (saleDetail.Count() == 0 || saleDetail.Any())
            {
                return RedirectToAction();
            }

            return View(saleDetail);
        }

        /// <summary>
        /// Metodo que carga la vista de para crear la vista detalle de venta
        /// </summary>
        /// <param name="SaleId"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateSaleDetail(int SaleId)
        {
            var model = new SaleDetailViewModel();
            model.SaleId = SaleId;
            model.Products = await GetProducts();

            return View(model);
        }

        /// <summary>
        /// Metodo que ejecuta la accion de crear detalle de venta
        /// </summary>
        /// <param name="saleDetailViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateSaleDetail(SaleDetailViewModel saleDetailViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction();
            }

            await _repositorySaleDetail.CreateSaleDetail(saleDetailViewModel);
            return RedirectToAction("Index", "Sale");
        }

        /// <summary>
        /// Metdodo que llena la lista de productos
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetProducts()
        {
            var paymetStatusList = await _repositoryProduct.ListProducts();
            return paymetStatusList.Select(p => new SelectListItem(p.ProductName, p.ProductId.ToString()));
        }
    }
}
