using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog.WebAPI.Context;
using Serilog.WebAPI.Entities;

namespace Serilog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly SerilogDbContext _context;

        public ProductController(SerilogDbContext context, ILogger<ProductController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateAsync([FromForm]Product product)
        {
            try
            {
                product.Id = Guid.NewGuid();
                var createProduct = await _context.AddAsync(product);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Mehsul ugurla yaradildi. Product ID: {product.Id}, Name: {product.Name}, Price: {product.Price}");
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Mehsul yaradilmadi.Mehsul melumatlari{@product}");
                return BadRequest(ex);
            }
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var allData = await _context.Products.ToListAsync();
            return Ok(allData);
        }
    }
}
