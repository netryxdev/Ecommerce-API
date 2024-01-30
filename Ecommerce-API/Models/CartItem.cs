using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_API.Models
{
    public class CartItem
    {
        [Column("cart_item_id")]
        public int CartItemId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        
        [Column("product_id")]
        public int ProductId { get; set;}

        [Column("quantity")]
        public int Quantity { get; set;}
    }
}
