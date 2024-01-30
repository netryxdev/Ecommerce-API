using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_API.Models
{
    [Table("t_products")]
    public class Product
    {
        [Column("product_id")]
        public int ProductId { get; set; }
        
        [Column("product_name")]
        public string ProductName { get; set; }

        [Column("product_description")]
        public string ProductDescription { get; set; }
        
        [Column("product_price")]
        public decimal ProductPrice { get; set; }
        
        [Column("product_discount")]
        public decimal ProductDiscount { get; set; }
        
        [Column("category_id")]
        public int ProductCategoryId { get; set; }
        
        [Column("product_img_url")]
        public string ProductImgUrl { get; set; }
    }
}
