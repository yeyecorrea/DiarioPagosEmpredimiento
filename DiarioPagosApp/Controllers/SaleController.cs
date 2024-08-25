using DiarioPagosApp.Models;
using DiarioPagosApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient.DataClassification;

namespace DiarioPagosApp.Controllers
{
    public class SaleController : Controller
    {
        private readonly IRepositorySale _repositorySale;
        private readonly IRepositoryCustomer _repositoryCustomer;
        private readonly IRepositoryPaymentStatus _repositoryPaymentStatus;
        private readonly IRepositoryUser _repositoryUser;

        public SaleController(IRepositorySale repositorySale, IRepositoryCustomer repositoryCustomer, IRepositoryPaymentStatus repositoryPaymentStatus, 
            IRepositoryUser repositoryUser)
        {
            _repositorySale = repositorySale;
            _repositoryCustomer = repositoryCustomer;
            _repositoryPaymentStatus = repositoryPaymentStatus;
            _repositoryUser = repositoryUser;

        }

        /// <summary>
        /// Metodo que carga la vista principal
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var userId =  _repositoryUser.GetUser();
            var sales = await _repositorySale.ListSales(userId);

            return View(sales);
        }


        /// <summary>
        /// Metodo que carga la vista de crear
        /// </summary>
        /// <param name="sale"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateSale()
        {
            // Creamos la instancia del modelo
            var modelo = new SaleCreationViewModel();

            // Llamamos al metodo que llena la lista de customers
            modelo.Customers = await GetCustomers();
            modelo.PaymentStatus = await GetPaymentStatus();

            return View(modelo);
        }

        /// <summary>
        /// Metodo que ejecuta la cion del formulario
        /// </summary>
        /// <param name="model"></param>
        /// <param name="numero"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateSale(SaleCreationViewModel model)
        {
            var userId = _repositoryUser.GetUser();
            model.UserId = userId;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ModelError", "NotFound");
            }

            model.Customers = await GetCustomers();
            model.PaymentStatus = await GetPaymentStatus();

            int id = await _repositorySale.CreateSale(model);
            // Redireccionamos al controlador SaleDetails
            return RedirectToAction("CreateSaleDetail", "SaleDetails", new { SaleId = id });

        }

        /// <summary>
        /// Metodo que llena la lista de customers
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetCustomers()
        {
            // Obtenemos el usuario actual
            int userId = _repositoryUser.GetUser();

            var customersList = await _repositoryCustomer.ListCustomersForUserId(userId);
            return customersList.Select(c => new SelectListItem(c.FirstName,c.CustomerId.ToString()));
        }

        /// <summary>
        /// Metdodo que llena la lista de estados de pago
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectListItem>> GetPaymentStatus()
        {
            var paymetStatusList = await _repositoryPaymentStatus.ListPaymentStatus();
            return paymetStatusList.Select(p => new SelectListItem(p.Status, p.PaymentStatusId.ToString()));
        }
    }
}
