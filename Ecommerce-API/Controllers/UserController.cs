using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Ecommerce_API.Data;
using Microsoft.AspNetCore.Identity.Data;
using Ecommerce_API.Models.DTOs;
using Azure;
using Ecommerce_API.Models;

namespace Ecommerce_API.Controllers
{
    [Route("api/UsersAuth")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        protected APIResponse _response;

        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _response = new();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {

        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
        {
            //Passar a verificacao se usuario ja existe para um repositorio de User UserRepository e IUserRepository.
            var isUserUnique =  _dbContext.Users.FirstOrDefault(x => x.UserName == model.UserName);
            if (isUserUnique != null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }


        }
    }
}
}
