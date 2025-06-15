using Identity_API.Model;
using Identity_API.Model.DTO;
using Identity_API.Model.Interfaces;
using Identity_API.Model.ViewModel;
using Identity_Service.Model;
using Microsoft.EntityFrameworkCore;

namespace Identity_API.Services
{
    public class RequestService : IRequestService
    {
        private readonly IdentityContext _context;
        public RequestService(IdentityContext context)
        {
            _context = context;
        }
        public async Task<List<Request>> GetRequests()
        {
            return await _context.Request.Select(u => new Request
            {
                id = u.id,
                creationDate = u.creationDate,
                text = u.text,
                userId = u.userId,
            }).ToListAsync();
        }

        public async Task<RequestViewModel> GetRequestById(int id)
        {
            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return null;
            }

            return new RequestViewModel
            {
                text = request.text,
                creationDate = request.creationDate,
                userId = request.userId
            };
        }

        public async Task<bool> UpdateRequest(int id, RequestDTOUpdate request)
        {
            var entity = await _context.Request.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(request.text))
            {
                entity.text = request.text;
            }

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int?> PostRequest(RequestDTO request)
        {
            if (request == null)
            {
                return null;
            }

            var newRequest = new Request
            {
                userId = request.userId,
                text = request.text
            };

            _context.Request.Add(newRequest);

            await _context.SaveChangesAsync();

            return newRequest.id;
        }

        public async Task<int?> DeleteRequest(int id)
        {
            var entity = _context.Request.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            var user = await _context.User.FindAsync((await entity).userId);
            if (user != null)
            {
                return -1;
            }

            _context.Request.Remove(await entity);

            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<List<RequestViewModel>> GetRequestByUser(int id)
        {
            User user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return await _context.Request.Where(r => r.userId == id)
                                         .Select(u => new RequestViewModel
                                         {
                                             creationDate = u.creationDate,
                                             text = u.text,
                                             userId = u.userId,
                                         }).ToListAsync();
                                         
        }
    }
}
