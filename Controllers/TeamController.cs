using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelacionTablas.Data;
using RelacionTablas.Dtos;
using RelacionTablas.Models;
using RelacionTablas.Repository.Interfaces;

namespace RelacionTablas.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _repo;
        public TeamController(ITeamRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var teamsDto = await _repo.GetTeamsDtoAsync();

            return Ok(teamsDto);       
        }
            
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id <= 0)return BadRequest("Invalid id");

            var teamDto = await _repo.GetTeamByIdAsync(id);
            
            return Ok(teamDto);            
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostTeamDto teamDto)
        {
            if(teamDto == null)return BadRequest("Invalid team");

            if(await _repo.CreateTeamAsync(teamDto) > 0)return CreatedAtAction(nameof(Get), new {id = teamDto.Id}, teamDto);

            return BadRequest("The team could be not created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PostTeamDto teamDto)
        {
            if(id != teamDto.Id) return BadRequest("Ids are not the same");
            if(teamDto == null) return BadRequest("Invalid team");

            if(await _repo.EditTeamAsync(id, teamDto) > 0)return Ok(teamDto.Name);

            return BadRequest("The team could be not edited");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id <= 0)return BadRequest("Invalid id");

            if(await _repo.DeleteTeamAsync(id) > 0)return Ok("The team was removed from the db");

            return BadRequest("The team could be not deleted");
        }
    }
}