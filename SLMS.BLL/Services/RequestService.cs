using SLMS.DOL.Entities;
using SLMS.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace SLMS.BLL.Services
{
    public class RequestService : IRequestService
    {
        private readonly SLMSDbContext _context;

        public RequestService(SLMSDbContext context)
        {
            _context = context;
        }

        public void AddRequest(Request request)
        {
            request.RequestDate = request.RequestDate.ToUniversalTime();
            _context.Requests.Add(request);
            _context.SaveChanges(); // 🔥 THIS SAVES TO DB
        }

        public List<Request> GetAllRequests()
        {
            return _context.Requests
                .Include(r => r.Employee)
                .Include(r => r.Resource)
                .ToList();
        }

        public Request GetRequestById(int id)
        {
            return _context.Requests
                .Include(r => r.Employee)
                .Include(r => r.Resource)
                .FirstOrDefault(r => r.Id == id);
        }

        public void UpdateRequest(Request request)
        {
            _context.Requests.Update(request);
            _context.SaveChanges();
        }

        public void DeleteRequest(int id)
        {
            var request = _context.Requests.Find(id);
            if (request != null)
            {
                _context.Requests.Remove(request);
                _context.SaveChanges();
            }
        }
    }
}