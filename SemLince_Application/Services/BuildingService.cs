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
    public class BuildingService : IBuildingService
    {
        public readonly IBuildingRepository _buildingRepository;

        public BuildingService(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        public async Task<Building> AddAsync(Building entity)
        {
            Building createdBuilding = await _buildingRepository.AddAsync(entity) ?? throw new ValidationException();
            return createdBuilding;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool rowsAffected = await _buildingRepository.DeleteAsync(id);
            if (!rowsAffected)
            {
                throw new NotFoundException($"El registro con id: {id}, a eliminar no se encontro." +
                    $"Favor de verificar e intentar de nuevo");
            }
            return rowsAffected;
        }

        public async Task<IEnumerable<Building>> GetAllAsync()
        {
            IEnumerable<Building> buildings = await _buildingRepository.GetAllAsync();
            if (!buildings.Any())
            {
                throw new NotFoundException("Actualmente dicho apartado no tiene registros.");
            }
            return buildings;
        }

        public async Task<Building> GetByIdAsync(int id)
        {
            Building building = await _buildingRepository.GetByIdAsync(id);
            return building ?? throw new NotFoundException($"El registro con id: {id}, no se encontro. " +
                   $"Favor de verificar e intentar de nuevo");
        }

        public async Task<Building> UpdateAsync(int id, Building entity)
        {
            Building buildingExists = await _buildingRepository.GetByIdAsync(id);
            if (buildingExists == null)
            {
                throw new NotFoundException($"El registro con id: {id}, a actualizar no se encontro. " +
                      $"Favor de verificar e intentar de nuevo");
            }
            return await _buildingRepository.UpdateAsync(id, entity);
        }
    }
}
