using System.ComponentModel.DataAnnotations;

namespace StackOverflowClone.Data
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public ApplicationUser Publisher { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}