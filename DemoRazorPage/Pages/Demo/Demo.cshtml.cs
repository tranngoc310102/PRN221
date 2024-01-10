using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoRazorPage.Pages.Demo
{
    public class DemoModel : PageModel
    {
        public String Name { get; set; }
        public void OnGet()
        {
            //ViewData["Name"] = "NHHH";
            Name = "TTTTT123";
        }
    }
}
