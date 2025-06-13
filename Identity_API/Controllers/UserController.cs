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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            _logger.LogInformation("UserController.GetAllUsers called");

            var result = await _userService.GetUsers();

            if (result == null || !result.Any())
            {
                _logger.LogInformation("No Users found.");
                return NoContent();
            }
            else
            {
                _logger.LogInformation("Found {Count} Users.", result.Count);
                return Ok(result);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            _logger.LogInformation("UserController.GetUsersById called");
            var result = await _userService.GetUserById(id);

            if (result == null)
            {
                _logger.LogInformation("User with ID {Id} not found.", id);
                return NotFound();
            }
            _logger.LogInformation("Found User with ID {Id}.", id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, UserDTOUpdate user)
        {
            _logger.LogInformation("UserController.UpdateUser called");
            var result = await _userService.UpdateUser(id, user);
            if (!result)
            {
                _logger.LogWarning("User with ID {Id} not found or update failed.", id);
                return NotFound();
            }
            _logger.LogInformation("User with ID {Id} updated successfully.", id);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(UserDTO user)
        {
            _logger.LogInformation("UsersController.CreateUser called");
            var result = await _userService.PostUsers(user);

            if (result == null)
            {
                _logger.LogWarning("Failed to create user.");
                return BadRequest("Failed to create user.");
            }
            _logger.LogInformation("User created with ID {Id}.", result);
            return CreatedAtAction("GetUser", new { id = result }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            _logger.LogInformation("UserController.DeleteUser called");
            var result = await _userService.DeleteUsers(id);
            if (result == null)
            {
                _logger.LogWarning("User with ID {Id} not found or delete failed.", id);
                return NotFound();
            }
            _logger.LogInformation("User with ID {Id} deleted successfully.", id);
            return Ok();
        }

    }
}
