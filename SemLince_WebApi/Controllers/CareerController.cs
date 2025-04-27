using Microsoft.AspNetCore.Mvc;
using SemLince_Application.IServices;
using SemLince_Domain.Entities;

namespace SemLince_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CareerController : Controller
    {
        private readonly ILogger<CareerController> _logger;
        private ICareerService _careersService;

        public CareerController(ILogger<CareerController> logger, ICareerService careersService)
        {
            _logger = logger;
            _careersService = careersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Career>>> GetAllAsync()
        {
            IEnumerable<Career> careersFromService = await _careersService.GetAllAsync();
            return Ok(careersFromService.ToList());
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Career>> GetByIdAsync(int id)
        {
            Career careerFromService = await _careersService.GetByIdAsync(id);
            if (careerFromService == null)
            {
                return NotFound();
            }
            return Ok(careerFromService);
        }

        [HttpPost]
        public async Task<ActionResult<Career>> PostCareerAsync(Career career)
        {
            Career createdCategory = await _careersService.AddAsync(career);
            if (createdCategory is null)
            {
                return BadRequest();
            }
            return Ok(createdCategory);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<Career>> UpdateCareerAsync(int id, Career career)
        {
            Career updatedCareer = await _careersService.UpdateAsync(id, career);
            if (updatedCareer is null)
            {
                return NotFound();
            }
            return Ok(updatedCareer);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Career>> DeleteAsync(int id) {
            bool rowsAffected = await _careersService.DeleteAsync(id);
            if (rowsAffected) {
                return Ok(id);
            }
            return NotFound();
        }
    }
}
