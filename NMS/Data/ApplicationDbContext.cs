using Microsoft.EntityFrameworkCore;
using NMS.Models;

namespace NMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

         public DbSet<Category> Categories { get; set; }
         public DbSet<Note> Notes { get; set; }
        
    }
}
