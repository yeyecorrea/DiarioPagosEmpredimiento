using Dapper;
using DiarioPagosApp.Models;
using DiarioPagosApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DiarioPagosApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepositoryCustomer _repositoryCustomer;
        
        public CustomerController(IRepositoryCustomer repositoryCustomer)
        {
            _repositoryCustomer = repositoryCustomer;
        }

        public async Task<IActionResult> Index()
        {
            var listCustomer = await _repositoryCustomer.ListCustomers(); 
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
            // validamos el modelo
            if (!ModelState.IsValid)
            {
                return View();
            }

            // validamos datos duplicados
            var IsRepeated = await _repositoryCustomer.IsRepeatedCustomer(customer.CustomerEmail, customer.PhoneNumber);
            if (IsRepeated)
            {
                ModelState.AddModelError(nameof(customer.CustomerEmail) ,$"El dato {customer.CustomerEmail} o {customer.PhoneNumber} ya existen");
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
            var CustomerId = await _repositoryCustomer.ListCustomerForId(Id);
            if (CustomerId is null)
            {
                return RedirectToAction("");
            }

            return View(CustomerId);
        }

        /// <summary>
        /// Metodo que ejecuta la actualizacion de el dato
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditCustomer(Customer customer)
        {
            
            var CustomerId = await _repositoryCustomer.ListCustomerForId(customer.CustomerId);
            if (CustomerId is null)
            {
                return RedirectToAction("");
            }

            // validamos datos duplicados
            var IsRepeated = await _repositoryCustomer.IsRepeatedCustomer(customer.CustomerEmail, customer.PhoneNumber);
            if (IsRepeated)
            {
                ModelState.AddModelError(nameof(customer.PhoneNumber), $"El dato ya existe");
                return View(customer);
            }

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
            var CustomerId = await _repositoryCustomer.ListCustomerForId(Id);
            if (CustomerId is null)
            {
                return RedirectToAction("");
            }

            return View(CustomerId);
        }

        /// <summary>
        /// Metodo quye carga al pagina de eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteCustomer(int CustomerId)
        {
            var Customer = await _repositoryCustomer.ListCustomerForId(CustomerId);
            if (Customer is null)
            {
                return RedirectToAction("");
            }

            await _repositoryCustomer.DeleteCustomer(CustomerId);
            return RedirectToAction("Index");
        }
    }
}
