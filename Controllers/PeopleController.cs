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
            var person = await _context.People
                .Include(p => p.Profession)
                .Include(p => p.Hobbies)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person is null) return NotFound();

            var personDto = _mapper.Map<PersonViewDto>(person);
            return Ok(personDto);
        }


        // POST: api/People
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PersonAddEditDto Input)
        {
            if (Input.ProfessionId == 0) Input.ProfessionId = null; // Handle ProfessionId=0 as null

            var hobbyIds = Input.HobbyIds
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

            var personModel = _mapper.Map<PersonModel>(Input);
            
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
        // PUT: api/People/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonModel(int id, PersonAddEditDto Input)

        {

            if (existingPerson is null) return NotFound();

            if (Input.ProfessionId == 0) Input.ProfessionId = null; // Handle ProfessionId=0 as null
            
            var hobbyIds = Input.HobbyIds
                .Where(h => h > 0)
                .ToList();

            List<HobbyModel> existingHobbies = new();
            
            
            var existingPerson = await _context.People
                //.Include(p => p.Hobbies) 
                .FirstOrDefaultAsync(p => p.Id == id);


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

            

            _mapper.Map(Input, existingPerson);
            existingPerson.Hobbies = existingHobbies;

            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        // DELETE: api/People/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonModel(int id)
        {
      
            var personModel = await _context.People.FindAsync(id);
            if (personModel is null) return NotFound();

            _context.People.Remove(personModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonModelExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }

        // DELETE: api/People/{personId}/hobbies/{hobbyId}
        [HttpDelete("{personId}/hobbies/{hobbyId}")]
        public async Task<IActionResult> RemoveHobbyFromPerson(int personId, int hobbyId)
        {
            var person = await _context.People.Include(p => p.Hobbies).FirstOrDefaultAsync(p => p.Id == personId);
            if (person is null)
            {
                return NotFound("Person not found");
            }

            var hobby = person.Hobbies.FirstOrDefault(h => h.Id == hobbyId);
            if (hobby is null)
            {
                return NotFound("Person doesn't have this hobby");
            }

            person.Hobbies.Remove(hobby);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
