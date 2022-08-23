using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMS.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; } 
        [Required]
        public int CategoryId { get; set; }

        //[ForeignKey("CategoryId")]
        //public Category Category { get; set; }
                        
        public virtual Category Category { get; set; }

        //[NotMapped]
        //public SelectList CategoryList { get; set; } //Category Name
        [NotMapped]
        public string CategoryName { get; set; }

    }
}
