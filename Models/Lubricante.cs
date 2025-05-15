using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaAPI.Models
{
    public class Lubricante
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string Tipo { get; set; } = string.Empty;

        public string Viscosidad { get; set; } = string.Empty;

        public int ProveedorId { get; set; }

        public Proveedor? Proveedor { get; set; }
    }
}
