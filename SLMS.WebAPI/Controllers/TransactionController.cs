using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Services;
using SLMS.DOL.Entities;
using AutoMapper;
using SLMS.Shared.DTOs;

namespace SLMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly TransactionService _transactionService;
        private readonly IMapper _mapper;

        // ✅ Constructor
        public TransactionController(
            IRequestService requestService,
            TransactionService transactionService,
            IMapper mapper)
        {
            _requestService = requestService;
            _transactionService = transactionService;
            _mapper = mapper;
        }

        // ================= CREATE REQUEST =================
        [HttpPost("create")]
        public IActionResult CreateRequest([FromBody] RequestCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            // 🔥 VALIDATE EMPLOYEE
            var employeeExists = _requestService.EmployeeExists(dto.EmployeeId);
            if (!employeeExists)
            {
                return BadRequest($"Invalid EmployeeId: {dto.EmployeeId}");
            }

            // 🔥 VALIDATE RESOURCE (MOST IMPORTANT FIX)
            var resourceExists = _requestService.ResourceExists(dto.ResourceId);
            if (!resourceExists)
            {
                return BadRequest($"Invalid ResourceId: {dto.ResourceId}");
            }

            // 🔥 MAP DTO → ENTITY
            var request = _mapper.Map<Request>(dto);

            request.Status = "Pending";
            request.RequestDate = DateTime.Now;

            _requestService.AddRequest(request);

            return Ok(new
            {
                message = "Request created successfully"
            });
        }

        // ================= GET ALL =================
        [HttpGet("all")]
        public IActionResult GetAllRequests()
        {
            var requests = _requestService.GetAllRequests();

            var result = _mapper.Map<List<RequestResponseDTO>>(requests);

            return Ok(result);
        }

        // ================= GET BY ID =================
        [HttpGet("{id}")]
        public IActionResult GetRequestById(int id)
        {
            var request = _requestService.GetRequestById(id);

            if (request == null)
                return NotFound("Request not found");

            var result = _mapper.Map<RequestResponseDTO>(request);

            return Ok(result);
        }

        // ================= UPDATE STATUS =================
        [HttpPut("update-status/{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] UpdateStatusDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Status))
                return BadRequest("Invalid status");

            var request = _requestService.GetRequestById(id);

            if (request == null)
                return NotFound("Request not found");

            request.Status = dto.Status;

            _requestService.UpdateRequest(request);

            return Ok("Status updated successfully");
        }

        // ================= DELETE =================
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteRequest(int id)
        {
            var request = _requestService.GetRequestById(id);

            if (request == null)
                return NotFound("Request not found");

            _requestService.DeleteRequest(id);

            return Ok("Request deleted successfully");
        }

        // ================= ISSUE BOOK =================
        [HttpPost("issue")]
        public IActionResult IssueBook([FromBody] IssueBookDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            var result = _transactionService.IssueBook(dto.BookId, dto.UserId);

            if (result == "SUCCESS")
                return Ok("Book Issued");

            return BadRequest(result);
        }

        // ================= RETURN BOOK =================
        [HttpPost("return")]
        public IActionResult ReturnBook([FromBody] ReturnBookDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            var result = _transactionService.ReturnBook(dto.IssueId);

            if (result is string)
                return BadRequest(result);

            return Ok(result);
        }

        // ================= OVERDUE =================
        [HttpGet("overdue")]
        public IActionResult GetOverdueBooks()
        {
            var data = _transactionService.GetOverdueBooks();
            return Ok(data);
        }
    }
}