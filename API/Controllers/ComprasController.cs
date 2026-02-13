using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicsStoreAPI.Data;
using ElectronicsStoreAPI.Models;

namespace ElectronicsStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComprasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Compras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compra>>> GetCompras()
        {
            return await _context.Compras.OrderByDescending(c => c.FechaCompra).ToListAsync();
        }

        // GET: api/Compras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Compra>> GetCompra(int id)
        {
            var compra = await _context.Compras.FindAsync(id);

            if (compra == null)
            {
                return NotFound(new { message = "Compra no encontrada" });
            }

            return compra;
        }

        // POST: api/Compras
        [HttpPost]
        public async Task<ActionResult<Compra>> PostCompra(Compra compra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar que el producto existe
            var producto = await _context.Productos.FindAsync(compra.ProductoId);
            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado" });
            }

            // Verificar stock disponible
            if (producto.Cantidad < compra.Cantidad)
            {
                return BadRequest(new { message = $"Stock insuficiente. Disponible: {producto.Cantidad}" });
            }

            // Calcular precio total
            compra.PrecioTotal = producto.Precio * compra.Cantidad;
            compra.FechaCompra = DateTime.Now;

            // Decrementar stock
            producto.Cantidad -= compra.Cantidad;

            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompra), new { id = compra.Id }, compra);
        }

        // GET: api/Compras/reporte
        [HttpGet("reporte")]
        public async Task<ActionResult<object>> GetReporteVentas()
        {
            var totalVentas = await _context.Compras.SumAsync(c => c.PrecioTotal);
            var totalProductosVendidos = await _context.Compras.SumAsync(c => c.Cantidad);
            var numeroCompras = await _context.Compras.CountAsync();

            return Ok(new
            {
                totalVentas = totalVentas,
                totalProductosVendidos = totalProductosVendidos,
                numeroCompras = numeroCompras,
                promedioVenta = numeroCompras > 0 ? totalVentas / numeroCompras : 0
            });
        }
    }
}
