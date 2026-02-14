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
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            // ELIMINAR VALIDACIÓN: Evita que la API rechace el producto por no traer ID o Fecha
            ModelState.Remove(nameof(producto.Id));
            ModelState.Remove(nameof(producto.FechaRegistro));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            try 
            {
                producto.Id = 0; // Garantiza autoincremento en SQLite
                producto.FechaRegistro = DateTime.Now; // El servidor pone la fecha real

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error en BD", detail = ex.Message });
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();
            return producto;
        }
    }
}