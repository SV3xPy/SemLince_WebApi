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
    public class CategoryService : ICategoryService
    {
        //Constructor Dependecy Injection
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAllAsync();
            if (!categories.Any())
            {
                throw new NotFoundException("Actualmente dicho apartado no tiene registros.");
            }
            return categories;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            Category category = await _categoryRepository.GetByIdAsync(id);
            return category ?? throw new NotFoundException($"El registro con id: {id}, no se encontro. " +
                   $"Favor de verificar e intentar de nuevo");
        }

        public async Task<Category> AddAsync(Category entity)
        {
            Category createdCategory = await _categoryRepository.AddAsync(entity) ?? throw new ValidationException();
            return createdCategory;
        }

        public async Task<Category> UpdateAsync(int id, Category entity)
        {
            Category categoryExists = await _categoryRepository.GetByIdAsync(id);
            if (categoryExists == null)
            {
                throw new NotFoundException($"El registro con id: {id}, a actualizar no se encontro. " +
                  $"Favor de verificar e intentar de nuevo");
            }
            return await _categoryRepository.UpdateAsync(id, entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool rowsAffected = await _categoryRepository.DeleteAsync(id);
            if (!rowsAffected)
            {
                throw new NotFoundException($"El registro con id: {id}, a eliminar no se encontro." +
                    $"Favor de verificar e intentar de nuevo");
            }
            return rowsAffected;
        }
    }
}
