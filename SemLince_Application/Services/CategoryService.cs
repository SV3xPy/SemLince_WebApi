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
    public class CategoryService : ICategoryService
    {
        //Constructor Dependecy Injection
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        /* IMPLMENTACION ANTERIOR NO ASINCRONA
        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }
        public Category GetCategoryById(int id)
        {
            return _categoryRepository.GetCategoryById(id);
        }
        public Category CreateCategory(Category category)
        {
            return _categoryRepository.CreateCategory(category);
        }

        public bool DeleteCategory(int id)
        {
            return _categoryRepository.DeleteCategory(id);
        }

        public Category UpdateCategory(int id, Category category)
        {
            return _categoryRepository.UpdateCategory(id, category);
        }
        */

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return _categoryRepository.GetAllAsync();
        }

        public Task<Category> GetByIdAsync(int id)
        {
            return _categoryRepository.GetByIdAsync(id);
        }

        public Task<Category> AddAsync(Category entity)
        {
            return _categoryRepository.AddAsync(entity);
        }

        public Task<Category> UpdateAsync(int id, Category entity)
        {
            return _categoryRepository.UpdateAsync(id, entity);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _categoryRepository.DeleteAsync(id);
        }
    }
}
