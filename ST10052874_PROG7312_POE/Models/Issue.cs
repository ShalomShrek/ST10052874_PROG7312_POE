namespace ST10052874_PROG7312_POE.Models
{
    public class Issue : IComparable<Issue>
    {
        public string location { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public IFormFile mediaAttachment { get; set; }

        public string mediaFilePath { get; set; }
        public int CompareTo(Issue other)
        {
            if (other == null) return 1;
            return string.Compare(this.location, other.location, StringComparison.Ordinal);
        }
    }
}
