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
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        private readonly ILogger<UserRoleController> _logger;

        public UserRoleController(IUserRoleService userRoleService, ILogger<UserRoleController> logger)
        {
            _userRoleService = userRoleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRoles()
        {
            _logger.LogInformation("UserRoleController.GetAllUserRoles called");

            var result = await _userRoleService.GetAllUserRoles();

            if (result == null || !result.Any())
            {
                _logger.LogInformation("No UserRoles found.");
                return NoContent();
            }
            else
            {
                _logger.LogInformation("Found {Count} UserRoles.", result.Count);
                return Ok(result);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserRole(int id)
        {
            _logger.LogInformation("UserRoleController.GetUserRolesById called");
            var result = await _userRoleService.GetUserRoleById(id);

            if (result == null)
            {
                _logger.LogInformation("UserRole with ID {Id} not found.", id);
                return NotFound();
            }
            _logger.LogInformation("Found UserRole with ID {Id}.", id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRole(int id, UserRoleDTOUpdate userRole)
        {
            _logger.LogInformation("UserRoleController.UpdateUserRole called");
            var result = await _userRoleService.UpdateUserRole(id, userRole);
            if (!result)
            {
                _logger.LogWarning("UserRole with ID {Id} not found or update failed.", id);
                return NotFound();
            }
            _logger.LogInformation("UserRole with ID {Id} updated successfully.", id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostUserRole(UserRoleDTO userRole)
        {
            _logger.LogInformation("UserRolesController.CreateUserRole called");
            var result = await _userRoleService.PostUserRole(userRole);

            if (result == null)
            {
                _logger.LogWarning("Failed to create userRole.");
                return BadRequest("Failed to create userRole.");
            }
            if (result == -1)
            {
                _logger.LogWarning("No user with such Id");
                return BadRequest("Failed to create role. No user with such Id.");
            }
            if (result == -2)
            {
                _logger.LogWarning("No role with such Id");
                return BadRequest("Failed to create role. No role with such Id");
            }
            _logger.LogInformation("UserRole created with ID {Id}.", result);
            return CreatedAtAction("GetUserRole", new { id = result }, userRole);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRole(int id)
        {
            _logger.LogInformation("UserRoleController.DeleteUserRole called");
            var result = await _userRoleService.DeleteUserRole(id);
            if (result == null)
            {
                _logger.LogWarning("UserRole with ID {Id} not found or delete failed.", id);
                return NotFound();
            }
            _logger.LogInformation("UserRole with ID {Id} deleted successfully.", id);
            return Ok();
        }

    }
}
