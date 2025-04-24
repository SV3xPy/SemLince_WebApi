using Microsoft.AspNetCore.Mvc;
using SemLince_Application;
using SemLince_Domain;

namespace SemLince_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ILogger<CategoriesController> logger, ICategoryService service)
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

        [HttpPatch]
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

        /*
        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
