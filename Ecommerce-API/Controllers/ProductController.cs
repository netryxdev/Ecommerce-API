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

        public ProductController(IProductRepository productRepo,
            ApplicationDbContext dbContext, IProductService productService, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            this._response = new();
            _productRepo = productRepo;
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Product>> Get()
        {
            //_dbContext.Products.ToList();
            var products = await _productService.GetAllAsync(); // Refatorar futuramente para fazer a injecao de dependencia com as interfaces
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
                    
                var product = await _productService.GetAsync(id);

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

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> CreateProduct([FromBody] ProductCreateDTO createDTO)
        {
            try
            {   
                if (await _productService.ProductExistsAsync(createDTO.ProductName))
                {
                    ModelState.AddModelError("ErrorMessages", "Product already Exists!");
                    return BadRequest(ModelState);
                }

                Product createdProduct = _mapper.Map<Product>(createDTO);
                 
                // Criação no repo (database "upsert"/CRUD operations)
                await _productService.CreateAsync(createdProduct);

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
        public async Task<ActionResult<APIResponse>> UpdateProduct(int id, [FromBody] ProductUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.ProductId)
                {
                    return BadRequest();
                }

                if (await _productService.GetAsync(id) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Villa ID is Invalid!");
                    return BadRequest(ModelState);
                }

                Product productUpdated = _mapper.Map<Product>(updateDTO);

                await _productService.UpdateAsync(productUpdated);

                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteProduct(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var product = await _productService.GetAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                await _productService.RemoveAsync(product);
                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }
    }
}
