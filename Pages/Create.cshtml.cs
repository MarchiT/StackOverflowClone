using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StackOverflowClone.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace StackOverflowClone.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager) {
            _context = context;
            _userManager = userManager;
        }
        
        public List<Tag> Tagslist { get; set; }
        public IActionResult OnGet()
        {

            Tagslist = _context.Tags.ToList();
            return Page();
        }
      

        [BindProperty]
        public Question Question { get; set; }
        [BindProperty]
        public int TagId { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) 
            {
                return Page();
            }

            var user = await GetCurrentUser();
            user.Points += 5;
            Tag tag = _context.Tags.First(t =>t.Id == TagId);
            QuestionTag qt = new QuestionTag {
                QuestionId =Question.Id,
                Question = Question,
                TagId = tag.Id,
                Tag = tag

            };
            Question.QuestionTags.Add(qt);
            user.Questions.Add(Question);
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private Task<ApplicationUser> GetCurrentUser() 
            => _userManager.GetUserAsync(HttpContext.User);        
    }
}