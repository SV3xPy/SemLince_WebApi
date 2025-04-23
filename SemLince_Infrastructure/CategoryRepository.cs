using SemLince_Application;
using SemLince_Domain;

namespace SemLince_Infrastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        public static List<Category> categories = new List<Category>()
        {
            // de prueba antes de conectarse a dB
            new Category {Id = 1, Name= "Curso"},
            new Category {Id = 2, Name = "Taller"},
            new Category {Id = 3, Name = "Torneo"}
        };
        public List<Category> GetAllCategories()
        {
            return categories;
        }

        public Category GetCategoryById(int id)
        {
            return categories[id];
        }
    }
}
