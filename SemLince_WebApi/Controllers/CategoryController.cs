using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            IEnumerable<Category> categoriesFromService = await _categoryService.GetAllAsync();
            if (categoriesFromService.IsNullOrEmpty())
            {
                return NotFound("Actualmente dicho apartado no tiene registros.");
            }
            return Ok(categoriesFromService);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            Category categoryFromService = await _categoryService.GetByIdAsync(id);
            if (categoryFromService is null)
            {
                return NotFound($"El registro con id: {id}, no se encontro. " +
                    $"Favor de verificar e intentar de nuevo");
            }
            return Ok(categoryFromService);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            Category createdCategory = await _categoryService.AddAsync(category);
            if (createdCategory is null)
            {
                return BadRequest();
            }
            return Ok(createdCategory);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, Category category)
        {
            if (await _categoryService.GetByIdAsync(id) is null)
            {
                return NotFound($"El registro con id: {id}, a actualizar no se encontro. " +
                    $"Favor de verificar e intentar de nuevo");
            }
            Category updatedCategory = await _categoryService.UpdateAsync(id, category);
            if (updatedCategory is null)
            {
                return BadRequest();
            }
            return Ok(updatedCategory);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            bool rowsAffected = await _categoryService.DeleteAsync(id);
            if (!rowsAffected)
            {
                return NotFound($"El registro con id: {id}, a eliminar no se encontro." +
                     $"Favor de verificar e intentar de nuevo");
            }
            return Ok(id);
        }
    }
}
