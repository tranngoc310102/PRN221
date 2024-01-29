using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Filter.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Filter.Pages.Stu
{
    public class IndexModel : PageModel
    {
        private readonly Filter.Models.PRN211_1Context _context;

        public IndexModel(Filter.Models.PRN211_1Context context)
        {
            _context = context;
        }

        public IList<Student> Student { get; set; } = default!;

        public async Task OnGetAsync(string? sgender, string? sdepart, int? sgpa)
        {
            if (_context.Students != null)
            {
				ViewData["depart"] = new SelectList(_context.Departments, "Id", "Name");
				if (sgender != null || sdepart != null || sgpa != null)
                {
					IQueryable<Student> query = _context.Students.Include(s => s.Depart);

					if (!string.IsNullOrEmpty(sgender) && !sgender.Equals("None"))
					{
						bool gender = bool.Parse(sgender);
						query = query.Where(x => x.Gender == gender);
					}

					if (!string.IsNullOrEmpty(sdepart) && !sdepart.Equals("None"))
					{
						query = query.Where(x => x.Depart.Id == sdepart);
					}

					if (sgpa.HasValue && !sgpa.Equals("None"))
					{
						query = query.Where(x => x.Gpa <= sgpa.Value);
					}

					// Execute the query and get the results
					Student = await query.ToListAsync();
				}
                else
                {
                    Student = await _context.Students
                    .Include(s => s.Depart).ToListAsync();
                }
            }
        }
    }
}
