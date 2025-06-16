namespace Identity_API.Model
{
    public class Role
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public virtual ICollection<UserRole> userRoles { get; set; }
    }
}
