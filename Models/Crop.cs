using System.Text.Json.Serialization;

namespace GreenhouseAPI.Models
{
    public class Crop
    {
        public Guid Id { get; set; }
        public DateTime PlantingDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public CropStatus Status { get; set; }
        public int NumberOfTrays { get; set; }
        public int trayCapacity { get; set; }
        public Plant Plant { get; set; }
        public Guid PlantId { get; set; }
        [JsonIgnore]
        public Greenhouse Greenhouse { get; set; }
        public Guid GreenhouseId { get; set; }
    }

    public enum CropStatus { planting, germination, readyToDeliver, delivered }
}
