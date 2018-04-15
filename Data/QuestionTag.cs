using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowClone.Data
{
    public class QuestionTag
    {
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        public int TagId { get; set; }
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}