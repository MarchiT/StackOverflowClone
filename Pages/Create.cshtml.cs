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
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager) {
            _db = db;
            _userManager = userManager;
        }

        [BindProperty]
        public Question Question { get; set; }


        public Task<ApplicationUser> GetCurrentUser() => _userManager.GetUserAsync(HttpContext.User);
        

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) {
                return Page();
            }

            var user = await GetCurrentUser();
            user.Points += 5;
            user.Questions.Add(Question);            
            await _db.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

    }
}