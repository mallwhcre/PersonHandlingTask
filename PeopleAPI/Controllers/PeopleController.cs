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
    public class PeopleController : ControllerBase
    {
        private readonly PeopleDbContext _context;

        public PeopleController(PeopleDbContext context)
        {
            _context = context;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonModel>>> GetPeople()
        {
            return await _context.People.Include(p => p.Hobbies).ToListAsync();
        }

        // GET: api/People/id
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonModel>> GetPersonModel(int id)
        {
            var personModel = await _context.People.Include(p => p.Hobbies).FirstOrDefaultAsync(p => p.Id == id);

            if (personModel == null)
            {
                return NotFound();
            }

            return personModel;
        }

        // PUT: api/People/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonModel(int id, PersonModel personModel)
        {
            if (id != personModel.Id)
            {
                return BadRequest();
            }

            // Check if professionID exists
            if (personModel.ProfessionId.HasValue)
            {
                var professionExists = await _context.Professions.AnyAsync(p => p.Id == personModel.ProfessionId.Value);
                if (!professionExists)
                {
                    return BadRequest($"Profession with ID {personModel.ProfessionId} does not exist.");
                }
            }


            //prevent accidental creation of new hobby entity 
            personModel.Hobbies.Clear();

            _context.Entry(personModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonModelExists(id))
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

        // POST: api/People
        [HttpPost]
        public async Task<ActionResult<PersonModel>> PostPersonModel(PersonModel personModel)
        {
            
            if (personModel.ProfessionId.HasValue)
            {
                var professionExists = await _context.Professions.AnyAsync(p => p.Id == personModel.ProfessionId.Value);
                if (!professionExists)
                {
                    return BadRequest($"Profession with ID {personModel.ProfessionId} does not exist.");
                }
            }
            
            var hobbyIds = personModel.Hobbies
                .Where(h => h.Id > 0)
                .Select(h => h.Id)
                .ToList();

            personModel.Hobbies.Clear();
            
            if (hobbyIds.Any())
            {
                //check if hobby ids exist
                var existingHobbies = await _context.Hobbies
                    .Where(h => hobbyIds.Contains(h.Id))
                    .ToListAsync();
                
                if (existingHobbies.Count != hobbyIds.Count)
                {
                    var missingIds = hobbyIds.Except(existingHobbies.Select(h => h.Id));
                    return BadRequest($"The following hobby IDs do not exist: {string.Join(", ", missingIds)}");
                }
                
                //add existing hobbies to person
                personModel.Hobbies.AddRange(existingHobbies);
            }

            _context.People.Add(personModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonModel", new { id = personModel.Id }, personModel);
        }

        // DELETE: api/People/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonModel(int id)
        {
            var personModel = await _context.People.FindAsync(id);
            if (personModel == null)
            {
                return NotFound();
            }

            _context.People.Remove(personModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonModelExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }

      /*  // POST: api/People/{personId}/hobbies/{hobbyId}
        [HttpPost("{personId}/hobbies/{hobbyId}")]
        public async Task<IActionResult> AddHobbyToPerson(int personId, int hobbyId)
        {
            var person = await _context.People.Include(p => p.Hobbies).FirstOrDefaultAsync(p => p.Id == personId);
            if (person == null)
            {
                return NotFound("Person not found");
            }

            var hobby = await _context.Hobbies.FindAsync(hobbyId);
            if (hobby == null)
            {
                return NotFound("Hobby not found");
            }

            if (person.Hobbies.Any(h => h.Id == hobbyId))
            {
                return BadRequest("Person already has this hobby");
            }

            person.Hobbies.Add(hobby);
            await _context.SaveChangesAsync();

            return Ok();
        }
        */
        // DELETE: api/People/{personId}/hobbies/{hobbyId}
        [HttpDelete("{personId}/hobbies/{hobbyId}")]
        public async Task<IActionResult> RemoveHobbyFromPerson(int personId, int hobbyId)
        {
            var person = await _context.People.Include(p => p.Hobbies).FirstOrDefaultAsync(p => p.Id == personId);
            if (person == null)
            {
                return NotFound("Person not found");
            }

            var hobby = person.Hobbies.FirstOrDefault(h => h.Id == hobbyId);
            if (hobby == null)
            {
                return NotFound("Person doesn't have this hobby");
            }

            person.Hobbies.Remove(hobby);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET: api/People/{personId}/hobbies
        [HttpGet("{personId}/hobbies")]
        public async Task<ActionResult<IEnumerable<HobbyModel>>> GetPersonHobbies(int personId)
        {
            var person = await _context.People.Include(p => p.Hobbies).FirstOrDefaultAsync(p => p.Id == personId);
            if (person == null)
            {
                return NotFound("Person not found");
            }

            return Ok(person.Hobbies);
        }
    }
}
