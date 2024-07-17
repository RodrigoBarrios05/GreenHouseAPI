using GreenhouseAPI.Models;

namespace GreenhouseAPI.DTOs
{
    public class CreateCropDto
    {
        public DateTime PlantingDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public CropStatus Status { get; set; }
        public int NumberOfTrays { get; set; }
        public int trayCapacity { get; set; }
        public Guid PlantId { get; set; }
        public Guid GreenhouseId { get; set; }
    }
}
