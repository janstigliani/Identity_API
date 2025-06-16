using Identity_API.Model.DTO;
using Identity_API.Model.ViewModel;
using Identity_Service.Model;

namespace Identity_API.Model.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> GetUsers();
        public Task<UserViewModel> GetUserById(int id);
        public Task<bool> UpdateUser(int id,UserDTOUpdate user);
        public Task<int?> PostUsers(UserDTO user);
        public Task<int?> DeleteUsers(int id);
        public Task<List<RoleViewModel>> GetPersonalRolesByUserId(int UserId);
    }
}
