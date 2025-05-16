using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaAPI.Data;
using PruebaTecnicaAPI.Models;

namespace PruebaTecnicaAPI.Controllers
{
    [ApiController]
    [Route("api/machines/{machineId}/[controller]")]
    public class ComponentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComponentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/machines/1/components
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Component>>> GetAll(int machineId)
        {
            var machineExists = await _context.Machines.AnyAsync(m => m.Id == machineId);
            if (!machineExists)
                return NotFound("Máquina no encontrada.");

            return await _context.Components
                                 .Where(c => c.MachineId == machineId)
                                 .ToListAsync();
        }

        // GET: api/machines/1/components/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Component>> GetById(int machineId, int id)
        {
            var component = await _context.Components
                .FirstOrDefaultAsync(c => c.MachineId == machineId && c.Id == id);

            return component == null ? NotFound() : component;
        }

        // POST: api/machines/1/components
        [HttpPost]
        public async Task<ActionResult<Component>> Create(int machineId, Component component)
        {
            var machineExists = await _context.Machines.AnyAsync(m => m.Id == machineId);
            if (!machineExists)
                return NotFound("Máquina no encontrada.");

            component.MachineId = machineId;
            _context.Components.Add(component);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { machineId = machineId, id = component.Id }, component);
        }

        // PUT: api/machines/1/components/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int machineId, int id, Component component)
        {
            if (id != component.Id)
                return BadRequest("El ID no coincide.");

            component.MachineId = machineId;

            var exists = await _context.Machines.AnyAsync(m => m.Id == machineId);
            if (!exists)
                return NotFound("Máquina no encontrada.");

            _context.Entry(component).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Components.Any(c => c.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        // DELETE: api/machines/1/components/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int machineId, int id)
        {
            var component = await _context.Components
                .FirstOrDefaultAsync(c => c.MachineId == machineId && c.Id == id);

            if (component == null)
                return NotFound();

            _context.Components.Remove(component);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
