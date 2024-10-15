namespace ST10052874_PROG7312_POE.Models
{
    public class Event
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int EventID {  get; set; }
    }
    public class SearchHistory
    {
        public List<Event> PreviousSearches { get; set; }

        public SearchHistory()
        {
            PreviousSearches = new List<Event>();
        }
    }
}
