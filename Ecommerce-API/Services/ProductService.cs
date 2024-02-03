using Ecommerce_API.Repository.IRepository;
using Ecommerce_API.Services.IServices;

namespace Ecommerce_API.Services
{
    public class ProductService : IProductService// Service = Regra de negocios
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository villaRepository)
        {
            _productRepository = villaRepository;
        }

        //public async Task<bool> ProductExistsAsync(string productName)
        //{
        //    if (await _productRepository.GetAsync(x => x.ProductName.ToLower() == productName.ToLower()) =! null)
        //        return 
        //}

        public async Task<bool> ProductExistsAsync(string productName)
        {
            return await _productRepository.ElementExistsAsync(x => x.ProductName.ToLower() == productName.ToLower());
        }
    }
}
