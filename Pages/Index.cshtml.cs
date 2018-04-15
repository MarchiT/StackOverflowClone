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

        public async Task OnGetAsync()
        {
            Questions = await _context.Questions
                .Include(q => q.Publisher)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
