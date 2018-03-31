using System.ComponentModel.DataAnnotations;

namespace StackOverflowClone.Data
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Body { get; set; }
    }
}