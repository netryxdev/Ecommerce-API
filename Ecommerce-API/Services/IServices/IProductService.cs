namespace Ecommerce_API.Services.IServices
{
    public interface IProductService
    {
        Task<bool> ProductExistsAsync(string productName);
    }
}
