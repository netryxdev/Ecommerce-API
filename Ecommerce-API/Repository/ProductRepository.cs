using AutoMapper;
using Ecommerce_API.Data;
using Ecommerce_API.Models;
using Ecommerce_API.Models.DTOs.ProductDTOs;
using Ecommerce_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_API.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository // Verificar bug?
    {
        // readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db) 
        { 
            _db = db;
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            entity.Updated_date = DateTime.Now;
            _db.Products.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }


        //public async Task<bool> GetAsyncProd(int id)
        //{
        //    return _db.Products.GetAsync(x => x.ProductId == id);
        //}
        //public async Task CreateAsync (ProductCreateDTO productCreateDTO)
        //{
        //    Product product = _mapper.Map<Product>(productCreateDTO);
        //    _db.Products.Add(product);
        //    await _db.SaveChangesAsync();
        //}
    }
}
