using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleAPI.Data;
using PeopleAPI.Models;
using PeopleAPI.Dto;
using PeopleAPI.Common;

namespace PeopleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleDbContext _context;
        private readonly IMapper _mapper;

        public PeopleController(PeopleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; 
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonViewDto>>> GetPeople()
        {
            var people = await _context.People
                .Include(p => p.Profession)
                .Include(p => p.Hobbies)
                .ToListAsync();
            
            var peopleDto = _mapper.Map<IEnumerable<PersonViewDto>>(people);
            return Ok(peopleDto);
        }

        // GET: api/People/id
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonViewDto>> GetPersonModel(int id) 
        {
            // Validate ID parameter using InputGuards
            if (!InputGuards.IsValidId(id))
            {
                return BadRequest("Invalid ID provided");
            }

            var person = await _context.People
                .Include(p => p.Profession)
                .Include(p => p.Hobbies)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            var personDto = _mapper.Map<PersonViewDto>(person);
            return Ok(personDto);
        }

        // PUT: api/People/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonModel(int id, PersonAddEditDto personDto)
        {
            // Validate ID parameter
            if (!InputGuards.IsValidId(id))
            {
                return BadRequest("Invalid ID provided");
            }

            if (id != personDto.Id)
            {
                return BadRequest("ID mismatch between route and body");
            }

            // Use ValidationHelper for comprehensive validation
            var validation = await ValidationHelper.ValidatePerson(this, personDto, _context);
            if (validation != null) return validation;

            // Prevent accidental creation of new hobby entity 
            personDto.HobbyIds.Clear();

            var existingPerson = await _context.People.FindAsync(id);
            if (existingPerson == null) return NotFound();
            
            _mapper.Map(personDto, existingPerson);

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
        public async Task<IActionResult> CreatePerson([FromBody] PersonAddEditDto personDto)
        {
            // Use ValidationHelper for comprehensive validation
            var validation = await ValidationHelper.ValidatePerson(this, personDto, _context);
            if (validation != null) return validation;

            var hobbyIds = personDto.HobbyIds
                .Where(h => h > 0)
                .ToList();

            List<HobbyModel> existingHobbies = new();

            if (hobbyIds.Any())
            {
                // Check if hobby ids exist using ResourceGuards
                existingHobbies = await _context.Hobbies
                    .Where(h => hobbyIds.Contains(h.Id))
                    .ToListAsync();
                
                if (existingHobbies.Count != hobbyIds.Count)
                {
                    var missingIds = hobbyIds.Except(existingHobbies.Select(h => h.Id));
                    return BadRequest($"The following hobby IDs do not exist: {string.Join(", ", missingIds)}");
                }
            }

            // Map DTO to model (without hobbies for now)
            var personModel = _mapper.Map<PersonModel>(personDto);
            
            // Associate hobbies with the person
            personModel.Hobbies = existingHobbies;
            
            _context.People.Add(personModel);
            await _context.SaveChangesAsync();

            // Reload the person with navigation properties for the response
            var createdPerson = await _context.People
                .Include(p => p.Profession)
                .Include(p => p.Hobbies)
                .FirstOrDefaultAsync(p => p.Id == personModel.Id);

            var createdPersonDto = _mapper.Map<PersonViewDto>(createdPerson);

            return CreatedAtAction("GetPersonModel", new { id = personModel.Id }, createdPersonDto);
        }

        // DELETE: api/People/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonModel(int id)
        {
            // Validate ID parameter
            if (!InputGuards.IsValidId(id))
            {
                return BadRequest("Invalid ID provided");
            }

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

     /*   // GET: api/People/{personId}/hobbies
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
        */
    }
}
