using Ecommerce_API.Repository.IRepository;

namespace Ecommerce_API.Services
{
    public class ProductService // Service = Regra de negocios
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
