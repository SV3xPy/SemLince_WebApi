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
    public class EventService : IEventService
    {
        public readonly IEventRepository _eventRepository;
        public readonly ILocationRepository _locationRepository;
        public readonly ICareerRepository _careerRepository;
        public readonly ICategoryRepository _categoryRepository;

        public EventService(IEventRepository eventRepository, ILocationRepository locationRepository, ICareerRepository careerRepository, ICategoryRepository categoryRepository)
        {
            _eventRepository = eventRepository;
            _locationRepository = locationRepository;
            _careerRepository = careerRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Event> AddAsync(Event entity)
        {
            /*SE REALIZAN LAS VALIDACIONES DE LAS CLAVES FORANEAS.
             SE UTILIZA EL DESCARTE _ PORQUE LOS VALORES NO SON RELEVANTES*/
            _ = await _locationRepository.GetByIdAsync(entity.Eve_IdLocation) ?? throw new ValidationException("La Ubicacion especificada no existe.");
            _ = await _careerRepository.GetByIdAsync(entity.Eve_IdCareer) ?? throw new ValidationException("La Carrera especificada no existe.");
            _ = await _categoryRepository.GetByIdAsync(entity.Eve_IdCategory) ?? throw new ValidationException("La Categoria especificada no existe.");
            Event createdEvent = await _eventRepository.AddAsync(entity) ?? throw new Exception("Ocurrio una excepcion al insertar el registro.");
            return createdEvent;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool rowsAffected = await _eventRepository.DeleteAsync(id);
            if (!rowsAffected)
            {
                throw new NotFoundException($"El registro con id: {id}, a eliminar no se encontro," +
                    $"Favor de verificar e intentar de nuevo.");
            }
            return rowsAffected;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            IEnumerable<Event> events = await _eventRepository.GetAllAsync();
            if (!events.Any())
            {
                throw new NotFoundException("Actualmente el apartado consultado, no tiene registros.");
            }
            return events;
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            Event eventExists = await _eventRepository.GetByIdAsync(id);
            return eventExists ?? throw new NotFoundException($"El registro con id: {id}, no se encontro." +
                $"Favor de verificar e intentar de nuevo.");
        }

        public async Task<Event> UpdateAsync(int id, Event entity)
        {
            _ = await _eventRepository.GetByIdAsync(id) ?? throw new NotFoundException($"El registro con id: {id}, no se encontro." +
                $"Favor de verificar e intentar de nuevo.");
            /*SE REALIZAN LAS VALIDACIONES DE LAS CLAVES FORANEAS.
            SE UTILIZA EL DESCARTE _ PORQUE LOS VALORES NO SON RELEVANTES*/
            _ = await _locationRepository.GetByIdAsync(entity.Eve_IdLocation) ?? throw new ValidationException("La Ubicacion especificada no existe.");
            _ = await _careerRepository.GetByIdAsync(entity.Eve_IdCareer) ?? throw new ValidationException("La Carrera especificada no existe.");
            _ = await _categoryRepository.GetByIdAsync(entity.Eve_IdCategory) ?? throw new ValidationException("La Categoria especificada no existe.");
            return await _eventRepository.UpdateAsync(id, entity);
        }
    }
}
