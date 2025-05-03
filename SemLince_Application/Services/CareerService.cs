using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SemLince_Application.Exceptions;
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
            Career career = await _careerRepository.AddAsync(entity) ?? throw new ValidationException();
            return career;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool rowsAffected = await _careerRepository.DeleteAsync(id);
            if (!rowsAffected)
            {
                throw new NotFoundException($"El registro con id: {id}, a eliminar no se encontro." +
                    $"Favor de verificar e intentar de nuevo");
            }
            return rowsAffected;
        }

        public async Task<IEnumerable<Career>> GetAllAsync()
        {
            IEnumerable<Career> careers = await _careerRepository.GetAllAsync();
            if (!careers.Any())
            {
                throw new NotFoundException("Actualmente dicho apartado no tiene registros.");
            }
            return careers;
        }

        public async Task<Career> GetByIdAsync(int id)
        {
            Career career = await _careerRepository.GetByIdAsync(id);
            return career ?? throw new NotFoundException($"El registro con id: {id}, no se encontro. " +
                   $"Favor de verificar e intentar de nuevo");
        }

        public async Task<Career> UpdateAsync(int id, Career entity)
        {
            Career careerExists = await _careerRepository.GetByIdAsync(id);
            if (careerExists == null)
            {
                throw new NotFoundException($"El registro con id: {id}, a actualizar no se encontro. " +
                 $"Favor de verificar e intentar de nuevo");
            }
            return await _careerRepository.UpdateAsync(id, entity);
        }
    }
}
