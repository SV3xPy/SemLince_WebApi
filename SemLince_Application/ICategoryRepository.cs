using SemLince_Domain;

namespace SemLince_Application
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
    }
}