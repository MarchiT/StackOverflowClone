using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StackOverflowClone.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace StackOverflowClone.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DetailsModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Question Question { get; private set; }
        [BindProperty]
        public Comment Comment { get; set; 
        }
        public async Task<IActionResult> OnGetAsync(int? id) 
        {    
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Questions
                .Include(q => q.Publisher)
                .Include(q => q.Answers)
                    .ThenInclude(a => a.Comments)
                .Include(c => c.Comments)
                    .ThenInclude(c => c.Question)
                .Include(c => c.Comments)
                    .ThenInclude(c => c.Publisher)
                .Include(p => p.QuestionTags)
                    .ThenInclude(pq => pq.Tag)
                .SingleOrDefaultAsync(m => m.Id == id);

            
            if (Question == null) 
            {
                return NotFound();
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostCommentAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                return RedirectToPage(errors[0]);
            }
            
            _context.Comments.Add(Comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}