using DemoRazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoRazorPage.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly PRN211_1Context _context;
        public List<Student> student { get; set; }

        public IndexModel(PRN211_1Context context)
        {
            _context = context;
        }
        public void OnGet()
        {
            student = _context.Students.Include(x => x.Depart).ToList();
        }
    }
}
