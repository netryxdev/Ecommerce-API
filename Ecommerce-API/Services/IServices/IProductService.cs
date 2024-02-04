using Ecommerce_API.Models;
using Ecommerce_API.Models.DTOs.ProductDTOs;
using Ecommerce_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Services.IServices
{
    public interface IProductService
    {
        // Aqui eu fiz um acoplamento forte com IProductRepository
        // para testar ver como que ficaria chamando apenas o service.
        // Mas como gera acoplamento forte isso nao deve ser muito usado na pratica.
        Task<bool> ProductExistsAsync(string productName);
        Task<Product> GetAsync(int id, bool tracked = true);
        Task<IEnumerable<Product>> GetAllAsync();
        Task CreateAsync(Product product);
        Task RemoveAsync(Product product);
        Task<Product> UpdateAsync(Product entity);
        Task<List<Product>> SearchProductByNameAsync(string productName);
    }
}
