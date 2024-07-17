using GreenhouseAPI.Data;
using GreenhouseAPI.DTOs;
using GreenhouseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GreenhouseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CropsController : ControllerBase
    {
        private readonly GreenhouseDbContext _context;

        public CropsController(GreenhouseDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crop>>> GetCrops()
        {
            return await _context.Crops.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Crop>> GetCrop(Guid id)
        {
            var crop = await _context.Crops.FindAsync(id);
            if (crop == null)
            {
                return NotFound();
            }
            return crop;
        }

        [HttpPost]
        public async Task<ActionResult<Crop>> PostCrop(CreateCropDto crop)
        {
            var newCrop = new Crop
            {
                PlantId = crop.PlantId,
                GreenhouseId = crop.GreenhouseId,
                Status = (CropStatus)crop.Status
            };

            _context.Crops.Add(newCrop);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCrop", new { id = newCrop.Id }, newCrop);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrop(Guid id, Crop crop)
        {
            if (id != crop.Id)
            {
                return BadRequest();
            }
            _context.Entry(crop).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CropExists(id))
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
        public async Task<IActionResult> DeleteCrop(Guid id)
        {
            var crop = await _context.Crops.FindAsync(id);
            if (crop == null)
            {
                return NotFound();
            }
            _context.Crops.Remove(crop);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool CropExists(Guid id)
        {
            return _context.Crops.Any(e => e.Id == id);
        }
    }
}