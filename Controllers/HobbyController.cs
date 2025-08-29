using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleAPI.Data;
using PeopleAPI.Models;
using PeopleAPI.Dto;
using AutoMapper;
using PeopleAPI.Common;

namespace PeopleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbyController : ControllerBase
    {
        private readonly PeopleDbContext _context;
        private readonly IMapper _mapper;

        public HobbyController(PeopleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Hobby
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HobbyViewDto>>> GetHobbies()
        {
            var hobbies = await _context.Hobbies.ToListAsync();
            var hobbiesDto = _mapper.Map<IEnumerable<HobbyViewDto>>(hobbies);
            return Ok(hobbiesDto);
        }

        // GET: api/Hobby/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HobbyViewDto>> GetHobbyModel(int id)
        {
            // Validate ID parameter using InputGuards
            if (!InputGuards.IsValidId(id))
            {
                return BadRequest("Invalid ID provided");
            }

            var hobby = await _context.Hobbies.FindAsync(id);

            if (hobby == null)
            {
                return NotFound();
            }

            var hobbyDto = _mapper.Map<HobbyViewDto>(hobby);
            return Ok(hobbyDto);
        }

        // PUT: api/Hobby/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHobbyModel(int id, HobbyAddEditDto hobbyDto)
        {
            // Validate ID parameter
            if (!InputGuards.IsValidId(id))
            {
                return BadRequest("Invalid ID provided");
            }

            if (id != hobbyDto.Id)
            {
                return BadRequest("ID mismatch between route and body");
            }

            //Validate with ValidationHelper
            var validation = ValidationHelper.ValidateHobby(this, hobbyDto);
            if (validation != null) return validation;

            var existingHobby = await _context.Hobbies.FindAsync(id);
            if (existingHobby == null) return NotFound();

            _mapper.Map(hobbyDto, existingHobby);

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
        [HttpPost]
        public async Task<IActionResult> PostHobbyModel([FromBody] HobbyAddEditDto hobbyDto)
        {
            var validation = ValidationHelper.ValidateHobby(this, hobbyDto);
            if (validation != null) return validation;

            // Map DTO to model
            var hobbyModel = _mapper.Map<HobbyModel>(hobbyDto);
            
            _context.Hobbies.Add(hobbyModel);
            await _context.SaveChangesAsync();

            var createdHobbyDto = _mapper.Map<HobbyViewDto>(hobbyModel);
            return CreatedAtAction("GetHobbyModel", new { id = hobbyModel.Id }, createdHobbyDto);
        }

        // DELETE: api/Hobby/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHobbyModel(int id)
        {
            // Validate ID parameter
            if (!InputGuards.IsValidId(id))
            {
                return BadRequest("Invalid ID provided");
            }

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
