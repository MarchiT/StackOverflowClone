using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StackOverflowClone.Data
{
    public class Tag
    {
        public Tag()
        {
            this.QuestionTags = new HashSet<QuestionTag>();
        }
        public int Id { get; set;}

        [Required, StringLength(100)]
        public string Name { get; set; }
        public ICollection<QuestionTag> QuestionTags { get; set; }
    }
}