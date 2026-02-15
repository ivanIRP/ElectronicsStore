using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectronicsStoreAPI.Data;
using ElectronicsStoreAPI.Models;

namespace ElectronicsStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.AsNoTracking()
                .OrderByDescending(p => p.FechaRegistro).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto([FromBody] Producto producto)
        {
            ModelState.Remove(nameof(producto.Id));
            ModelState.Remove(nameof(producto.FechaRegistro));

            if (!ModelState.IsValid)
            {
                var listaErrores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { message = "Validación fallida", errors = listaErrores });
            }

            try 
            {
                producto.Id = 0; 
                producto.FechaRegistro = DateTime.Now;

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error en base de datos", detail = ex.Message });
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound(new { message = "Producto no encontrado" });
            return producto;
        }
    }
}