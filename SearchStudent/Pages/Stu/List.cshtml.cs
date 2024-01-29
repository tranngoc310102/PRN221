using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SearchStudent.Models;

namespace SearchStudent.Pages.Stu
{
    public class ListModel : PageModel
    {
        private readonly SearchStudent.Models.PRN211_1Context _context;

        public ListModel(SearchStudent.Models.PRN211_1Context context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync(string? key)
        {
            if (_context.Students != null)
            {
                if (key != null)
                {
                    Student = await _context.Students.Include(x => x.Depart).Where(s => s.Depart.Name.Contains(key) || s.Name.Contains(key)).ToListAsync();
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
