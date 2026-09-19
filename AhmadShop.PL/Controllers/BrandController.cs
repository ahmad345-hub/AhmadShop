using AhmadShop.DAL.Data;
using AhmadShop.DAL.Model;
using AhmadShop.DAL.Repositires;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AhmadShop.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        CategoryRepository<Brand> repo = new CategoryRepository<Brand>();

        [HttpGet("")]
        public IActionResult Index()
        {
            var Brands = repo.Get();
            return Ok(Brands);
        }


        [HttpGet("{id}")]
        public IActionResult GetBYId(int id)
        {
            var Brand = repo.Find(id);
            return Ok(Brand);
        }


        [HttpPost("")]
        public IActionResult Create(Brand Brand)
        {
            repo.Create(Brand);

            return Ok();
        }
    }
}