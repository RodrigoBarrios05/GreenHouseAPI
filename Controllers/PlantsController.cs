using GreenhouseAPI.Data;
using GreenhouseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenhouseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlantsController : Controller
    {
        private readonly GreenhouseDbContext _context;
        public PlantsController(GreenhouseDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plant>>> GetPlants()
        {
            return await _context.Plants.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Plant>> GetPlant(Guid id)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }
            return plant;
        }

        [HttpPost]
        public async Task<ActionResult<Plant>> PostPlant(Plant plant)
        {
            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPlant", new { id = plant.Id }, plant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlant(Guid id, Plant plant)
        {
            if (id != plant.Id)
            {
                return BadRequest();
            }
            _context.Entry(plant).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant(Guid id)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }
            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool PlantExists(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
