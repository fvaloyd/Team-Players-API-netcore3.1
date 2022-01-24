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
using RelacionTablas.Repository.Interfaces;

namespace RelacionTablas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly IPlayerRepository _repo;
        public PlayerController(ApplicationContext context, IMapper mapper, IPlayerRepository repo)
        {
            _mapper = mapper;
            _context = context;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var playersDto = await _repo.GetPlayersDtoAsync();

            return Ok(playersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id <= 0)return BadRequest("Invalid id");
            
            var playerDto = await _repo.GetPlayerDtoByIdAsync(id);

            return Ok(playerDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostPlayerDto playerDto)
        {
            if(playerDto == null)return BadRequest("Invalid player");
            
            if(await _repo.CreatePlayerAsync(playerDto) > 0)return CreatedAtAction(nameof(Get), new {id = playerDto.Id}, playerDto);
            return BadRequest("could not create player");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PostPlayerDto playerDto)
        {
            if(id != playerDto.Id)return BadRequest("The ids are not the same");

            if(await _repo.EditPlayerAsync(id, playerDto) > 0)return Ok(playerDto);

            return BadRequest("Player could not be edited");
        }   

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id <= 0)return BadRequest("Invalid id");

            if(await _repo.DeletePlayerAsync(id) > 0)return Ok("player was deleted");
            
            return BadRequest("Could not delete the player");
        }
    }
}