using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StackOverflowClone.Data;

namespace StackOverflowClone.Pages.Tags
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public string Parameter { get; private set; }
        public IList<Tag> Tags { get; private set; }
        public IList<QuestionTag> QuestionTags { get; private set; }

        public async Task OnGet(string name)
        {
            Tags = await _context.Tags
                .Include(t => t.QuestionTags)
                    .ThenInclude(qt => qt.Question)
                        .ThenInclude(q => q.Publisher)
                .AsNoTracking()
                .ToListAsync();

            if (name != null)
            {
                Parameter = name;
                var SelectedTag = Tags.SingleOrDefault(t => t.Name == name);
                
                if (SelectedTag != null)
                    QuestionTags = SelectedTag.QuestionTags.ToList();
                else
                    QuestionTags = new List<QuestionTag>();
            }    
        }
    }
}