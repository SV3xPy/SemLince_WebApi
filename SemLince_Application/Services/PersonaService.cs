using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemLince_Application.Exceptions;
using SemLince_Application.IRepositories;
using SemLince_Application.IServices;
using SemLince_Domain.Entities;

namespace SemLince_Application.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly ICareerRepository _careerRepository;

        public PersonaService(IPersonaRepository personaRepository, ICareerRepository careerRepository)
        {
            _personaRepository = personaRepository;
            _careerRepository = careerRepository;
        }

        public async Task<Persona> AddAsync(Persona entity)
        {
            //Validaciones de llaves foraneas
            _ = await _careerRepository.GetByIdAsync(entity.Per_Carrer) ?? throw new ValidationException("La Carrera especificada no existe.");
            Persona createdPersona = await _personaRepository.AddAsync(entity) ?? throw new Exception("Ocurrio una excepcion al insertar el registro."); ;
            return createdPersona;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool rowsAffected = await _personaRepository.DeleteAsync(id);
            if (!rowsAffected)
            {
                throw new NotFoundException($"El registro con id: {id}, a eliminar no se encontro," +
                       $"Favor de verificar e intentar de nuevo.");
            }
            return rowsAffected;
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            IEnumerable<Persona> personas = await _personaRepository.GetAllAsync();
            if (!personas.Any())
            {
                throw new NotFoundException("Actualmente el apartado consultado, no tiene registros.");
            }
            return personas;
        }

        public async Task<Persona> GetByIdAsync(int id)
        {
            Persona persona = await _personaRepository.GetByIdAsync(id);
            return persona ?? throw new NotFoundException($"El registro con id: {id}, no se encontro." +
                $"Favor de verificar e intentar de nuevo.");
        }

        public Task<Persona> LoginAsync(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public async Task<Persona> UpdateAsync(int id, Persona entity)
        {
            _ = await _personaRepository.GetByIdAsync(id) ?? throw new NotFoundException($"El registro con id: {id}, no se encontro." +
                $"Favor de verificar e intentar de nuevo.");

            //Se realizan las validaciones de las claves foraneas
            _ = await _careerRepository.GetByIdAsync(entity.Per_Carrer) ?? throw new ValidationException("La Carrera especificada no existe.");
            return await _personaRepository.UpdateAsync(id, entity);
        }
    }
}
