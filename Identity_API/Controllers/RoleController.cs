using Identity_API.Model.DTO;
using Identity_API.Model.Interfaces;
using Identity_Service.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleService roleService, ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            _logger.LogInformation("RoleController.GetAllRoles called");

            var result = await _roleService.GetRoles();

            if (result == null || !result.Any())
            {
                _logger.LogInformation("No Roles found.");
                return NoContent();
            }
            else
            {
                _logger.LogInformation("Found {Count} Roles.", result.Count);
                return Ok(result);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            _logger.LogInformation("RoleController.GetRoleById called");
            var result = await _roleService.GetRoleById(id);

            if (result == null)
            {
                _logger.LogInformation("Role with ID {Id} not found.", id);
                return NotFound();
            }
            _logger.LogInformation("Found Role with ID {Id}.", id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, RoleDTOUpdate role)
        {
            _logger.LogInformation("RoleController.UpdateRole called");
            var result = await _roleService.UpdateRole(id, role);
            if (!result)
            {
                _logger.LogWarning("Role with ID {Id} not found or update failed.", id);
                return NotFound();
            }
            _logger.LogInformation("Role with ID {Id} updated successfully.", id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostRole(RoleDTO role)
        {
            _logger.LogInformation("RolesController.CreateRole called");
            var result = await _roleService.PostRole(role);

            if (result == null)
            {
                _logger.LogWarning("Failed to create role.");
                return BadRequest("Failed to create role.");
            }
            
            _logger.LogInformation("Role created with ID {Id}.", result);
            return CreatedAtAction("GetRoleById", new { id = result }, role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            _logger.LogInformation("RoleController.DeleteRole called");
            var result = await _roleService.DeleteRole(id);
            if (result == null)
            {
                _logger.LogWarning("Role with ID {Id} not found or delete failed.", id);
                return NotFound();
            }
            _logger.LogInformation("Role with ID {Id} deleted successfully.", id);
            return Ok();
        }

    }
}
