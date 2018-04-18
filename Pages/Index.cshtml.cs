using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StackOverflowClone.Data;

namespace StackOverflowClone.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Question> Questions { get; private set; }

        public async Task<IActionResult> OnGetAsync(string state)
        {
            Questions = await _context.Questions
                .Include(q => q.Publisher)
                .AsNoTracking()
                .ToListAsync();
            
            if (state == null)
            {
                return Page();
            } 
            else if (Enum.TryParse(state, out State State))
            {
                Questions = Questions.Where(q => q.State == State).ToList();
                return Page();
            }
            else
            {
                return NotFound();                
            }
        }
    }
}
