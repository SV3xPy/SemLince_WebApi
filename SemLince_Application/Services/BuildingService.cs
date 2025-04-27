using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return await _buildingRepository.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _buildingRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Building>> GetAllAsync()
        {
            return await _buildingRepository.GetAllAsync();
        }

        public async Task<Building> GetByIdAsync(int id)
        {
            return await _buildingRepository.GetByIdAsync(id);
        }

        public async Task<Building> UpdateAsync(int id, Building entity)
        {
            return await _buildingRepository.UpdateAsync(id, entity);
        }
    }
}
