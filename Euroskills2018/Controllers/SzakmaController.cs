using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Euroskills2018.Data;
using Euroskills2018.Models;

namespace Euroskills2018.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SzakmaController : ControllerBase
    {
        private readonly EuroskillsContext _context;

        public SzakmaController(EuroskillsContext context)
        {
            _context = context;
        }

        // GET: api/Szakma
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Szakmak.ToListAsync());
        }

        // GET: api/Szakma/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var szakma = await _context.Szakmak.FindAsync(id);
            if (szakma == null)
            {
                return NotFound();
            }
            return Ok(szakma);
        }

        // POST: api/Szakma
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Szakma s)
        {
            _context.Szakmak.Add(s);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = s.Id }, s);
        }

        // PUT: api/Szakma/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Szakma s)
        {
            var existing = await _context.Szakmak.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.SzakmaNev = s.SzakmaNev;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Szakma/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var szakma = await _context.Szakmak.FindAsync(id);
            if (szakma == null)
            {
                return NotFound();
            }

            _context.Szakmak.Remove(szakma);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
