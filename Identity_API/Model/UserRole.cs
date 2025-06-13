namespace Identity_API.Model
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime? EndDate { get; set; } 
    }
}
