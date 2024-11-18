using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ST10052874_PROG7312_POE.Models;

namespace ST10052874_PROG7312_POE.Pages
{
    public class Status1Model : PageModel
    {
        // Binary Search Tree for managing service requests
        public BinarySearchTree<Issue> IssueTree { get; set; } = new BinarySearchTree<Issue>();
        public MinHeap UnresolvedHeap { get; set; } = new MinHeap();

        public List<Issue> DisplayedIssues { get; set; } = new List<Issue>();

        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public void OnGet(string filter = null, string sort = null)
        {
            var sampleIssues = new List<Issue>
            {
                new Issue { location = "Home Affairs", category = "Plumbing", description = "Leaking tap", status = "Unresolved" },
                new Issue { location = "Town Hall", category = "Electrical", description = "Power outage", status = "Resolved" },
                new Issue { location = "Baywest", category = "HVAC", description = "AC not working", status = "Unresolved" }
            };
            foreach (var item in PrivacyModel.issueList)
            {
                sampleIssues.Add(item);
            }

            foreach (var issue in sampleIssues)
            {
                IssueTree.Insert(issue);
                if (issue.status == "Unresolved")
                {
                    UnresolvedHeap.Add(issue); 
                }
            }

            if (!string.IsNullOrEmpty(filter))
            {
                CurrentFilter = filter;
                if (filter == "Unresolved")
                {
                    DisplayedIssues = new List<Issue>(UnresolvedHeap.GetAll());
                }
                else
                {
                    var allIssues = new List<Issue>();
                    IssueTree.InOrderTraversal(IssueTree.Root, allIssues);
                    DisplayedIssues = allIssues.FindAll(i => i.status == "Resolved");
                }
            }
            else
            {
                IssueTree.InOrderTraversal(IssueTree.Root, DisplayedIssues);
            }

            if (!string.IsNullOrEmpty(sort))
            {
                CurrentSort = sort;
                DisplayedIssues.Sort(sort switch
                {
                    "Location" => (a, b) => a.location.CompareTo(b.location),
                    "Category" => (a, b) => a.category.CompareTo(b.category),
                    "Status" => (a, b) => a.status.CompareTo(b.status),
                    _ => (a, b) => 0
                });
            }
        }
    }
}