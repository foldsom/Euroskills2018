using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Euroskills2018.Data;
using Euroskills2018.Models;

namespace Euroskills2018.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrszagController : ControllerBase
    {
        private readonly EuroskillsContext _context;

        public OrszagController(EuroskillsContext context)
        {
            _context = context;
        }

        // GET: api/Orszag
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Orszagok.ToListAsync());
        }

        // GET: api/Orszag/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var orszag = await _context.Orszagok.FindAsync(id);
            if (orszag == null)
            {
                return NotFound();
            }
            return Ok(orszag);
        }

        // POST: api/Orszag
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Orszag o)
        {
            _context.Orszagok.Add(o);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = o.Id }, o);
        }

        // PUT: api/Orszag/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Orszag o)
        {
            var existing = await _context.Orszagok.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.OrszagNev = o.OrszagNev;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Orszag/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var orszag = await _context.Orszagok.FindAsync(id);
            if (orszag == null)
            {
                return NotFound();
            }

            _context.Orszagok.Remove(orszag);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
