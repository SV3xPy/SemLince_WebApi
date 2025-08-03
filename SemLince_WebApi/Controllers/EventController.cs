using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SemLince_Application.Exceptions;
using SemLince_Application.IServices;
using SemLince_Domain.Entities;

namespace SemLince_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventService _eventService;

        public EventController(ILogger<EventController> logger, IEventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAllEventsAsync()
        {
            try
            {
                IEnumerable<Event> eventsFromService = await _eventService.GetAllAsync();
                return Ok(eventsFromService.ToList());
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
        public async Task<ActionResult<Event>> GetEventByIdAsync(int id)
        {
            try
            {
                Event eventFromService = await _eventService.GetByIdAsync(id);
                return Ok(eventFromService);
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
        public async Task<ActionResult<Event>> PostEventAsync(Event pevent)
        {
            try
            {
                Event createdEvent = await _eventService.AddAsync(pevent);
                return Ok(createdEvent);
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
        public async Task<ActionResult<Event>> UpdateBuildingAsync(int id, Event uevent)
        {
            try
            {
                Event updatedEvent = await _eventService.UpdateAsync(id, uevent);
                return Ok(updatedEvent);
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
        public async Task<ActionResult<bool>> DeleteEventAsync(int id)
        {
            try
            {
                await _eventService.DeleteAsync(id);
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
