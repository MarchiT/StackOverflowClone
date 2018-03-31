using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StackOverflowClone.Data;
using Microsoft.AspNetCore.Identity;

namespace StackOverflowClone.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IList<Question> Questions { get; private set; }


        public async Task OnGetAsync()
        {
            Questions = await _db.Questions.AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var question = await _db.Questions.FindAsync(id);

            if (question != null)
            {
                question.Answers.Clear();
                _db.Questions.Remove(question);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
