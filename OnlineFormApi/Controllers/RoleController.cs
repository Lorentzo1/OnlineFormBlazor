using Microsoft.AspNetCore.Mvc;
using OnlineFormApi.Services.Abstractions;

namespace OnlineFormApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [HttpGet("/role/names")]
        public async Task<ActionResult<IEnumerable<string>>> GetRoles()
        {
            var roles = await roleRepository.GetAllRolesNames();
            if (roles.Count() > 0)
            {
                return Ok(roles);
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
