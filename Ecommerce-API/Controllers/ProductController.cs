using AutoMapper;
using Ecommerce_API.Data;
using Ecommerce_API.Models;
using Ecommerce_API.Models.DTOs.ProductDTOs;
using Ecommerce_API.Repository.IRepository;
using Ecommerce_API.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;

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
        private readonly IProductRepository _productRepo;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        //readonly ProductService _productService; futuramente para regras de negocio.

        public ProductController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Product> Get()
        {
            var products = _dbContext.Products.ToList(); // Refatorar futuramente para fazer a injecao de dependencia com as interfaces
            return products;
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> GetProduct(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                    // Corrigir bug aqui
                Product product = await _productRepo.GetAsync(x => x.ProductId == id);

                if (product == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<Product>(product);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpGet("{searchTerm}", Name = "Search")]
        public IActionResult SearchProducts(string searchTerm)
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
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> CreateProduct([FromBody] ProductCreateDTO createDTO)
        {
            try
            {   // Verificacao no service (Business Logic)
                if (await _productService.ProductExistsAsync(createDTO.ProductName))
                {
                    ModelState.AddModelError("ErrorMessages", "Product already Exists!");
                    return BadRequest(ModelState);
                }

                Product createdProduct = _mapper.Map<Product>(createDTO);
                 
                // Criação no repo (database "upsert"/CRUD operations)
                await _productRepo.CreateAsync(createdProduct);

                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return CreatedAtRoute("GetProduct", new { id = createdProduct.ProductId }, _response);
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
