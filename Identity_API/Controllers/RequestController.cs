using Identity_API.Model.DTO;
using Identity_API.Model.Interfaces;
using Identity_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Identity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly ILogger<RequestController> _logger;

        public RequestController(IRequestService requestService, ILogger<RequestController> logger)
        {
            _requestService = requestService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetRequests()
        {
            _logger.LogInformation("RequestController.GetAllRequests called");

            var result = await _requestService.GetRequests();

            if (result == null || !result.Any())
            {
                _logger.LogInformation("No Requests found.");
                return NoContent();
            }
            else
            {
                _logger.LogInformation("Found {Count} Requests.", result.Count);
                return Ok(result);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRequest(int id)
        {
            _logger.LogInformation("RequestController.GetRequestsById called");
            var result = await _requestService.GetRequestById(id);

            if (result == null)
            {
                _logger.LogInformation("Request with ID {Id} not found.", id);
                return NotFound();
            }
            _logger.LogInformation("Found Request with ID {Id}.", id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, RequestDTOUpdate request)
        {
            _logger.LogInformation("RequestController.UpdateRequest called");
            var result = await _requestService.UpdateRequest(id, request);
            if (!result)
            {
                _logger.LogWarning("Request with ID {Id} not found or update failed.", id);
                return NotFound();
            }
            _logger.LogInformation("Request with ID {Id} updated successfully.", id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostRequest(RequestDTO request)
        {
            _logger.LogInformation("RequestController.CreateRequest called");
            var result = await _requestService.PostRequest(request);

            if (result == null)
            {
                _logger.LogWarning("Failed to create Request.");
                return BadRequest("Failed to create Request.");
            }
            _logger.LogInformation("Request created with ID {Id}.", result);
            return CreatedAtAction("GetRequest", new { id = result }, request);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            _logger.LogInformation("RequestController.DeleteRequest called");
            var result = await _requestService.DeleteRequest(id);
         
            if (result == null)
            {
                _logger.LogWarning("delete failed.");
                return NotFound();
            }
            if (result == -1)
            {
                _logger.LogWarning("Request with ID {Id} not found", id);
                return NotFound();
            }
            _logger.LogInformation("Request with ID {Id} deleted successfully.", id);
            return Ok();
        }

        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetRequestsByUser(int userId)
        {
            _logger.LogInformation("RequestController.GetRequestByUser called");
            var result = await _requestService.GetRequestByUser(userId);
            if (result == null)
            {
                _logger.LogWarning("User with ID {Id} not found", userId);
                return NotFound();
            }

            _logger.LogInformation("Found {Count} Requests.", result.Count);
            return Ok(result);
        }
    }
}
