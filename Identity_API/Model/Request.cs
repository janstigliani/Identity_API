using System.Numerics;
using Identity_Service.Model;

namespace Identity_API.Model
{
    public class Request
    {
        public int id { get; set; }
        public DateTime creationDate { get; set; }
        public int userId { get; set; }
        public string text { get; set; }
        public virtual User user { get; set; }
    }
}
