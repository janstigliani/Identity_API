using Identity_API.Model.DTO;
using Identity_API.Model.Interfaces;
using Identity_API.Model.ViewModel;
using Identity_Service.Model;
using Microsoft.EntityFrameworkCore;

namespace Identity_API.Services
{
    public class UserService : IUserService
    {
        private readonly IdentityContext _context;
        public UserService(IdentityContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.User.Select(u => new User
            {
                id = u.id,
                surname = u.surname,
                name = u.name,
                password = u.password,
                email = u.email
            }).ToListAsync();
        }

        public async Task<UserViewModel> GetUserById(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            return new UserViewModel
            {
                name = user.name,
                surname = user.surname,
                email = user.email
            };
        }

        public async Task<bool> UpdateUser(int id, UserDTOUpdate user)
        {
            var entity = await _context.User.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(user.name))
            {
                entity.name = user.name;
            }

            if (!string.IsNullOrWhiteSpace(user.surname))
            {
                entity.surname = user.surname;
            }

            if (!string.IsNullOrWhiteSpace(user.email))
            {
                entity.email = user.email;
            }

            if (!string.IsNullOrWhiteSpace(user.password))
            {
                entity.password = user.password;
            }

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int?> PostUsers(UserDTO user)
        {
            if (user == null)
            {
                return null;
            }

            var newUser = new User
            {
                name = user.name,
                surname = user.surname,
                email = user.email,
                password = user.password
            };

            _context.User.Add(newUser);

            await _context.SaveChangesAsync();

            return newUser.id;
        }

        public async Task<int?> DeleteUsers(int id)
        {
            var entity = _context.User.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            _context.User.Remove(await entity);

            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<List<RoleViewModel>> GetPersonalRolesByUserId(int UserId)
        {
            var entityList = await _context.UserRoles
                .Where(ur => ur.UserId == UserId).ToListAsync();

            if (entityList.Count == 0)
            {
                return null;
            }

            var result = new List<RoleViewModel>();

            foreach (var entity in entityList)
            {
                var role = await _context.Role.FindAsync(entity.RoleId);

                if (role == null)
                {
                    continue;
                }

                RoleViewModel newRole = new RoleViewModel
                {
                    name = role.name,
                    description = role.description,
                };

                result.Add(newRole);
            }

            return result;
        }
    }
}
