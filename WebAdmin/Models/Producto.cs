using System.ComponentModel.DataAnnotations;

namespace ElectronicsStoreWeb.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del artículo es requerido")]
        [Display(Name = "Nombre del Artículo")]
        [StringLength(200)]
        public string NombreArticulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La marca es requerida")]
        [StringLength(100)]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser negativa")]
        public int Cantidad { get; set; }

        [StringLength(1000)]
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string? Descripcion { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }
    }
}
