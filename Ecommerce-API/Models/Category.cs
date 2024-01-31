using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_API.Models
{
    [Table("t_categories")]
    public class Category
    {
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("category_name")]
        public string CategoryName { get; set; }
    }
}
