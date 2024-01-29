using DemoRazorPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoRazorPage.Pages.Students
{
	public class DeleteModel : PageModel
	{

		private readonly PRN211_1Context _context;
		public DeleteModel(PRN211_1Context context)
		{
			_context = context;
		}

		[BindProperty]
		public Student student { get; set; }
		public void OnGet(string sid)
		{
			if (sid != null)
			{
				try
				{
					int id = int.Parse(sid);
					student = _context.Students.Include(x => x.Depart).FirstOrDefault(x => x.Id == id);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}
			else Console.WriteLine("sid must not null");
		}

		public IActionResult OnPost(string sid)
		{
			if (sid != null)
			{
				try
				{
					int id = int.Parse(sid);
					student = _context.Students.FirstOrDefault(x => x.Id == id);
					_context.Students.Remove(student);
					_context.SaveChanges();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}
			else Console.WriteLine("sid must not null");
			return RedirectToPage("/Students/Index");
		}
	}

}
