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

        [TempData]
        public int QuestionId { get; set; }
        [BindProperty]
        public Answer Answer { get; set; }
        [BindProperty]
        public Comment Comment { get; set; }
        
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

            QuestionId = Question.Id;
            
            return Page();
        }

        public async Task<IActionResult> OnPostAnswerAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Index");
            }

            Answer.Question = await GetQuestionAsync();
            var user = await GetUserAsync();
            user.Answers.Add(Answer);
            await _context.SaveChangesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostCommentAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Comment.Question = await GetQuestionAsync();
            var user = await GetUserAsync();
            user.Comments.Add(Comment);
            await _context.SaveChangesAsync();

            return Page();
        }

        private Task<Question> GetQuestionAsync() => _context.Questions.SingleOrDefaultAsync(q => q.Id == QuestionId);
        private Task<ApplicationUser> GetUserAsync() => _userManager.GetUserAsync(User);
    }
}