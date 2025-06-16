using Identity_API.Model;
using Identity_API.Model.DTO;
using Identity_API.Model.Interfaces;
using Identity_API.Model.ViewModel;
using Identity_Service.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Identity_API.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IdentityContext _context;
        public UserRoleService(IdentityContext context)
        {
            _context = context;
        }

        public async Task<List<UserRole>> GetAllUserRoles()
        {
            return await _context.UserRoles.Select(ur => new UserRole
            {
                Id = ur.Id,
                UserId = ur.UserId,
                RoleId = ur.RoleId,
                StartDate = ur.StartDate,
                EndDate = ur.EndDate,
            }).ToListAsync();
        }

        public async Task<UserRoleViewModel> GetUserRoleById(int id)
        {
            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole == null)
            {
                return null;
            }

            return new UserRoleViewModel
            {
                UserId = userRole.UserId,
                RoleId = userRole.RoleId,
                StartDate = userRole.StartDate,
                EndDate = userRole.EndDate,
            };
        }

        public async Task<bool> UpdateUserRole(int id, UserRoleDTOUpdate userRole)
        {
            var entity = await _context.UserRoles.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            //if (userRole.StartDate.HasValue)
            //{
            //    entity.StartDate = DateTime.UtcNow;
            //}
            if (userRole.EndDate.HasValue)
            {
                entity.EndDate = userRole.EndDate;
            }

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int?> PostUserRole(UserRoleDTO userRole)
        {
            if (userRole == null)
            {
                return null;
            }

            User user = await _context.User.FindAsync(userRole.UserId);
            if ( user == null )
            {
                return -1;
            }

            Role role = await _context.Role.FindAsync(userRole.RoleId);
            if (role == null)
            {
                return -2;
            }

            var newUserRole = new UserRole
            {
                UserId = userRole.UserId,
                RoleId = userRole.RoleId,
            };

            _context.UserRoles.Add(newUserRole);

            await _context.SaveChangesAsync();

            return newUserRole.Id;
        }

        public async Task<int?> DeleteUserRole(int id)
        {
            var entity = _context.UserRoles.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            _context.UserRoles.Remove(await entity);

            await _context.SaveChangesAsync();

            return id;
        }
    }
}