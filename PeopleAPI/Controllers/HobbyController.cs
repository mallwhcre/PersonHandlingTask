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
    public class HobbyController : ControllerBase
    {
        private readonly PeopleDbContext _context;

        public HobbyController(PeopleDbContext context)
        {
            _context = context;
        }

        // GET: api/Hobby
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HobbyModel>>> GetHobbies()
        {
            return await _context.Hobbies.ToListAsync();
        }

        // GET: api/Hobby/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HobbyModel>> GetHobbyModel(int id)
        {
            var hobbyModel = await _context.Hobbies.FindAsync(id);

            if (hobbyModel == null)
            {
                return NotFound();
            }

            return hobbyModel;
        }

        // PUT: api/Hobby/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHobbyModel(int id, HobbyModel hobbyModel)
        {
            if (id != hobbyModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(hobbyModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HobbyModelExists(id))
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

        // POST: api/Hobby
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HobbyModel>> PostHobbyModel(HobbyModel hobbyModel)
        {
            _context.Hobbies.Add(hobbyModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHobbyModel", new { id = hobbyModel.Id }, hobbyModel);
        }

        // DELETE: api/Hobby/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHobbyModel(int id)
        {
            var hobbyModel = await _context.Hobbies.FindAsync(id);
            if (hobbyModel == null)
            {
                return NotFound();
            }

            _context.Hobbies.Remove(hobbyModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HobbyModelExists(int id)
        {
            return _context.Hobbies.Any(e => e.Id == id);
        }
    }
}
