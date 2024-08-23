using Dapper;
using DiarioPagosApp.Models;
using DiarioPagosApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace DiarioPagosApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepositoryCustomer _repositoryCustomer;
        private readonly IRepositoryUser _repositoryUser;

        public CustomerController(IRepositoryCustomer repositoryCustomer, IRepositoryUser repositoryUser)
        {
            _repositoryCustomer = repositoryCustomer;
            _repositoryUser = repositoryUser;
        }

        public async Task<IActionResult> Index()
        {
            // Obtenemos el usuario actual, para traer todos sus customers
            var userId = _repositoryUser.GetUser();

            var listCustomer = await _repositoryCustomer.ListCustomersForUserId(userId); 
            return View(listCustomer);
        }

        /// <summary>
        /// Metodo que carga la vista para la creacion
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateCustomer()
        {
            return View();
        }

        /// <summary>
        /// Meto que dejecuta la acion del formulario
        /// </summary>
        /// <param name="customer">Instancia de modelo</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            // Obtenemos el usuario actual, para traer todos sus customers
            var userId = _repositoryUser.GetUser();

            // asignamos el id al modelo
            customer.UserId = userId;

            // validamos el modelo
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ModelError", "NotFound");
            }

            // validamos datos duplicados
            var IsRepeated = await _repositoryCustomer.IsRepeatedCustomer(customer.CustomerEmail, userId);
            if (IsRepeated)
            {
                ModelState.AddModelError(nameof(customer.CustomerEmail) ,$"El dato {customer.CustomerEmail} ya existen");
                return View(customer);
            }

            await _repositoryCustomer.CreateCustomer(customer);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Metodo que carga la pagina de editar
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditCustomer(int Id)
        {
            // Obtenemos el usuario actual
            int userId = _repositoryUser.GetUser();
            var customerId = await _repositoryCustomer.ListCustomerForId(Id, userId);
            if (customerId is null)
            {
                return RedirectToAction("NotFoundError", "NotFound");
            }

            return View(customerId);
        }

        /// <summary>
        /// Metodo que ejecuta la actualizacion de el dato
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditCustomer(Customer customer)
        {
            // Obtenemos el usuario actual
            int userId = _repositoryUser.GetUser();

            var customerId = await _repositoryCustomer.ListCustomerForId(customer.CustomerId, userId);
            if (customerId is null)
            {
                return RedirectToAction("NotFoundError", "NotFound");
            }

            // Asignamos el userId al campo UserId del modelo customer 
            customer.UserId = userId;

            await _repositoryCustomer.UpdateCustomer(customer);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Metodo quye carga al pagina de eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int Id)
        {
            // Obtenemos el usuario actual
            int userId = _repositoryUser.GetUser();

            var customerId = await _repositoryCustomer.ListCustomerForId(Id, userId);
            if (customerId is null)
            {
                return RedirectToAction("NotFoundError", "NotFound");
            }

            return View(customerId);
        }

        /// <summary>
        /// Metodo quye carga al pagina de eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///
        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(int CustomerId)
        {
            // Obtenemos el usuario actual
            int userId = _repositoryUser.GetUser();

            var customer = await _repositoryCustomer.ListCustomerForId(CustomerId, userId);
            if (customer is null)
            {
                return RedirectToAction("NotFoundError", "NotFound");
            }

            await _repositoryCustomer.DeleteCustomer(CustomerId);
            return RedirectToAction("Index");
        }
    }
}
