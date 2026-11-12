using AuthApi.Models;
using AuthApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AuthApi.Controllers
{
    [Route("roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AuthuserContext _context;

        public RolesController(AuthuserContext context)
        {
            _context = context;
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

                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();

                return StatusCode(201, new { message = "Felvéve", result = role });
            }
            catch (Exception exception)
            {
                return StatusCode(400, new { message = exception.Message});
            }
        }

        [HttpGet("allRole")]
        public async Task<IActionResult> AllRole()
        {
            try
            {
                var roles = await _context.Roles.ToListAsync();

                if (roles is null)
                {
                    return StatusCode(404, new { message = "Nincs találat" });
                }

                return StatusCode(200, new { message = "Lekérdezve", result = roles });
            }
            catch (Exception exception)
            {
                return StatusCode(400, new { message = exception.Message });
            }
        }

        [HttpGet("byId")]
        public async Task<IActionResult> RoleById([FromQuery] Guid id)
        {
            try
            {
                var role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == id);

                if (role is null)
                {
                    return StatusCode(404, new { message = "Nincs találat" });
                }
                else 
                {
                    return StatusCode(200, new { message = "Lekérdezve", result = role });
                }
            }
            catch (Exception exception)
            {
                return StatusCode(400, new { message = exception.Message });
            }
          

          
        }
    }
}
