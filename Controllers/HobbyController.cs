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
        public async Task<ActionResult<HobbyViewDto>> GetHobby(int id)
        {
            var hobby = await _context.Hobbies.FindAsync(id);

            if (hobby == null)
            {
                return NotFound();
            }

            var hobbyDto = _mapper.Map<HobbyViewDto>(hobby);
            return Ok(hobbyDto);
        }

        // POST: api/Hobby
        [HttpPost]
        public async Task<IActionResult> AddHobby([FromBody] HobbyAddEditDto Input)
        {
            // Map DTO to model
            var hobbyModel = _mapper.Map<HobbyModel>(Input);

            _context.Hobbies.Add(hobbyModel);
            await _context.SaveChangesAsync();

            var createdHobbyDto = _mapper.Map<HobbyViewDto>(hobbyModel);
            return CreatedAtAction("AddHobby", new { id = hobbyModel.Id }, createdHobbyDto);
        }

        // PUT: api/Hobby/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditHobby(int id, HobbyAddEditDto Input)  
        {
            var existingHobby = await _context.Hobbies.FindAsync(id);
            
            if (existingHobby == null) 
                return NotFound();

            _mapper.Map(Input, existingHobby);

            await _context.SaveChangesAsync();
            
            return NoContent();
        }


        // DELETE: api/Hobby/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHobby(int id)
        {
            var hobbyModel = await _context.Hobbies.FindAsync(id);

            if (hobbyModel is null)
                return NotFound();

            _context.Hobbies.Remove(hobbyModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
