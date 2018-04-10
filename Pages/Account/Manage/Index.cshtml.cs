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
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        public IList<Question> Questions { get; private set; }
        public IList<Answer> Answers { get; private set; }
        public int Points { get; private set; }
        public int Rank { get; private set; }
        public int UsersCount { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            
            Questions = await _db.Questions.AsNoTracking().ToListAsync();
            Questions = Questions.Where(q => q.PublisherId.Equals(user.Id)).ToList();

            // Questions = user.Questions;
            Answers = user.Answers.ToList();
            Points = user.Points;
            Rank = user.Points;
            UsersCount = _db.Users.Count();

            return Page();
        }
    }
}
