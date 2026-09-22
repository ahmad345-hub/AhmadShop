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
        private readonly IGenericRepositry<Category> _repositry;
        public CategoriesController(IGenericRepositry<Category> repositry)
        {
            _repositry=repositry;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var Categories = _repositry.Get();
            return Ok(Categories);
        }


        [HttpGet("{id}")]
        public IActionResult GetBYId(int id)
        {
            var Category = _repositry.Find(id);
            return Ok(Category);
        }


        [HttpPost("")]
        public IActionResult Create(Category Category)
        {
            _repositry.Create(Category);

            return Ok();
        }


        [HttpPut("")]
        public IActionResult Update(Category Category)
        {
            _repositry.Update(Category);

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Category = _repositry.Find(id);
            _repositry.Remove(Category);

            return Ok();
        }
    }
}