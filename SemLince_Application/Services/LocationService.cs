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
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        //Building tambien es necesario debido a la relacion 1-n
        private readonly IBuildingRepository _buildingRepository;

        public LocationService(ILocationRepository locationRepository, IBuildingRepository buildingRepository)
        {
            _locationRepository = locationRepository;
            _buildingRepository = buildingRepository;
        }

        public async Task<Location> AddAsync(Location entity)
        {
            Building buildingExists = await _buildingRepository.GetByIdAsync(entity.Loc_Building);
            return buildingExists == null
            ? throw new ValidationException("El edificio especificado no existe.")
            : await _locationRepository.AddAsync(entity);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool rowsAffected = await _locationRepository.DeleteAsync(id);
            if (!rowsAffected)
            {
                throw new NotFoundException($"El registro con id: {id}, a eliminar no se encontro." +
                    $"Favor de verificar e intentar de nuevo");
            }
            return rowsAffected;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            IEnumerable<Location> locations = await _locationRepository.GetAllAsync();
            if (!locations.Any())
            {
                throw new NotFoundException("Actualmente dicho apartado no tiene registros.");
            }
            return locations;
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            Location location = await _locationRepository.GetByIdAsync(id);
            return location ?? throw new NotFoundException($"El registro con id: {id}, no se encontro. " +
                   $"Favor de verificar e intentar de nuevo");
        }

        public async Task<Location> UpdateAsync(int id, Location entity)
        {
            Location locationExists = await _locationRepository.GetByIdAsync(id);
            if (locationExists == null)
            {
                throw new NotFoundException($"El registro con id: {id}, a actualizar no se encontro. " +
                   $"Favor de verificar e intentar de nuevo");
            }
            Building buildingExists = await _buildingRepository.GetByIdAsync(entity.Loc_Building);
            if (buildingExists == null)
            {
                throw new ValidationException($"El Edificio con id: {entity.Loc_Building} especificado no existe.");
            }
            return await _locationRepository.UpdateAsync(id, entity);
        }
    }
}
