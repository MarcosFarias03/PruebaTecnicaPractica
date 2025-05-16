using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaAPI.Data;
using PruebaTecnicaAPI.Models;

namespace PruebaTecnicaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachinesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MachinesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/machines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Machine>>> GetAll()
        {
            return await _context.Machines
                                 .Include(m => m.Components)
                                 .ToListAsync();
        }

        // GET: api/machines/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Machine>> GetById(int id)
        {
            var machine = await _context.Machines
                                        .Include(m => m.Components)
                                        .FirstOrDefaultAsync(m => m.Id == id);

            return machine == null ? NotFound() : machine;
        }

        // POST: api/machines
        [HttpPost]
        public async Task<ActionResult<Machine>> Create(Machine machine)
        {
            _context.Machines.Add(machine);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = machine.Id }, machine);
        }

        // PUT: api/machines/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Machine machine)
        {
            if (id != machine.Id)
                return BadRequest();

            _context.Entry(machine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Machines.Any(m => m.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/machines/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
                return NotFound();

            bool tieneComponentes = await _context.Components.AnyAsync(c => c.MachineId == id);
            if (tieneComponentes)
                return BadRequest("No se puede eliminar una máquina con componentes asociados.");

            _context.Machines.Remove(machine);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
