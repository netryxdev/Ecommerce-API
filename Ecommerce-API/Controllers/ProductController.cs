using Ecommerce_API.Data;
using Ecommerce_API.Models;
using Ecommerce_API.Models.DTOs.ProductDTOs;
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
        //Passar esse context para a camada de Repository
        private readonly ApplicationDbContext _dbContext;
        protected APIResponse _response;
        private readonly ProductService _productService;

        public ProductController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Product> Get()
        {
            var products = _dbContext.Products.ToList(); // Refatorar futuramente para fazer a injecao de dependencia com as interfaces
            return products;
        }

        [HttpGet("Search/{searchTerm}")]
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
        public async Task<ActionResult<APIResponse>> CreateProduct([FromBody] ProductCreateDTO createDTO)
        {
            try
            {
                if (await _villaService.VillaExistsAsync(createDTO.Name))
                {
                    ModelState.AddModelError("ErrorMessages", "Villa already Exists!");
                    return BadRequest(ModelState);
                }

                // Criação no Service
                VillaDTO createdVilla = await _villaService.CreateVillaAsync(createDTO);

                // Resposta
                return CreatedAtRoute("GetVilla", new { id = createdVilla.Id }, createdVilla);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
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
