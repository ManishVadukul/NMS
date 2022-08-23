using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NMS.Core.IRepositories;
using NMS.Data;
using NMS.Models;

namespace NMS.Core.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base()
        {
            _db = db;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return (await _db.Categories.ToListAsync());
        }

        public async Task<Category> CreateCategory(Category category)
        {
                await _db.AddAsync(category);
                _db.SaveChanges();
                return category;
        }
    }
}