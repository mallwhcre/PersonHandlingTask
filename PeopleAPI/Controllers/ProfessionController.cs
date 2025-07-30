using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleAPI.Data;
using PeopleAPI.Models;

namespace PeopleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionController : ControllerBase
    {
        private readonly PeopleDbContext _context;

        public ProfessionController(PeopleDbContext context)
        {
            _context = context;
        }

        // GET: api/Profession
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfessionModel>>> GetProfessions()
        {
            return await _context.Professions.ToListAsync();
        }

        // GET: api/Profession/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfessionModel>> GetProfessionModel(int id)
        {
            var professionModel = await _context.Professions.FindAsync(id);

            if (professionModel == null)
            {
                return NotFound();
            }

            return professionModel;
        }

        // PUT: api/Profession/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessionModel(int id, ProfessionModel professionModel)
        {
            if (id != professionModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(professionModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessionModelExists(id))
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

        // POST: api/Profession
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProfessionModel>> PostProfessionModel(ProfessionModel professionModel)
        {
            _context.Professions.Add(professionModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfessionModel", new { id = professionModel.Id }, professionModel);
        }

        // DELETE: api/Profession/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfessionModel(int id)
        {
            var professionModel = await _context.Professions.FindAsync(id);
            if (professionModel == null)
            {
                return NotFound();
            }

            _context.Professions.Remove(professionModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfessionModelExists(int id)
        {
            return _context.Professions.Any(e => e.Id == id);
        }
    }
}
