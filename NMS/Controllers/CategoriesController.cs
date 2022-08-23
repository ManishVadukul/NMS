using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NMS.Core.IRepositories;
using NMS.Models;
//using NMS.Repository;

namespace NMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryRepository categoryRepo, ILogger<CategoriesController> logger)
        {
            _categoryRepo = categoryRepo;
            _logger = logger;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/categories", "GET");
            return Ok(await _categoryRepo.GetAllCategories());
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            _logger.LogInformation("Run endpoint {endpoint} {verb}", "/api/categories", "POST");
            return Ok(await _categoryRepo.CreateCategory(category));
        }

    }
}
