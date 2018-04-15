using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowClone.Data
{
    public class Answer
    {
        public Answer()
        {
            Comments = new HashSet<Comment>();
        }
        
        public int Id { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        public int Vote { get; set; }

        public ApplicationUser Publisher { get; set; }
        public Question Question { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}