using Identity_API.Model.DTO;
using Identity_API.Model.ViewModel;
using Identity_Service.Model;

namespace Identity_API.Model.Interfaces
{
    public interface IRequestService
    {
        public Task<List<RequestViewModel>> GetRequests();
        public Task<RequestViewModel> GetRequestById(int id);
        public Task<List<RequestViewModel>> GetRequestByUser(int id);
        public Task<bool> UpdateRequest(int id, RequestDTOUpdate request);
        public Task<int?> PostRequest(RequestDTO request);
        public Task<int?> DeleteRequest(int id);
    }
}
