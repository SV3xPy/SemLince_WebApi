using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SemLince_Application.IRepositories;
using SemLince_Application.IServices;
using SemLince_Domain.Entities;

namespace SemLince_Application.Services
{
    public class CareerService : ICareerService
    {
        private readonly ICareerRepository _careerRepository;

        public CareerService(ICareerRepository careerRepository)
        {
            _careerRepository = careerRepository;
        }
        public async Task<Career> AddAsync(Career entity)
        {
            return await _careerRepository.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _careerRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Career>> GetAllAsync()
        {
            return await _careerRepository.GetAllAsync();
        }

        public async Task<Career> GetByIdAsync(int id)
        {
            return await _careerRepository.GetByIdAsync(id);
        }

        public async Task<Career> UpdateAsync(int id, Career entity)
        {
            return await _careerRepository.UpdateAsync(id, entity);
        }
    }
}
