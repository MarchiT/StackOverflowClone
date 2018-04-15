using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowClone.Data
{
    public enum State
    {
        Open, Closed
    }

    public class Question
    {
        public Question()
        {
            this.QuestionTags = new HashSet<QuestionTag>();
            this.Answers = new HashSet<Answer>();
            this.Comments = new HashSet<Comment>();

            this.State = State.Open;
        }

        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public int Vote { get; set; }
        public State State { get; set; }

        public ApplicationUser Publisher { get; set; }
        public ICollection<QuestionTag> QuestionTags { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }

}