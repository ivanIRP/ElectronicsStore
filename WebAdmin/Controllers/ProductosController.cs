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

        public async Task<IActionResult> Index() => View(await _apiService.GetProductosAsync());

        public IActionResult Create() => View(new Producto());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            ModelState.Remove(nameof(producto.FechaRegistro));
            if (ModelState.IsValid)
            {
                var (success, error, _) = await _apiService.CreateProductoAsync(producto);
                if (success) {
                    TempData["Success"] = "¡Producto creado!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", error);
            }
            return View(producto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _apiService.GetProductoAsync(id);
            if (producto == null) return NotFound();
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
                var (success, error) = await _apiService.UpdateProductoAsync(producto);
                if (success) {
                    TempData["Success"] = "¡Actualizado!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", error);
            }
            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var (success, error) = await _apiService.DeleteProductoAsync(id);
            if (success) TempData["Success"] = "Eliminado correctamente";
            else TempData["Error"] = error;
            return RedirectToAction(nameof(Index));
        }
    }
}