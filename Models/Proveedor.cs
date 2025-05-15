using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaAPI.Models
{
    public class Proveedor
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string RazonSocial { get; set; } = string.Empty;

        public List<Lubricante> Lubricantes { get; set; } = new();
    }
}
