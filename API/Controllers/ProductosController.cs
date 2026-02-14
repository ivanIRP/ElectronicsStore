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

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos
                .AsNoTracking() // Mejora el rendimiento para consultas de solo lectura
                .OrderByDescending(p => p.FechaRegistro)
                .ToListAsync();
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound(new { message = $"El producto con ID {id} no existe." });
            }

            return producto;
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            // AJUSTE CLAVE: Eliminamos la validación de campos que el cliente no debe enviar
            ModelState.Remove(nameof(producto.Id));
            ModelState.Remove(nameof(producto.FechaRegistro));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try 
            {
                // Aseguramos que la fecha sea la del servidor y el ID sea autogenerado
                producto.Id = 0; 
                producto.FechaRegistro = DateTime.Now;

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno al guardar el producto", detail = ex.Message });
            }
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest(new { message = "El ID de la URL no coincide con el ID del cuerpo." });
            }

            // Al editar, la fecha suele mantenerse, pero podrías querer actualizarla o validarla
            ModelState.Remove(nameof(producto.FechaRegistro));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(producto).State = EntityState.Modified;

            // Evitamos que se intente modificar la fecha de registro original si no queremos
            _context.Entry(producto).Property(x => x.FechaRegistro).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound(new { message = "No se puede actualizar porque el producto ya no existe." });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound(new { message = "El producto que intenta eliminar no existe." });
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}