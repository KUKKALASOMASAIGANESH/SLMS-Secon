using Microsoft.AspNetCore.Mvc;
using SLMS.DOL.Entities;   // ✅ Correct namespace
using SLMS.BLL.Services;   // (Assuming you have service layer)

namespace SLMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IRequestService _requestService;

        // ✅ Constructor
        public TransactionController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // ✅ CREATE REQUEST
        [HttpPost("create")]
        //[HttpPost("create")]
        public IActionResult CreateRequest([FromBody] Request request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data");
            }

            // 🔥 Ignore navigation properties
            request.Employee = null!;
            request.Resource = null!;

            request.Status = "Pending";
            request.RequestDate = DateTime.Now;

            _requestService.AddRequest(request);

            return Ok(new
            {
                message = "Request created successfully",
                data = request
            });
        }
        // ✅ GET ALL REQUESTS
        [HttpGet("all")]
        public IActionResult GetAllRequests()
        {
            var requests = _requestService.GetAllRequests();
            return Ok(requests);
        }

        // ✅ GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetRequestById(int id)
        {
            var request = _requestService.GetRequestById(id);

            if (request == null)
            {
                return NotFound("Request not found");
            }

            return Ok(request);
        }

        // ✅ UPDATE STATUS
        [HttpPut("update-status/{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] string status)
        {
            var request = _requestService.GetRequestById(id);

            if (request == null)
            {
                return NotFound("Request not found");
            }

            request.Status = status;
            _requestService.UpdateRequest(request);

            return Ok("Status updated successfully");
        }

        // ✅ DELETE REQUEST
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteRequest(int id)
        {
            var request = _requestService.GetRequestById(id);

            if (request == null)
            {
                return NotFound("Request not found");
            }

            _requestService.DeleteRequest(id);

            return Ok("Request deleted successfully");
        }
    }
}