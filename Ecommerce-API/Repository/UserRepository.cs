using Ecommerce_API.Data;
using Ecommerce_API.Models;
using Ecommerce_API.Models.DTOs;
using Ecommerce_API.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;

        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.Users.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower()
                && u.UserPassword == loginRequestDTO.Password);

            if (user == null)
                return null;

            // if user was found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.UserId.ToString()),
                    new Claim(ClaimTypes.Role,user.RoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Tempo de expiracao do JWT token
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };
            return loginResponseDTO;
        }

        public async Task<User> Register(RegisterRequestDTO registerarionRequestDTO)
        {
            User user = new();
            {
                user.UserName = registerarionRequestDTO.UserName;
                user.UserPassword = registerarionRequestDTO.UserPassword;
                user.UserName = registerarionRequestDTO.UserName;
                user.RoleId = registerarionRequestDTO.RoleId;
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            user.UserPassword = ""; // Retorna vazio para confidecialidade 
            return user;
        }
    }
}
