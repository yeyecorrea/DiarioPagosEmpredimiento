using DiarioPagosApp.Models;
using DiarioPagosApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiarioPagosApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepositoryProduct _repositoryProduct;
        private readonly IRepositoryUser _repositoryUser;

        public ProductController(IRepositoryProduct repositoryProduct, IRepositoryUser repositoryUser)
        {
            _repositoryProduct = repositoryProduct;
            _repositoryUser = repositoryUser;
        }

        public async Task<IActionResult> Index()
        {
            // Obtenemos el usuario actual
            var userId = _repositoryUser.GetUser();
            var listProducts = await _repositoryProduct.ListProductsForUserId(userId);

            return View(listProducts);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            // Obtenemos el usuario actual
            var userId = _repositoryUser.GetUser();

            //Validamos el modelo
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Validamos que no este repetido el dato antes de guardarlo
            var productIsRepeated = await _repositoryProduct.IsRepeatedProduct(product.ProductName, userId);
            if (productIsRepeated)
            {
                ModelState.AddModelError(nameof(product.ProductName), $"El {product.ProductName} ya se encuentra registrado");
                return View(product);
            }

            // Asignamos el Id del usuario al modelo product
            product.UserId = userId;

            await _repositoryProduct.CreateProduct(product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditProduct(int Id)
        {
            // Obtenemos el usuario actual
            var userId = _repositoryUser.GetUser();

            // Optenemos el Id a editar
            var productId = await _repositoryProduct.ListProductForId(Id, userId);
            if (productId is null)
            {
                return View();
            }

            return View(productId);

        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {
            // Obtenemos el usuario actual
            var userId = _repositoryUser.GetUser();

            // Optenemos el Id a editar
            var productId = await _repositoryProduct.ListProductForId(product.ProductId, userId);
            if (productId is null)
            {
                return View(); 
            }

            //// Validamos que el Dato no se repita
            //var IsRepead = await _repositoryProduct.IsRepeatedProduct(product.ProductName);
            //if (IsRepead)
            //{
            //    return View(product);
            //}

            product.UserId = userId;

            await _repositoryProduct.UpdateProduct(product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var userId = _repositoryUser.GetUser();

            // Optenemos el Id a editar
            var productId = await _repositoryProduct.ListProductForId(Id, userId);
            if (productId is null)
            {
                return View();
            }

            return View(productId);

        }

        /// <summary>
        /// Metodo quye carga al pagina de eliminar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int ProductId)
        {
            var userId = _repositoryUser.GetUser();

            // Optenemos el Id a eliminar
            var productId = await _repositoryProduct.ListProductForId(ProductId, userId);
            if (productId is null)
            {
                return View();
            }

            await _repositoryProduct.DeleteProduct(ProductId, userId);
            return RedirectToAction("Index");
        }
    }
}
