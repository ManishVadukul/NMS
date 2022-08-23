using Microsoft.EntityFrameworkCore;
using NMS.Data;
using NMS.Models;


namespace NMS.Repository
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) 
        {
            _db = db;
        }

        public async Task<List<Category>> GetCategories()
        {

            return await _db.Categories.ToListAsync();
        }


        public async Task<Category> CreateCategory(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return (category);
        }

    }
}
