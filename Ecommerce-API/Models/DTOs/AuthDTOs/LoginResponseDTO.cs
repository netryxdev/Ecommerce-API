namespace Ecommerce_API.Models.DTOs.AuthDTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public User User { get; set; }
    }
}
