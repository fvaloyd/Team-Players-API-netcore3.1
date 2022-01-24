using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionTablas.Data;
using RelacionTablas.Dtos;
using RelacionTablas.Models;

namespace RelacionTablas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public TeamController(ApplicationContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var teamsDto = await _context.Teams.Include(x => x.Players).Select(t => _mapper.Map<GetTeamsDto>(t)).ToListAsync();

            return Ok(teamsDto);       
        }
            
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id <= 0)return BadRequest("Invalid id");

            var team = await _context.Teams.Include(x => x.Players).FirstOrDefaultAsync(t => t.Id == id);

            if(team == null)return NotFound("Team not found");

            var teamDto = _mapper.Map<GetTeamsDto>(team);
            
            return Ok(teamDto);            
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostTeamDto teamDto)
        {
            if(teamDto == null)return BadRequest("Invalid team");

            var team = _mapper.Map<Team>(teamDto);

            await _context.Teams.AddAsync(team);

            if(await _context.SaveChangesAsync() > 0)return CreatedAtAction(nameof(Get), new {id = team.Id}, team);

            return BadRequest("The team could be not created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PostTeamDto teamDto)
        {
            if(id != teamDto.Id)return BadRequest("Ids are not the same");

            var team = await _context.Teams.FindAsync(id);

            if(teamDto == null) return BadRequest("Invalid team");

            _mapper.Map(teamDto, team);

            if(await _context.SaveChangesAsync() > 0)return Ok(team.Name);

            return BadRequest("The team could be not edited");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id <= 0)return BadRequest("Invalid id");

            var team = await _context.Teams.Include(x => x.Players).FirstOrDefaultAsync(x => x.Id == id);

            if(team == null)return NotFound("The team not found");
            
            _context.RemoveRange(team.Players);
            _context.Remove(team);

            if(await _context.SaveChangesAsync() > 0)return Ok("The team was removed from the db");

            return BadRequest("The team could be not deleted");
        }
    }
}