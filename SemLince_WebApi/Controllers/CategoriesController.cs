using Microsoft.AspNetCore.Mvc;
using SemLince_Application;
using SemLince_Domain;

namespace SemLince_WebApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ILogger<CategoriesController> logger, ICategoryService service)
        {
            //_logger = logger;
            _categoryService = service;
        }

        [HttpGet(Name = "GetAllCategories")]
        public ActionResult<List<Category>> GetAllCategories()
        {
            var categoriesFromService = _categoryService.GetAllCategories();
            return Ok(categoriesFromService);
        }
        
        [HttpGet()]
        [Route("{id}")]
        public ActionResult<Category> GetCategoryById(int id)
        {
            var categoryFromService = _categoryService.GetCategoryById(id);
            return Ok(categoryFromService);
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
