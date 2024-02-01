using Ecommerce_API.Models;
using Ecommerce_API.Models.DTOs.AuthDTOs;

namespace Ecommerce_API.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<User> Register(RegisterRequestDTO registerarionRequestDTO);

    }
}
