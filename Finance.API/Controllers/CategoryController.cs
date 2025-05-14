using Finance.API.Model;
using Finance.API.Service.Interface;
using Finance.API.ViewModel;
using Finance.API.ViewModel.Request;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestVM request)
        {
            Category entity = new Category(request);
            bool result = await _categoryService.CreateCategory(entity);

            return SendSuccess(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCategories()
        {
            List<Category> categoryList = await _categoryService.GetAllCategories();
            List<CategoryVM> result = new List<CategoryVM>();

            foreach (Category category in categoryList)
            {
                result.Add(new CategoryVM(category));
            }

            return SendSuccess(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            Category entity = await _categoryService.GetCategoryById(id);

            CategoryVM result = new CategoryVM(entity);

            return SendSuccess(result);
        }
    }
}
