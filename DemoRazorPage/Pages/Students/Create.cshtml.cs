using DemoRazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoRazorPage.Pages.Students
{
    public class CreateModel : PageModel
    {
        public List<Department> listDepart { get; private set; }
        private readonly PRN211_1Context _context;
        public CreateModel(PRN211_1Context context)
        {
            _context = context;
        }
        [BindProperty]
        public Student student { get; set; }
        public void OnGet()
        {
            listDepart = _context.Departments.ToList();
            ViewData["depart"] = new SelectList(_context.Departments,"Id", "Name");
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var x = _context.Students.Find(student.Id);
                if (x != null)   return RedirectToAction("/Students/Index");
                _context.Students.Add(student);
                _context.SaveChanges();
            }
            return Redirect("/Students/Index");
        }
    }
}
