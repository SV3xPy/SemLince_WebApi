using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SemLince_Application.Exceptions;
using SemLince_Application.IServices;
using SemLince_Domain.Entities;

namespace SemLince_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly ILogger<PersonaController> _logger;
        private readonly IPersonaService _personaService;

        public PersonaController(ILogger<PersonaController> logger, IPersonaService personaService)
        {
            _logger = logger;
            _personaService = personaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Persona>>> GetAllPersonasAsync()
        {
            try
            {
                IEnumerable<Persona> personasFromService = await _personaService.GetAllAsync();
                return Ok(personasFromService.ToList());
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Persona>> GetPersonaByIdAsync(int id)
        {
            try
            {
                Persona personaFromService = await _personaService.GetByIdAsync(id);
                return Ok(personaFromService);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersonaAsync(Persona persona)
        {
            try
            {
                var hashedPassword = new PasswordHasher<Persona>()
                    .HashPassword(persona, persona.Per_Password);

                persona.Per_Password = hashedPassword;
                Persona createdPersona = await _personaService.AddAsync(persona);
                return Ok(createdPersona);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Persona>> UpdatePersonaAsync(int id, Persona persona)
        {
            try
            {
                Persona updatedPersona = await _personaService.UpdateAsync(id,persona);
                return Ok(updatedPersona);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<bool>> DeletePersonaAsync(int id)
        {
            try
            {
                await _personaService.DeleteAsync(id);
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
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
