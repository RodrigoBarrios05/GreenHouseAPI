using GreenhouseAPI.Data;
using GreenhouseAPI.DTOs;
using GreenhouseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenhouseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GreenhousesController : ControllerBase
    {
        private readonly GreenhouseDbContext _context;

        public GreenhousesController(GreenhouseDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Greenhouse>>> GetGreenhouses()
        {
            return await _context.Greenhouses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Greenhouse>> GetGreenhouse(Guid id)
        {
            var greenhouse = await _context.Greenhouses.FindAsync(id);
            if (greenhouse == null)
            {
                return NotFound();
            }
            return greenhouse;
        }

        [HttpPost]
        public async Task<ActionResult<Greenhouse>> PostGreenhouse(CreateGreenhouseDto greenhouse)
        {
            var newGreenhouse = new Greenhouse
            {
                Name = greenhouse.Name,
                TraysCapacity = greenhouse.TraysCapacity,
                Status = (GreenhouseStatus)greenhouse.Status

            };

            _context.Greenhouses.Add(newGreenhouse);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetGreenhouse", new { id = newGreenhouse.Id }, newGreenhouse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGreenhouse(Guid id, Greenhouse greenhouse)
        {
            if (id != greenhouse.Id)
            {
                return BadRequest();
            }
            _context.Entry(greenhouse).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GreenhouseExists(id))
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
        public async Task<IActionResult> DeleteGreenhouse(Guid id)
        {
            var greenhouse = await _context.Greenhouses.FindAsync(id);
            if (greenhouse == null)
            {
                return NotFound();
            }
            _context.Greenhouses.Remove(greenhouse);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool GreenhouseExists(Guid id)
        {
            return _context.Greenhouses.Any(e => e.Id == id);
        }
    }
}
