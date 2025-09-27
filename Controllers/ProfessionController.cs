using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleAPI.Data;
using PeopleAPI.Models;
using AutoMapper;
using PeopleAPI.Dto;
using PeopleAPI.Common;

namespace PeopleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionController : ControllerBase
    {
        private readonly PeopleDbContext _context;
        private readonly IMapper _mapper;

        public ProfessionController(PeopleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        // GET: api/Profession
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfessionViewDto>>> GetProfessions()
        {
            var professions = await _context.Professions.ToListAsync();
            var professionsDto = _mapper.Map<IEnumerable<ProfessionViewDto>>(professions);
            return Ok(professionsDto);
        }

        // GET: api/Profession/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfessionViewDto>> GetProfessionModel(int id)
        {
            var profession = await _context.Professions.FindAsync(id);

            if (profession is null)
            {
                return NotFound();
            }

            var professionDto = _mapper.Map<ProfessionViewDto>(profession);
            return Ok(professionDto);
        }

        // PUT: api/Profession/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessionModel(int id, ProfessionAddEditDto Input)
        {
    
            var existingProfession = await _context.Professions.FindAsync(id);
            if (existingProfession is null) return NotFound();

            _mapper.Map(Input, existingProfession);

            return NoContent();
        }

        // POST: api/Profession
        [HttpPost]
        public async Task<IActionResult> PostProfessionModel([FromBody] ProfessionAddEditDto professionDto)
        {
            // Map DTO to model
            var professionModel = _mapper.Map<ProfessionModel>(professionDto);
            
            _context.Professions.Add(professionModel);
            await _context.SaveChangesAsync();

            var createdProfessionDto = _mapper.Map<ProfessionViewDto>(professionModel);
            return CreatedAtAction("GetProfessionModel", new { id = professionModel.Id }, createdProfessionDto);
        }

        // DELETE: api/Profession/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfessionModel(int id)
        {
            var professionModel = await _context.Professions.FindAsync(id);
            if (professionModel is null) return NotFound();


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
