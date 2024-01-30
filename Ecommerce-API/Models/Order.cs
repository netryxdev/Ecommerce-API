using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_API.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }

        [Column("status_id")]
        public int OrderStatus { get; set; }
    }
}
