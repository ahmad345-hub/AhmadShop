
using AhmadShop.BLL;
using AhmadShop.DAL.DTO.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AhmadShop.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var categories =
                await _service.GetAllAsync(cancellationToken);

            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var category =
                await _service.GetByIdAsync(id, cancellationToken);

            if (category == null)
                return NotFound(new { message = "Category not found." });

            return Ok(category);
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CategoryRequest request,
            CancellationToken cancellationToken)
        {
            var result =
                await _service.CreateAsync(request, cancellationToken);

            if (!result)
            {
                return Conflict(new
                {
                    message = "A category with this name already exists."
                });
            }

            return Ok(new
            {
                message = "Category created successfully."
            });
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CategoryRequest request,
            CancellationToken cancellationToken)
        {
            var result =
                await _service.UpdateAsync(id, request, cancellationToken);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Category not found or name already exists."
                });
            }

            return Ok(new
            {
                message = "Category updated successfully."
            });
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            var result =
                await _service.DeleteAsync(id, cancellationToken);

            if (!result)
                return NotFound(new { message = "Category not found." });

            return Ok(new
            {
                message = "Category deleted successfully."
            });
        }
    }
}
