using Identity_API.Model.DTO;
using Identity_API.Model.ViewModel;
using Identity_Service.Model;

namespace Identity_API.Model.Interfaces
{
    public interface IRoleService
    {
        public Task<List<Role>> GetRoles();
        public Task<RoleViewModel> GetRoleById(int id);
        public Task<bool> UpdateRole(int id,RoleDTOUpdate role);
        public Task<int?> PostRole(RoleDTO role);
        public Task<int?> DeleteRole(int id);
        public Task<List<UserViewModel>> GetUsersByRoleId(int Id);
    }
}