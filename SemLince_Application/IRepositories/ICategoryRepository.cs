using SemLince_Domain.Entities;

namespace SemLince_Application.IRepositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
        Category CreateCategory(Category category);
        bool DeleteCategory(int id);
        Category UpdateCategory(int id, Category category);
    }
}