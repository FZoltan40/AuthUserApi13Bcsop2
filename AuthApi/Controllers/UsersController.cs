using AuthApi.Models;
using AuthApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AuthuserContext _context;

        public UsersController(AuthuserContext context)
        {
            _context = context;
        }

        [HttpPost("newUser")]
        public async Task<IActionResult> NewUser([FromBody]NewUserDto newUserDto )
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = newUserDto.Name,
                Email = newUserDto.Email,
                Password = newUserDto.Password,
            };

            if (user is null)
            {
                return StatusCode(400, new { message = "Hiba"});
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return StatusCode(201, new { message = "Felvéve" , result = user});
        }
    }
}
