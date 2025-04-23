using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemLince_Domain;

namespace SemLince_Application
{
    public class CategoryService : ICategoryService
    {
        //Constructor Dependecy Injection
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }
        public Category GetCategoryById(int id) { 
            return _categoryRepository.GetCategoryById(id);
        }
    }
}
