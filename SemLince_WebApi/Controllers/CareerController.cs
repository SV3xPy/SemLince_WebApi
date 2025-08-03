using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SemLince_Application.Exceptions;
using SemLince_Application.IServices;
using SemLince_Domain.Entities;

namespace SemLince_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CareerController : ControllerBase
    {
        private readonly ILogger<CareerController> _logger;
        private readonly ICareerService _careersService;

        public CareerController(ILogger<CareerController> logger, ICareerService careersService)
        {
            _logger = logger;
            _careersService = careersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Career>>> GetAllAsync()
        {
            try
            {
                IEnumerable<Career> careersFromService = await _careersService.GetAllAsync();
                return Ok(careersFromService.ToList());
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Career>> GetByIdAsync(int id)
        {
            try
            {
                Career careerFromService = await _careersService.GetByIdAsync(id);
                return Ok(careerFromService);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Career>> PostCareerAsync(Career career)
        {
            try
            {
                Career createdCategory = await _careersService.AddAsync(career);
                return Ok(createdCategory);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Career>> UpdateCareerAsync(int id, Career career)
        {
            try
            {
                Career updatedCareer = await _careersService.UpdateAsync(id, career);
                return Ok(updatedCareer);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<int>> DeleteAsync(int id)
        {
            try
            {
                bool rowsAffected = await _careersService.DeleteAsync(id);
                return Ok($"El registro con id: {id}, fue eliminado exitosamente.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
