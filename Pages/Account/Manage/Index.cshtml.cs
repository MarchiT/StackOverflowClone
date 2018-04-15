using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StackOverflowClone.Data;
using StackOverflowClone.Services;

namespace StackOverflowClone.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Question> Questions { get; private set; }
        public IList<Answer> Answers { get; private set; }
        public int Points { get; private set; }
        public int Rank { get; private set; }
        public int UsersCount { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _context.Users
                .Include(u => u.Questions)
                .Include(u => u.Answers)
                    .ThenInclude(a => a.Question)
                .SingleAsync(u => u.UserName.Equals(User.Identity.Name));
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{User.Identity.Name}'.");
            }
            
            Questions = user.Questions.ToList();
            Answers = user.Answers.ToList();
            Points = user.Points;
            Rank = user.Points; //TODO calculate this when the Leaderboard is done
            UsersCount = _context.Users.Count();

            return Page();
        }
    }
}
