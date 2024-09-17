namespace ST10052874_PROG7312_POE.Models
{
    public class Issue
    {
        public string location { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public IFormFile mediaAttachment { get; set; }
    }
}
