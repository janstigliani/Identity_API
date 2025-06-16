using Identity_API.Model;
using Identity_API.Model.DTO;
using Identity_API.Model.Interfaces;
using Identity_API.Model.ViewModel;
using Identity_Service.Model;
using Microsoft.EntityFrameworkCore;

namespace Identity_API.Services
{
    public class RoleService : IRoleService
    {
        private readonly IdentityContext _context;
        public RoleService(IdentityContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetRoles()
        {
            return await _context.Role.Select(u => new Role
            {
                id = u.id,
                name = u.name,
                description = u.description,
            }).ToListAsync();
        }

        public async Task<RoleViewModel> GetRoleById(int id)
        {
            var role = await _context.Role.FindAsync(id);
            if (role == null)
            {
                return null;
            }

            return new RoleViewModel
            {
                name = role.name,
                description = role.description,
            };
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

        public async Task<List<UserViewModel>> GetUsersByRoleId(int Id)
        {
            var entityList = await _context.UserRoles
                .Where(ur => ur.RoleId == Id).ToListAsync();

            if (entityList.Count == 0)
            {
                return null;
            }

            var result = new List<UserViewModel>();

            foreach (var entity in entityList)
            {
                var user = await _context.User.FindAsync(entity.UserId);

                if (user == null)
                {
                    continue;
                }

                UserViewModel newUser = new UserViewModel
                {
                    name = user.name,
                    surname = user.surname,
                    email = user.email
                };

                result.Add(newUser);
            }

            return result;
        }
    }
}