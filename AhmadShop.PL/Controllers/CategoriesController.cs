using AhmadShop.DAL.Data;
using AhmadShop.DAL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AhmadShop.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ApplicationDpContext context = new ApplicationDpContext();

        [HttpGet("")]
        public IActionResult Index()
        {
            var Categories = context.Categories.ToList();
            return Ok(Categories);
        }


        [HttpGet("{id}")]
        public IActionResult GetBYId(int id)
        {
            var Category = context.Categories.Find(id);
            return Ok(Category);
        }


        [HttpPost("")]
        public IActionResult Create(Category Category)
        {
            context.Categories.Add(Category);
            context.SaveChanges();
            return Ok();
        }


    }
}
