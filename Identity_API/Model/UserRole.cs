using Identity_Service.Model;

namespace Identity_API.Model
{
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}