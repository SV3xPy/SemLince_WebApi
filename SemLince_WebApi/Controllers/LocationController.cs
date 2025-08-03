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
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly ILocationService _locationService;

        public LocationController(ILogger<LocationController> logger, ILocationService locationService)
        {
            _logger = logger;
            _locationService = locationService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Location>>> GetAllLocationsAsync()
        {
            try
            {
                IEnumerable<Location> locationsFromService = await _locationService.GetAllAsync();
                return Ok(locationsFromService.ToList());
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
        public async Task<ActionResult<Location>> GetLocationByIdAsync(int id)
        {
            try
            {
                Location locationFromService = await _locationService.GetByIdAsync(id);
                return Ok(locationFromService);
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
        public async Task<ActionResult<Location>> PostLocationAsync(Location location)
        {
            try
            {
                Location createdLocation = await _locationService.AddAsync(location);
                return Ok(createdLocation);
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
        public async Task<ActionResult<Location>> UpdateLocationAsync(int id, Location location)
        {
            try
            {
                Location updatedLocation = await _locationService.UpdateAsync(id, location);
                return Ok(updatedLocation);
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
        public async Task<ActionResult<bool>> DeleteLocationAsync(int id)
        {
            try
            {
                bool rowsAffected = await _locationService.DeleteAsync(id);
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
