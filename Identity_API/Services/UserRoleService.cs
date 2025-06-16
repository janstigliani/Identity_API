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

        public async Task<bool> UpdateRole(int id, RoleDTOUpdate role)
        {
            var entity = await _context.Role.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(role.name))
            {
                entity.name = role.name;
            }

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int?> PostRole(RoleDTO role)
        {
            if (role == null)
            {
                return null;
            }

            var newRole = new Role
            {
                description = role.description,
                name = role.name
            };

            _context.Role.Add(newRole);

            await _context.SaveChangesAsync();

            return newRole.id;
        }

        public async Task<int?> DeleteRole(int id)
        {
            var entity = _context.Role.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            _context.Role.Remove(await entity);

            await _context.SaveChangesAsync();

            return id;
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
            if (userRole.StartDate.HasValue)
            {
                entity.StartDate = userRole.StartDate;
            }

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }

        public Task<int?> PostUserRole(UserRoleDTO userRole)
        {
            throw new NotImplementedException();
        }

        public Task<int?> DeleteUserRole(int id)
        {
            throw new NotImplementedException();
        }
    }
}