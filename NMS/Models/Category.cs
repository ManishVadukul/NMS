using System.ComponentModel.DataAnnotations;

namespace NMS.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } 
    }
}
