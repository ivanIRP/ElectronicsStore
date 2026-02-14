using Microsoft.AspNetCore.Mvc;
using ElectronicsStoreWeb.Services;

namespace ElectronicsStoreWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiService _apiService;

        public HomeController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var reporte = await _apiService.GetReporteVentasAsync();
            var productos = await _apiService.GetProductosAsync();
            
            ViewBag.Reporte = reporte;
            ViewBag.TotalProductos = productos.Count;
            ViewBag.ProductosBajoStock = productos.Count(p => p.Cantidad < 5);
            
            return View();
        }
    }
}
