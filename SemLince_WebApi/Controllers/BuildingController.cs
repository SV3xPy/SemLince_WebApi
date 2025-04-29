using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SemLince_Application.IServices;
using SemLince_Domain.Entities;

namespace SemLince_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuildingController : Controller
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
            IEnumerable<Building> buildingsFromService = await _buildingService.GetAllAsync();
            if (buildingsFromService.IsNullOrEmpty())
            {
                return NotFound("Actualmente dicho apartado no tiene registros.");
            }
            return Ok(buildingsFromService.ToList());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Building>> GetBuildingByIdAsync(int id)
        {
            Building buildingFromService = await _buildingService.GetByIdAsync(id);
            if (buildingFromService is null)
            {
                return NotFound($"El registro con id: {id}, no se encontro. " +
                    $"Favor de verificar e intentar de nuevo");
            }
            return Ok(buildingFromService);
        }

        [HttpPost]
        public async Task<ActionResult<Building>> PostBuildingAsync(Building building)
        {
            Building createdBuilding = await _buildingService.AddAsync(building);
            if (createdBuilding is null)
            {
                return BadRequest();
            }
            return Ok(createdBuilding);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Building>> UpdateBuildingAsync(int id, Building building)
        {
            if (await _buildingService.GetByIdAsync(id) is null)
            {
                return NotFound($"El registro con id: {id}, a actualizar no se encontro. " +
                    $"Favor de verificar e intentar de nuevo");
            }
            Building updatedBuilding = await _buildingService.UpdateAsync(id, building);
            if (updatedBuilding is null)
            {
                return BadRequest();
            }
            return Ok(updatedBuilding);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<int>> DeleteBuildingAsync(int id)
        {
            bool rowsAffected = await _buildingService.DeleteAsync(id);
            if (!rowsAffected)
            {
                return NotFound($"El registro con id: {id}, a eliminar no se encontro." +
                     $"Favor de verificar e intentar de nuevo");
            }
            return Ok($"El registro con id: {id}, fue eliminado exitosamente.");
        }
    }
}
