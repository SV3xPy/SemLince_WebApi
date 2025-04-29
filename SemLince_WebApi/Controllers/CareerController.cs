using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SemLince_Application.IServices;
using SemLince_Domain.Entities;

namespace SemLince_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CareerController : Controller
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
            IEnumerable<Career> careersFromService = await _careersService.GetAllAsync();
            if (careersFromService.IsNullOrEmpty())
            {
                return NotFound("Actualmente dicho apartado no tiene registros.");
            }
            return Ok(careersFromService.ToList());
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Career>> GetByIdAsync(int id)
        {
            Career careerFromService = await _careersService.GetByIdAsync(id);
            if (careerFromService == null)
            {
                return NotFound($"El registro con id: {id}, no se encontro. " +
                   $"Favor de verificar e intentar de nuevo");
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

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Career>> UpdateCareerAsync(int id, Career career)
        {
            if (await _careersService.GetByIdAsync(id) is null)
            {
                return NotFound($"El registro con id: {id}, a actualizar no se encontro. " +
                    $"Favor de verificar e intentar de nuevo");
            }
            Career updatedCareer = await _careersService.UpdateAsync(id, career);
            if (updatedCareer is null)
            {
                return BadRequest();
            }
            return Ok(updatedCareer);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<int>> DeleteAsync(int id)
        {
            bool rowsAffected = await _careersService.DeleteAsync(id);
            if (!rowsAffected)
            {
                return NotFound($"El registro con id: {id}, a eliminar no se encontro." +
                    $"Favor de verificar e intentar de nuevo");

            }
            return Ok($"El registro con id: {id}, fue eliminado exitosamente.");
        }
    }
}
