using Microsoft.AspNetCore.Mvc;
using ElectronicsStoreWeb.Models;
using ElectronicsStoreWeb.Services;

namespace ElectronicsStoreWeb.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApiService _apiService;

        public ProductosController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _apiService.GetProductosAsync();
            return View(productos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var producto = await _apiService.GetProductoAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                var result = await _apiService.CreateProductoAsync(producto);
                if (result)
                {
                    TempData["Success"] = "Producto creado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Error al crear el producto");
            }
            return View(producto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _apiService.GetProductoAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _apiService.UpdateProductoAsync(producto);
                if (result)
                {
                    TempData["Success"] = "Producto actualizado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Error al actualizar el producto");
            }
            return View(producto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _apiService.GetProductoAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _apiService.DeleteProductoAsync(id);
            if (result)
            {
                TempData["Success"] = "Producto eliminado exitosamente";
            }
            else
            {
                TempData["Error"] = "Error al eliminar el producto";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
