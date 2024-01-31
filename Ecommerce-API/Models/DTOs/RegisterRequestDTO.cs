namespace Ecommerce_API.Models.DTOs
{
    public class RegisterRequestDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserCity { get; set; }
        public int RoleId { get; set; } = 3;
        public string UserAdress { get; set; }
    }
}
