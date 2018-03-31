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
            this.Answers = new HashSet<Answer>();
            this.Tags = new HashSet<Tag>();

            this.State = State.Open;
        }

        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public int Vote { get; set; }
        public State State { get; set; }

        public string PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public virtual ApplicationUser Publisher { get; set; }


        public virtual ICollection<Answer> Answers { get; set; }
        public ICollection<Tag> Tags { get; set; }

    }

}