using Microsoft.AspNetCore.Mvc;
using SLMS.BLL.Services;   // (Assuming you have service layer)
using SLMS.DOL.Entities;   // ✅ Correct namespace
using AutoMapper;
using SLMS.Shared.DTOs;

namespace SLMS.WebAPI.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly TransactionService _transactionService;

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
        private readonly IMapper _mapper;
        // ✅ CREATE REQUEST
        //[HttpPost("create")]
        ////[HttpPost("create")]
        //public IActionResult CreateRequest([FromBody] Request request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest("Invalid request data");
        //    }

        //    // 🔥 Ignore navigation properties
        //    request.Employee = null!;
        //    request.Resource = null!;

        //    request.Status = "Pending";
        //    request.RequestDate = DateTime.Now;

        //    _requestService.AddRequest(request);

        //    return Ok(new
        //    {
        //        message = "Request created successfully",
        //        data = request
        //    });
        //}


        [HttpPost("create")]
        public IActionResult CreateRequest([FromBody] RequestCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

            var request = _mapper.Map<Request>(dto);

            request.Status = "Pending";
            request.RequestDate = DateTime.Now;
            _requestService.AddRequest(request);

            return Ok(new
            {
                message = "Request created successfully"
            });
        }
        //// ✅ GET ALL REQUESTS
        //[HttpGet("all")]
        //public IActionResult GetAllRequests()
        //{
        //    var requests = _requestService.GetAllRequests();
        //    return Ok(requests);
        //}

        [HttpGet("all")]
        public IActionResult GetAllRequests()
        {
            var requests = _requestService.GetAllRequests();

            var result = _mapper.Map<List<RequestResponseDTO>>(requests);
            

            return Ok(result);
        }

        //// ✅ GET BY ID
        //[HttpGet("{id}")]
        //public IActionResult GetRequestById(int id)
        //{
        //    var request = _requestService.GetRequestById(id);

        //    if (request == null)
        //    {
        //        return NotFound("Request not found");
        //    }

        //    return Ok(request);
        //}

        [HttpGet("{id}")]
        public IActionResult GetRequestById(int id)
        {
            var r = _requestService.GetRequestById(id);

            if (r == null)
                return NotFound();

            var result = _mapper.Map<RequestResponseDTO>(r);

            return Ok(result);
        }

        //// ✅ UPDATE STATUS
        //[HttpPut("update-status/{id}")]
        //public IActionResult UpdateStatus(int id, [FromBody] string status)
        //{
        //    var request = _requestService.GetRequestById(id);

        //    if (request == null)
        //    {
        //        return NotFound("Request not found");
        //    }

        //    request.Status = status;
        //    _requestService.UpdateRequest(request);

        //    return Ok("Status updated successfully");
        //}

        [HttpPut("update-status/{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] UpdateStatusDTO dto)
        {
            var request = _requestService.GetRequestById(id);

            if (request == null)
                return NotFound();

            request.Status = dto.Status;
            _requestService.UpdateRequest(request);

            return Ok("Status updated");
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
        //[HttpPost("issue")]
        //public IActionResult IssueBook(int bookId, int userId)
        //{
        //    var result = _transactionService.IssueBook(bookId, userId);

        //    if (result == "SUCCESS")
        //        return Ok("Book Issued");

        //    return BadRequest(result);
        //}

        [HttpPost("issue")]
        public IActionResult IssueBook([FromBody] IssueBookDTO dto)
        {
            var result = _transactionService.IssueBook(dto.BookId, dto.UserId);

            if (result == "SUCCESS")
                return Ok("Book Issued");

            return BadRequest(result);
        }
        //[HttpPost("return")]
        //public IActionResult ReturnBook(int issueId)
        //{
        //    var result = _transactionService.ReturnBook(issueId);

        //    if (result is string)
        //        return BadRequest(result);

        //    return Ok(result);
        //}

        [HttpPost("return")]
        public IActionResult ReturnBook([FromBody] ReturnBookDTO dto)
        {
            var result = _transactionService.ReturnBook(dto.IssueId);

            if (result is string)
                return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("overdue")]
        public IActionResult GetOverdueBooks()
        {
            var data = _transactionService.GetOverdueBooks();
            return Ok(data);
        }


    }
