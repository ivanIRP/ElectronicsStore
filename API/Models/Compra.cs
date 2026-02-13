using System.ComponentModel.DataAnnotations;

namespace ElectronicsStoreAPI.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [Required]
        public decimal PrecioTotal { get; set; }

        public DateTime FechaCompra { get; set; } = DateTime.Now;

        public string? ClienteInfo { get; set; }
    }
}
