using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GreenhouseAPI.Models
{
    public class Plant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Variety { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public string? ImageUrl { get; set; }
    }
}
