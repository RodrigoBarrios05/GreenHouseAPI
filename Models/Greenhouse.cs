namespace GreenhouseAPI.Models
{
    public class Greenhouse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TraysCapacity { get; set; }
        public GreenhouseStatus Status { get; set; }
        public List<Crop> Crops { get; set; }
    }

    public enum GreenhouseStatus
    {
        Active,
        Inactive,
        UnderMaintenance
    }
}
