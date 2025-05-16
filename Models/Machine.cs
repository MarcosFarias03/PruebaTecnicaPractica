using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaAPI.Models
{
    public class Machine
    {
        public int Id { get; set; }

        [Required]
        public string TechnicalLocation { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string MachineTypeName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public string Criticality { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;

        public List<Component> Components { get; set; } = new();
    }
}
