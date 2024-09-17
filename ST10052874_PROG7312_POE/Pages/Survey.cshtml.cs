using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ST10052874_PROG7312_POE.Models;

namespace ST10052874_PROG7312_POE.Pages
{
    public class SurveyModel : PageModel
    {
        public static List<Survey> SurveysList = new List<Survey>();//stores all submitted surveys
        public List<Survey> Surveys {  get; set; } //used to display all submitted surveys
        [BindProperty]//forces field to be filled out
        public string SurveyReason { get; set; }

        [BindProperty]//forces field to be filled out
        public string Information { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SurveysList.Add(new Survey
            {
                surveyReason = this.SurveyReason,
                information = this.Information
            }
            );

            OnGet();
            return Page();
        }
        public void OnGet()
        {
            Surveys = SurveysList;
        }
    }
}
