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
            
            ViewBag.Reporte = reporte ?? new { 
                totalVentas = 0m, 
                totalProductosVendidos = 0, 
                numeroCompras = 0, 
                promedioVenta = 0m 
            };
            
            ViewBag.TotalProductos = productos?.Count ?? 0;
            ViewBag.ProductosBajoStock = productos?.Count(p => p.Cantidad < 5) ?? 0;
            
            return View();
        }
    }
}