using Microsoft.AspNetCore.Mvc.Rendering;
using NMS.Models;

namespace NMS.ViewModels
{
    public class NotesVM
    {
        public Note Note { get; set; }
        public Category Category { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
