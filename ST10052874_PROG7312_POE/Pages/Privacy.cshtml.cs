using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ST10052874_PROG7312_POE.Models;

namespace ST10052874_PROG7312_POE.Pages
{
    public class PrivacyModel : PageModel
    {
        public static List<Issue> issueList = new List<Issue>(); //used to store all submitted reports
        public List<Issue> Issues { get; set; } //used to display all submitted reports

        [BindProperty]//forces field to be filled out
        public string Location { get; set; }

        [BindProperty]//forces field to be filled out
        public string Category { get; set; }

        [BindProperty]//forces field to be filled out
        public string Description { get; set; }

        [BindProperty]//forces field to be filled out
        public IFormFile MediaAttachment { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (MediaAttachment != null && MediaAttachment.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", MediaAttachment.FileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));//temporarily saves the file to uploads folder to fetch later if needed, is lost upon temination of application

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await MediaAttachment.CopyToAsync(stream);
                }
            }
            issueList.Add(new Issue
            {
                location = this.Location,
                category = this.Category,
                description = this.Description,
                mediaAttachment = this.MediaAttachment,
                status = "Unresolved"
            }
            );
            OnGet();
            return Page();
        }
        public void OnGet()
        {
            Issues = issueList;
        }
    }

}
