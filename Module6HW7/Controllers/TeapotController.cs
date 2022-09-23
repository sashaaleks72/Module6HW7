using Microsoft.AspNetCore.Mvc;
using Module6HW7.Exceptions;
using Module6HW7.Interfaces;
using Module6HW7.Models;
using Module6HW7.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Module6HW7.Controllers
{
    [Route("api")]
    [ApiController]
    public class TeapotController : ControllerBase
    {
        private readonly ITeapotService _teapotService;

        public TeapotController(ITeapotService teapotService)
        {
            _teapotService = teapotService;
        }

        [HttpGet("teapots")]
        public async Task<IActionResult> GetTeapots()
        {
            List<Teapot> teapots = null;
            
            try
            {
                teapots = await _teapotService.GetTeapots();
            }
            catch (BusinessException ex)
            {
                return NotFound(new { ex.Message });
            }

            return Ok(teapots);
        }

        [HttpGet("teapots/{id}")]
        public async Task<IActionResult> GetTeapotById([FromRoute] Guid id)
        {
            Teapot teapot = null;

            try
            {
                teapot = await _teapotService.GetTeapotById(id);
            }
            catch (BusinessException ex)
            {
                return NotFound(new { ex.Message });
            }

            return Ok(teapot);
        }

        [HttpPost("teapots")]
        public async Task<IActionResult> AddTeapot([FromBody] TeapotViewModel teapotFromBody)
        {
            await _teapotService.AddTeapot(teapotFromBody);

            return Ok(new {SuccessMessage = "Teapot has been added!"});
        }


        [HttpPut("teapots/{id}")]
        public async Task<IActionResult> EditTeapotById([FromRoute] Guid id, [FromBody] TeapotViewModel teapotFromBody)
        {
            try
            {
                await _teapotService.EditTeapotById(id, teapotFromBody);
            }
            catch (BusinessException ex)
            {
                return NotFound(new { ex.Message });
            }

            return Ok(new { SuccessMessage = "The teapot has been changed!" });
        }

        [HttpDelete("teapots/{id}")]
        public async Task<IActionResult> DeleteTeapotById([FromRoute] Guid id)
        {
            try
            {
                await _teapotService.DeleteTeapotById(id);
            }
            catch (BusinessException ex)
            {
                return NotFound(new { ex.Message });
            }

            return Ok(new { SuccessMessage = "The teapot has been removed!" });
        }
    }
}
