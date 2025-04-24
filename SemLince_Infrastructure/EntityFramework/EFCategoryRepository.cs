using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SemLince_Application;
using SemLince_Domain;

namespace SemLince_Infrastructure.EntityFramework
{
    // REPOSITORIO SECUNDARIO CONSIDERANDO QUE SE CAMBIA EL MANEJO DE BD
    // HACIA EL ORM EntityFramework
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly CategoryDbContext _categoryDbContext;
        public EFCategoryRepository(CategoryDbContext categoryDbContext)
        {
            _categoryDbContext = categoryDbContext;
        }
        public List<Category> GetAllCategories()
        {
            return _categoryDbContext.Categories.ToList();

        }

        public Category GetCategoryById(int id)
        {
            return _categoryDbContext.Categories.FirstOrDefault(g => g.Cat_ID == id);
        }

        public Category CreateCategory(Category category)
        {
            _categoryDbContext.Categories.Add(category);
            _categoryDbContext.SaveChanges();
            return category;
        }

        public bool DeleteCategory(int id)
        {

            //Se ocupa revisar mas a detalle
            _categoryDbContext.Categories.Where(cat => cat.Cat_ID == id).ExecuteDelete();
            _categoryDbContext.SaveChanges();
            return true;
        }

        public Category UpdateCategory(int id, Category category)
        {
            Category oldCategory = _categoryDbContext.Categories.Find(id);
            if (oldCategory is null)
            {
                return null;
            }
            oldCategory.Cat_Nombre = category.Cat_Nombre;
            _categoryDbContext.SaveChanges();
            return oldCategory;
            throw new NotImplementedException();
        }
    }
}
