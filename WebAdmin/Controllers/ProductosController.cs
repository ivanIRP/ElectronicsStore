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

        public IActionResult Create() => View(new Producto());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            // Evita que la validación falle por la fecha que pone el servidor
            ModelState.Remove(nameof(producto.FechaRegistro));

            if (ModelState.IsValid)
            {
                // Desestructuración corregida
                var (success, errorMsg, _) = await _apiService.CreateProductoAsync(producto);
                if (success)
                {
                    TempData["Success"] = "¡Producto creado!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", $"Error API: {errorMsg}");
            }
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto producto)
        {
            ModelState.Remove(nameof(producto.FechaRegistro));
            if (id != producto.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var (success, errorMsg) = await _apiService.UpdateProductoAsync(producto);
                if (success)
                {
                    TempData["Success"] = "¡Actualizado!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", errorMsg);
            }
            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var (success, errorMsg) = await _apiService.DeleteProductoAsync(id);
            if (success) TempData["Success"] = "Eliminado correctamente";
            else TempData["Error"] = errorMsg;
            return RedirectToAction(nameof(Index));
        }
    }
}