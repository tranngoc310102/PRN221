using DemoRazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoRazorPage.Pages.Students
{
    public class UpdateModel : PageModel
    {
		private readonly PRN211_1Context _context;
		public UpdateModel(PRN211_1Context context)
		{
			_context = context;
		}
		[BindProperty]
		public Student student { get; set; }
		public void OnGet(string? sid)
        {
            if (sid != null)
            {
                try
                {
                    int id = int.Parse(sid);
                    student = _context.Students.Find(id);
                    ViewData["depart"] = new SelectList(_context.Departments, "Id", "Name");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else Console.WriteLine("sid must not null");
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
            }
			return Redirect("/Students/Index");
		}
    }
}
