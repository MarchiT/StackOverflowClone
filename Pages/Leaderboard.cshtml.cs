using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using StackOverflowClone.Data;

namespace StackOverflowClone.Pages
{
    public class LeaderboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LeaderboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Competitor> Users { get; private set; }

        public IActionResult OnGet()
        {
            Users = Leaderboard.Calculate(_context);

            return Page();
        }
    }
}
