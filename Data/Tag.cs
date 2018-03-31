
using System.ComponentModel.DataAnnotations;

namespace StackOverflowClone.Data
{
    public class Tag
    {
        [Key]
        public int Id { get; set;}

        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}