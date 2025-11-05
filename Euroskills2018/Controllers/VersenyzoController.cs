using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Euroskills2018.Data;
using Euroskills2018.Models;

namespace Euroskills2018.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VersenyzoController : ControllerBase
    {
        private readonly EuroskillsContext _context;

        public VersenyzoController(EuroskillsContext context)
        {
            _context = context;
        }

        // GET: api/Versenyzo
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var versenyzok = await _context.Versenyzok
                .Include(v => v.Szakma)
                .Include(v => v.Orszag)
                .ToListAsync();

            return Ok(versenyzok);
        }

        // GET: api/Versenyzo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var versenyzo = await _context.Versenyzok
                .Include(v => v.Szakma)
                .Include(v => v.Orszag)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (versenyzo == null)
            {
                return NotFound();
            }

            return Ok(versenyzo);
        }

        // POST: api/Versenyzo
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Versenyzo v)
        {
            _context.Versenyzok.Add(v);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = v.Id }, v);
        }

        // PUT: api/Versenyzo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Versenyzo v)
        {
            var existing = await _context.Versenyzok.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Nev = v.Nev;
            existing.Pont = v.Pont;
            existing.SzakmaId = v.SzakmaId;
            existing.OrszagId = v.OrszagId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Versenyzo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var versenyzo = await _context.Versenyzok.FindAsync(id);
            if (versenyzo == null)
            {
                return NotFound();
            }

            _context.Versenyzok.Remove(versenyzo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
