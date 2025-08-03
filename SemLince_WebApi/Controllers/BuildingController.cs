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
    public class BuildingController : ControllerBase
    {
        private readonly ILogger<BuildingController> _logger;
        private readonly IBuildingService _buildingService;

        public BuildingController(ILogger<BuildingController> logger, IBuildingService buildingService)
        {
            _logger = logger;
            _buildingService = buildingService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Building>>> GetAllBuildingsAsync()
        {
            try
            {
                IEnumerable<Building> buildingsFromService = await _buildingService.GetAllAsync();
                return Ok(buildingsFromService.ToList());
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
        public async Task<ActionResult<Building>> GetBuildingByIdAsync(int id)
        {
            try
            {
                Building buildingFromService = await _buildingService.GetByIdAsync(id);
                return Ok(buildingFromService);
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
        public async Task<ActionResult<Building>> PostBuildingAsync(Building building)
        {
            try
            {
                Building createdBuilding = await _buildingService.AddAsync(building);
                return Ok(createdBuilding);
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
        public async Task<ActionResult<Building>> UpdateBuildingAsync(int id, Building building)
        {
            try
            {
                Building updatedBuilding = await _buildingService.UpdateAsync(id, building);
                return Ok(updatedBuilding);
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
        public async Task<ActionResult<bool>> DeleteBuildingAsync(int id)
        {
            try
            {
                await _buildingService.DeleteAsync(id);
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
