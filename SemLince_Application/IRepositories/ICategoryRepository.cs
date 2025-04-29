using SemLince_Domain.CommonInterfaces;
using SemLince_Domain.Entities;

namespace SemLince_Application.IRepositories
{
    /* Version Inicial de la interfaz
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
        Category CreateCategory(Category category);
        bool DeleteCategory(int id);
        Category UpdateCategory(int id, Category category);
    }*/
    public interface ICategoryRepository : ICommonRepository<Category>
    {

    }
}