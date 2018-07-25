using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RajUniEFCoreRP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RajUniEFCoreRP.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;

        public IndexModel(SchoolContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; }

        public async Task OnGetAsync()
        {
            Student = await _context.Student.ToListAsync();
        }
    }
}
