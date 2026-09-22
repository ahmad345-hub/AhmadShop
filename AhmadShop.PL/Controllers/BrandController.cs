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
        private readonly IGenericRepositry<Brand> _repositry;
        public BrandController(IGenericRepositry<Brand> repositry)
        {
            _repositry = repositry;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var Brands = _repositry.Get();
            return Ok(Brands);
        }


        [HttpGet("{id}")]
        public IActionResult GetBYId(int id)
        {
            var Brand = _repositry.Find(id);
            return Ok(Brand);
        }


        [HttpPost("")]
        public IActionResult Create(Brand Brand)
        {
            _repositry.Create(Brand);

            return Ok();
        }
    }
}