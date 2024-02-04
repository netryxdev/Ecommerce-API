using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Models.DTOs.ProductDTOs
{
    public class ProductUpdateDTO
    {
        [Required]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductDiscount { get; set; }
        public int ProductCategoryId { get; set; }
        public string ProductImgUrl { get; set; }
    }
}
