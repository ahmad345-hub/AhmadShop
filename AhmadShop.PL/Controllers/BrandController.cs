
using AhmadShop.DAL.Model;
using AhmadShop.DAL.Repositires;
using Microsoft.AspNetCore.Mvc;

namespace AhmadShop.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IGenericRepositry<Brand> _repository;

        public BrandController(IGenericRepositry<Brand> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var brands = await _repository.GetAsync(
                cancellationToken: cancellationToken);

            return Ok(brands);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var brand = await _repository.FindAsync(
                id,
                cancellationToken);

            if (brand is null)
                return NotFound();

            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            Brand brand,
            CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(
                brand,
                cancellationToken);

            return Ok(brand);
        }
    }
}
