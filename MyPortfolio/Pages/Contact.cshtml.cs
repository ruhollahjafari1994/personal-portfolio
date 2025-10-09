using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyPortfolio.Pages
{
    public class ContactModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // TODO: Handle form submission (e.g., send email)
            return RedirectToPage("/Index");
        }
    }
}
