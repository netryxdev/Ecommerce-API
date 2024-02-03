using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_API.Models
{
    [Table("t_users")]
    public class User
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("user_password")]
        public string UserPassword {  get; set; }

        [Column("user_city")]
        public string UserCity { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("user_address")]
        public string UserAdress { get; set; }
    }
}
