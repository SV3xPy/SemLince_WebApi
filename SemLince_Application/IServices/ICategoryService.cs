using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SemLince_Domain.CommonInterfaces;
using SemLince_Domain.Entities;

namespace SemLince_Application.IServices
{
    //Esto es un caso de uso

    /*
     * SERVICIO ANTERIOR SIN el ICommonService
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);

        Category CreateCategory(Category category);

        bool DeleteCategory(int id);

        Category UpdateCategory(int id, Category category);
    }*/

    public interface ICategoryService : ICommonService<Category>
    {

    }
}
