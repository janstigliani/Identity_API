using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity_API.Model;

namespace Identity_Service.Model
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public virtual ICollection<Request> requests { get; set; }
        public virtual ICollection<UserRole> userRoles { get; set; }
    }
}
