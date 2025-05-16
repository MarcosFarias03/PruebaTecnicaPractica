using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PruebaTecnicaAPI.Models
{
    public class Component
    {
        public int Id { get; set; }

        [Required]
        public string Part { get; set; } = string.Empty;

        public string ComponentType { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;

        public int MachineId { get; set; }

        [JsonIgnore]
        public Machine? Machine { get; set; }
    }
}