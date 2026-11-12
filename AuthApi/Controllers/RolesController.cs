using AuthApi.Models;
using AuthApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AuthApi.Controllers
{
    [Route("roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AuthuserContext _contex;

        public RolesController(AuthuserContext contex)
        {
            _contex = contex;
        }

        [HttpPost("newRole")]
        public async Task<IActionResult> NewRole([FromBody] NewRoleDto newRoleDto)
        {
            try
            {
                var role = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = newRoleDto.Name,
                    RegTime = DateTime.Now
                };

                if (role is null)
                {
                    return StatusCode(404, new { message = "Nincs szerepkör megadva." });
                }

                await _contex.Roles.AddAsync(role);
                await _contex.SaveChangesAsync();

                return StatusCode(201, new { message = "Felvéve", result = role });
            }
            catch (Exception exception)
            {
                return StatusCode(400, new { message = exception.Message});
            }
           
        }
    }
}
