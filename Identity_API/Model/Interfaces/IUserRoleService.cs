using Identity_API.Model.DTO;
using Identity_API.Model.ViewModel;
using Identity_Service.Model;

namespace Identity_API.Model.Interfaces
{
    public interface IUserRoleService
    {
        public Task<List<UserRole>> GetAllUserRoles();
        public Task<UserRoleViewModel> GetUserRoleById(int id);
        public Task<bool> UpdateUserRole(int id,UserRoleDTOUpdate userRole);
        public Task<int?> PostUserRole(UserRoleDTO userRole);
        public Task<int?> DeleteUserRole(int id);
    }
}