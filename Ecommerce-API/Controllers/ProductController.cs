using Ecommerce_API.Data;
using Ecommerce_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ecommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        // GET: api/<ProductController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Product> Get()
        {
            var products = _dbContext.Products.ToList(); // Refatorar futuramente para fazer a injecao de dependencia com as interfaces
            return products;
        }

        // GET api/<ProductController>/5
        [HttpGet("Search/{searchTerm}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult SearchProducts([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("O parâmetro 'searchTerm' é obrigatório.");
            }

            var matchingProducts = _dbContext.Products
                .Where(p => EF.Functions.Like(p.ProductName, $"%{searchTerm}%"))
                .ToList();

            if (matchingProducts.Count == 0)
            {
                return NotFound(); // Nenhum produto correspondente encontrado
            }

            return Ok(matchingProducts);
        }


        // POST api/<ProductController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public void Delete(int id)
        {
        }
    }
}
