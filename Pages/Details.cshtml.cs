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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public DetailsModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        public Question Question { get; private set; }
        public ApplicationUser Publisher { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id) 
        {    
            Question = await _db.Questions.FindAsync(id);
            
            if (Question == null) 
            {
                return RedirectToPage("/Index");
            }

            Publisher = await _userManager.FindByIdAsync(Question.PublisherId);
            
            return Page();
        }
    }
}