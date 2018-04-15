using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StackOverflowClone.Data
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Questions = new HashSet<Question>();
            this.Answers = new HashSet<Answer>();
            this.Comments = new HashSet<Comment>();
        }
        public int Points { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
