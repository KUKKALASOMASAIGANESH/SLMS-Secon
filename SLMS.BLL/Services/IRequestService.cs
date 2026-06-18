using SLMS.DOL.Entities;
using System.Collections.Generic;

namespace SLMS.BLL.Services
{
    public interface IRequestService
    {
        void AddRequest(Request request);
        List<Request> GetAllRequests();
        Request GetRequestById(int id);
        void UpdateRequest(Request request);
        void DeleteRequest(int id);
        bool ResourceExists(int resourceId);
        bool EmployeeExists(int employeeId);
    }
}
