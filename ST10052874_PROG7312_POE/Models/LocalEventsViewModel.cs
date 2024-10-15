namespace ST10052874_PROG7312_POE.Models
{
    public class LocalEventsViewModel
    {
        public Queue<Event> EventsQueue { get; set; }
        public SortedDictionary<DateTime, Queue<Event>> EventsByDate { get; set; }
        public HashSet<string> Categories { get; set; }

        public string SearchTerm { get; set; }
        public string SelectedCategory { get; set; }

        public DateTime? StartDate { get; set; }  
        public DateTime? EndDate { get; set; }   

        public List<Event> FilteredEvents { get; set; }
        public List<Event> RecommendedEvents { get; set; }
    }
}
