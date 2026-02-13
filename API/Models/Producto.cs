using System.ComponentModel.DataAnnotations;

namespace ElectronicsStoreAPI.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del artículo es requerido")]
        [StringLength(200)]
        public string NombreArticulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La marca es requerida")]
        [StringLength(100)]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser negativa")]
        public int Cantidad { get; set; }

        [StringLength(1000)]
        public string? Descripcion { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
