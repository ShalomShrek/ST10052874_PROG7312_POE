using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.ML;
using ST10052874_PROG7312_POE.Models;

namespace ST10052874_PROG7312_POE.Pages
{
    public class LocalEventsModel : PageModel
    {
        public LocalEventsViewModel ViewModel { get; set; }
        private Dictionary<string, int> searchPatterns = new Dictionary<string, int>();
        private readonly SearchHistory _searchHistory;
        private MLContext _mlContext;
        private ITransformer _model;

        public LocalEventsModel()
        {
            ViewModel = new LocalEventsViewModel
            {
                EventsQueue = new Queue<Event>(),
                EventsByDate = new SortedDictionary<DateTime, Queue<Event>>(),
                Categories = new HashSet<string>(),
                FilteredEvents = new List<Event>(),
                RecommendedEvents = new List<Event>()
            };
            _searchHistory = new SearchHistory();
            _mlContext = new MLContext();
            TrainRecommendationModel();
            LoadEvents();
            FilterEvents();
        }

        public void LoadEvents()
        {
            ViewModel.EventsQueue.Enqueue(new Event { Title = "Music Concert", Date = DateTime.Now.AddDays(2), Category = "Music", Description = "Enjoy a night of exciting music from many talented local artists, taking place at Grey High", EventID = 1 });
            ViewModel.EventsQueue.Enqueue(new Event { Title = "Art Exhibition", Date = DateTime.Now.AddDays(5), Category = "Art", Description = "Showcasing many incredible pieces made by students across the country, only at Varsity College PE", EventID = 2 });
            ViewModel.EventsQueue.Enqueue(new Event { Title = "Community Meetup", Date = DateTime.Now.AddDays(7), Category = "Community", Description = "Discussion of current issues which require action, taking place at the Town Hall", EventID = 3 });
            ViewModel.EventsQueue.Enqueue(new Event { Title = "Music Festival", Date = DateTime.Now.AddDays(10), Category = "Music", Description = "Enjoy a night of incredible music from MGK, Edd Shearan, and Lady Gaga, taking place at PE Stadium", EventID = 4 });
            ViewModel.EventsQueue.Enqueue(new Event { Title = "Potential Power outage", Date = DateTime.Now.AddDays(10), Category = "Announcement", Description = "Maintenance will be carried out on the main sub-station for 3 days, which may result in power outages.", EventID = 5 });
            ViewModel.EventsQueue.Enqueue(new Event { Title = "Painting Competition", Date = DateTime.Now.AddDays(5), Category = "Art", Description = "Taking place at Pearson, the painter who creates the best piece within 30 minutes will win R5000.", EventID = 6 });
            ViewModel.EventsQueue.Enqueue(new Event { Title = "Community Cleanup", Date = DateTime.Now.AddDays(7), Category = "Community", Description = "A cleanup of Hobie beach, starting at 8 AM.", EventID = 7 });
            ViewModel.EventsQueue.Enqueue(new Event { Title = "Potential Power outage", Date = DateTime.Now.AddDays(10), Category = "Announcement", Description = "Maintenance will be carried out on the water plant filtering system, which may result in loss of water to certain areas for 3-5 days.", EventID = 8 });



            while (ViewModel.EventsQueue.Count > 0)
            {
                Event currentEvent = ViewModel.EventsQueue.Dequeue();

                if (!ViewModel.EventsByDate.ContainsKey(currentEvent.Date))
                {
                    ViewModel.EventsByDate[currentEvent.Date] = new Queue<Event>();
                }
                ViewModel.EventsByDate[currentEvent.Date].Enqueue(currentEvent);
                ViewModel.Categories.Add(currentEvent.Category);
            }
        }

        public void OnGet()
        {
            FilterEvents();
        }

        public void OnPostSearch(string searchTerm, string selectedCategory, DateTime? startDate, DateTime? endDate)
        {
            TrackSearch(selectedCategory);

            ViewModel.SearchTerm = searchTerm;
            ViewModel.SelectedCategory = selectedCategory;
            ViewModel.StartDate = startDate;
            ViewModel.EndDate = endDate;
            FilterEvents();

            ShowRecommendations();
        }

        private void TrainRecommendationModel()
        {
            var searchData = new List<SearchData>
            {
                new SearchData { UserId = 1, EventId = 1, Label = 1 }, 
                new SearchData { UserId = 1, EventId = 2, Label = 0 }, 
                new SearchData { UserId = 2, EventId = 3, Label = -1 },
                new SearchData { UserId = 2, EventId = 4, Label = -1 },
                new SearchData { UserId = 2, EventId = 5, Label = -1 },
                new SearchData { UserId = 2, EventId = 6, Label = -1 },
                new SearchData { UserId = 2, EventId = 7, Label = -1 },
                new SearchData { UserId = 2, EventId = 8, Label = -1 },
            };

            var searchDataView = _mlContext.Data.LoadFromEnumerable(searchData);

            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("UserId")
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("EventId"))
                .Append(_mlContext.Recommendation().Trainers.MatrixFactorization(
                    labelColumnName: "Label",
                    matrixColumnIndexColumnName: "UserId",
                    matrixRowIndexColumnName: "EventId",
                    numberOfIterations: 20,       
                    learningRate: 0.01f,          
                    approximationRank: 100        
                    ));

            _model = pipeline.Fit(searchDataView);
        }

        public void ShowRecommendations()
        {
            var userId = 1;
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<SearchData, EventPrediction>(_model);

            ViewModel.RecommendedEvents = new List<Event>();

            var availableEvents = ViewModel.FilteredEvents;

            foreach (var eventData in availableEvents)
            {
                var prediction = predictionEngine.Predict(new SearchData { UserId = userId, EventId = eventData.EventID });
                Console.WriteLine($"Event ID: {eventData.EventID}, Score: {prediction.Score}");
                if (prediction.Score > 0.16) 
                {
                    ViewModel.RecommendedEvents.Add(eventData);
                }
            }
        }

        private void TrackSearch(string category)
        {
            if (!searchPatterns.ContainsKey(category))
                searchPatterns[category] = 0;

            searchPatterns[category]++;
            _searchHistory.PreviousSearches.Add(new Event { Category = category });
        }

        

        public void FilterEvents()
        {
            var filteredEvents = ViewModel.EventsByDate
                .SelectMany(e => e.Value)
                .Where(ev =>
                    (string.IsNullOrEmpty(ViewModel.SearchTerm) || ev.Title.ToLower().Contains(ViewModel.SearchTerm.ToLower())) &&
                    (string.IsNullOrEmpty(ViewModel.SelectedCategory) || ViewModel.SelectedCategory == "All Categories" || ev.Category == ViewModel.SelectedCategory) &&
                    (!ViewModel.StartDate.HasValue || ev.Date >= ViewModel.StartDate.Value) &&  
                    (!ViewModel.EndDate.HasValue || ev.Date <= ViewModel.EndDate.Value))        
                .ToList();

            ViewModel.FilteredEvents = filteredEvents;
        }
    }
    public class SearchData
    {
        public float UserId { get; set; }
        public float EventId { get; set; }
        public float Label { get; set; } 
    }

    public class EventPrediction
    {
        public float Score { get; set; } 
    }
}
