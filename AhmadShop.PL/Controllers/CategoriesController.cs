using AhmadShop.DAL.Data;
using AhmadShop.DAL.Model;
using AhmadShop.DAL.Repositires;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AhmadShop.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        CategoryRepository<Category> repo = new CategoryRepository<Category>();

        [HttpGet("")]
        public IActionResult Index()
        {
            var Categories = repo.Get();
            return Ok(Categories);
        }


        [HttpGet("{id}")]
        public IActionResult GetBYId(int id)
        {
            var Category = repo.Find(id);
            return Ok(Category);
        }


        [HttpPost("")]
        public IActionResult Create(Category Category)
        {
            repo.Create(Category);

            return Ok();
        }


        [HttpPut("")]
        public IActionResult Update(Category Category)
        {
            repo.Update(Category);

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Category = repo.Find(id);
            repo.Remove(Category);

            return Ok();
        }
    }
}