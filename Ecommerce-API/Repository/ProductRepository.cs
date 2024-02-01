using AutoMapper;
using Ecommerce_API.Data;
using Ecommerce_API.Models;
using Ecommerce_API.Models.DTOs.ProductDTOs;
using Ecommerce_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db, IMapper mapper) 
        { 
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> ProductExistsAsync (string productName)
        {
            return await _db.Products.AnyAsync(x => x.ProductName.ToLower() == productName.ToLower());
        }

        public async Task CreateAsync (ProductCreateDTO productCreateDTO)
        {
            Product product = _mapper.Map<Product>(productCreateDTO);
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
        }
    }
}
