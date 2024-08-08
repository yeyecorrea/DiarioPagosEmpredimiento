using DiarioPagosApp.Models;
using DiarioPagosApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiarioPagosApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepositoryProduct _repositoryProduct;

        public ProductController(IRepositoryProduct repositoryProduct)
        {
            _repositoryProduct = repositoryProduct;
        }

        public async Task<IActionResult> Index()
        {
            var listProducts = await _repositoryProduct.ListProducts();
            return View(listProducts);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            //Validamos el modelo
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Validamos que no este repetido el dato antes de guardarlo
            var productIsRepeated = await _repositoryProduct.IsRepeatedProduct(product.ProductName);
            if (productIsRepeated)
            {
                ModelState.AddModelError(nameof(product.ProductName), $"El {product.ProductName} ya se encuentra registrado");
                return View(product);
            }

            await _repositoryProduct.CreateProduct(product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditProduct(int Id)
        {
            // Optenemos el Id a editar
            var productId = await _repositoryProduct.ListProductForId(Id);
            if (productId is null)
            {
                return View();
            }

            return View(productId);

        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {
            // Optenemos el Id a editar
            var productId = await _repositoryProduct.ListProductForId(product.ProductId);
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

            await _repositoryProduct.UpdateProduct(product);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int Id)
        {
            // Optenemos el Id a editar
            var productId = await _repositoryProduct.ListProductForId(Id);
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
            // Optenemos el Id a eliminar
            var productId = await _repositoryProduct.ListProductForId(ProductId);
            if (productId is null)
            {
                return View();
            }

            await _repositoryProduct.DeleteProduct(ProductId);
            return RedirectToAction("Index");
        }
    }
}
