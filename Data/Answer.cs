using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowClone.Data
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Body { get; set; }
        public int Vote { get; set; }

        public string PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public virtual ApplicationUser Publisher { get; set; }

        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}