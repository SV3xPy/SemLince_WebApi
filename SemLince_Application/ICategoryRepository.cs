using SemLince_Domain;

namespace SemLince_Application
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