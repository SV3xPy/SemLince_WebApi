using Microsoft.AspNetCore.Mvc;
using SemLince_Application.IServices;
using SemLince_Domain.Entities;

namespace SemLince_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService service)
        {
            _logger = logger;
            _categoryService = service;
        }

        [HttpGet]
        public ActionResult<List<Category>> GetAllCategories()
        {
            List<Category> categoriesFromService = _categoryService.GetAllCategories();
            return Ok(categoriesFromService);
        }
        
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Category> GetCategoryById(int id)
        {
            Category categoryFromService = _categoryService.GetCategoryById(id);
            if(categoryFromService is null)
            {
                return NotFound();
            }
            return Ok(categoryFromService);
        }

        [HttpPost]
        public ActionResult<Category> PostCategory (Category category)
        {
            Category createdCategory = _categoryService.CreateCategory(category);
            if (createdCategory is null)
            {
                return BadRequest();
            }
            return Ok(createdCategory);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Category> UpdateCategory(int id, Category category) {
            Category updatedCategory = _categoryService.UpdateCategory(id, category);
            if(updatedCategory is null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCategory(int id) { 
            bool rowsAffected = _categoryService.DeleteCategory(id);
            if (rowsAffected) {
                return Ok(id);
            }
            return NotFound();
        }
    }
}
