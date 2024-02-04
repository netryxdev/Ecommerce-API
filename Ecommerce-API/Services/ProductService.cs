using Ecommerce_API.Models;
using Ecommerce_API.Models.DTOs.ProductDTOs;
using Ecommerce_API.Repository;
using Ecommerce_API.Repository.IRepository;
using Ecommerce_API.Services.IServices;

namespace Ecommerce_API.Services
{
    public class ProductService : IProductService
    {
        // Service = Regra de negocios
        // Aqui eu fiz um acoplamento forte com IProductRepository e tive que "Re-chamar" apontando os metodos.
        // para testar ver como que ficaria chamando apenas o service no controller de products.
        // Mas pode gerar acoplamento forte isso nao deve ser muito usado na pratica.
        // Como pode ver isso mantem as coisas mais complicadas, mas foi so para ver como funciona.
        // Ou seja, so e util em projetos complexos que trazem beneficios a longo prazo. Aqui tras redundancia.
        // Em resumo eu fiz o repoProduct e aqui em no service eu consumo tudo do repo + logica adicional de regra de negocio
        // Entao fica um pouco redundante em alguns casos como pode ver.
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        public async Task<bool> ProductExistsAsync(string productName)
        {
            return await _productRepo.ElementExistsAsync(x => x.ProductName.ToLower() == productName.ToLower());
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepo.GetAllAsync();
        }

        public async Task<Product> GetAsync(int id, bool tracked = true) //tracker è param opcional
        {
            return await _productRepo.GetAsync(x => x.ProductId == id, tracked);
        }

        public async Task CreateAsync(Product product)
        {
            await _productRepo.CreateAsync(product);
        }

        public async Task RemoveAsync(Product product)
        {
            await _productRepo.RemoveAsync(product);
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
           return await _productRepo.UpdateAsync(entity);
        }

        public async Task<List<Product>> SearchProductByNameAsync(string productName)
        {
            return await _productRepo.SearchProductByNameAsync(productName);
        }
        //public async Task<Product> UpdateProductAsync(Product entity)
        //{
        //    // Adicione lógica específica do serviço, se necessário
        //    entity.Updated_date = DateTime.Now;

        //    // Chame o método de repositório
        //    return await _productRepository.UpdateAsync(entity);
        //}
    }
}
