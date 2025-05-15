using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaAPI.Data;
using PruebaTecnicaAPI.Models;

namespace PruebaTecnicaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LubricantesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LubricantesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/lubricantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lubricante>>> GetAll()
        {
            return await _context.Lubricantes
                                 .Include(l => l.Proveedor)
                                 .ToListAsync();
        }

        // GET: api/lubricantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lubricante>> GetById(int id)
        {
            var lubricante = await _context.Lubricantes
                                           .Include(l => l.Proveedor)
                                           .FirstOrDefaultAsync(l => l.Id == id);

            return lubricante == null ? NotFound() : lubricante;
        }

        // POST: api/lubricantes
        [HttpPost]
        public async Task<ActionResult<Lubricante>> Create(Lubricante lubricante)
        {
            // Verificamos que el proveedor exista
            var proveedorExiste = await _context.Proveedores.AnyAsync(p => p.Id == lubricante.ProveedorId);
            if (!proveedorExiste)
                return BadRequest("ProveedorId no válido.");

            _context.Lubricantes.Add(lubricante);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = lubricante.Id }, lubricante);
        }

        // PUT: api/lubricantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Lubricante lubricante)
        {
            if (id != lubricante.Id)
                return BadRequest();

            // Verificamos que el proveedor exista
            var proveedorExiste = await _context.Proveedores.AnyAsync(p => p.Id == lubricante.ProveedorId);
            if (!proveedorExiste)
                return BadRequest("ProveedorId no válido.");

            _context.Entry(lubricante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Lubricantes.Any(l => l.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/lubricantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var lubricante = await _context.Lubricantes.FindAsync(id);
            if (lubricante == null)
                return NotFound();

            _context.Lubricantes.Remove(lubricante);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
