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
    public class PlayerController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public PlayerController(ApplicationContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var playersDto = await _context.Players.Include(x => x.Team).Select(p => _mapper.Map<GetPlayersDto>(p)).ToListAsync();

            return Ok(playersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id <= 0)return BadRequest("Invalid id");

            var player = await _context.Players.Include(x => x.Team).FirstOrDefaultAsync(p => p.Id == id);

            if(player == null)return NotFound("Player not found");
            
            var playerDto = _mapper.Map<GetPlayersDto>(player);

            return Ok(playerDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostPlayerDto playerDto)
        {
            if(playerDto == null)return BadRequest("Invalid player");

            var player = _mapper.Map<Player>(playerDto);

            await _context.Players.AddAsync(player);
            
            if(await _context.SaveChangesAsync() > 0)return CreatedAtAction(nameof(Get), new {id = player.Id}, player);
            return BadRequest("could not create player");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PostPlayerDto playerDto)
        {
            if(id != playerDto.Id)return BadRequest("The ids are not the same");

            var player = await _context.Players.FindAsync(id);

            if(player == null)NotFound("Player to update not found");

            _mapper.Map(playerDto, player);

            if(await _context.SaveChangesAsync() > 0)return Ok(player);
            return BadRequest("Player could not be edited");
        }   

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id <= 0)return BadRequest("Invalid id");

            var player = await _context.Players.FindAsync(id);

            if(player == null)return NotFound("Player to delete not found");

            _context.Players.Remove(player);

            if(await _context.SaveChangesAsync() > 0)return Ok("player was deleted");
            return BadRequest("Could not delete the player");
        }
    }
}