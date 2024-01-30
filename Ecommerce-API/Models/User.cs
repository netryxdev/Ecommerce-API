namespace Ecommerce_API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword {  get; set; }
        public string UserCity { get; set; }
        public int RoleId { get; set; }
        public string UserAdress { get; set; }
    }
}
