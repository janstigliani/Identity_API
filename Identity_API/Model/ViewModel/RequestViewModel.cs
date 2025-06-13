using Identity_Service.Model;

namespace Identity_API.Model.ViewModel
{
    public class RequestViewModel
    {
        public DateTime creationDate { get; set; }
        public int userId { get; set; }
        public string text { get; set; }
    }
}
